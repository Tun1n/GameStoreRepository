using FluentValidation;
using GameStore.Application.DTO.GameDTO;
using GameStore.Application.IServicesInterfaces;
using GameStore.Application.Pagination;
using GameStore.Domain.Models;
using GameStore.Domain.RepositoriesInterfaces.IGameRepositories;

namespace GameStore.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IValidator<GameCreateDTO> _gameCreateValidator;
        private readonly IValidator<GameUpdateDTO> _gameUpdateValidator;
        private readonly IValidator<GamePatchDTO> _gamePatchValidator;

        public GameService(IGameRepository gameRepository, 
            IValidator<GameCreateDTO> gameCreateValidator, 
            IValidator<GameUpdateDTO> gameUpdateValidator,
            IValidator<GamePatchDTO> gamePatchValidator)
        {
            _gameRepository = gameRepository;
            _gameCreateValidator = gameCreateValidator;
            _gameUpdateValidator = gameUpdateValidator;
            _gamePatchValidator = gamePatchValidator;
        }

        public async Task<Result<GameCreateDTO>> AddAsync(GameCreateDTO game)
        {
            var validacao = _gameCreateValidator.Validate(game);
            if (!validacao.IsValid)
            {
                var errors = string.Join(", ", validacao.Errors.Select(e => e.ErrorMessage));
                return Result<GameCreateDTO>.Failure(errors);
            }

            var existingGame = await _gameRepository.GetByNameAsync(game.Name); 
            if (existingGame is not null)
            {
                return Result<GameCreateDTO>.Failure("Game already exists");
            }

            var newGame = new Game
            {
                Name = game.Name,
                ImageURL = game.ImageURL
            };

            await _gameRepository.AddAsync(newGame);

            var createdGame = new GameCreateDTO(newGame.Name, newGame.ImageURL);

            return Result<GameCreateDTO>.Ok(createdGame);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var existingGame = await _gameRepository.GetByIdAsync(id);

            if (existingGame is null)
                return Result<bool>.Failure("Game not found");

            await _gameRepository.DeleteAsync(id);

            return Result<bool>.Ok(true, "Game successfully deleted");
        }

        public async Task<Result<GameGetDTO>> GetByIdAsync(int id)
        {
            var existingGame = await _gameRepository.GetByIdAsync(id);

            if (existingGame is null)
                return Result<GameGetDTO>.Failure("Game not found");

            var gameGetDTO = new GameGetDTO(existingGame.Name, existingGame.ImageURL, existingGame.IsInstalled, existingGame.Id);
            return Result<GameGetDTO>.Ok(gameGetDTO);
        }

        public async Task<Result<GameGetDTO>> GetByNameAsync(string name)
        {
            var existingGame = await _gameRepository.GetByNameAsync(name);

            if (existingGame is null)
                return Result<GameGetDTO>.Failure("Game not found");

            var gameGetDTO = new GameGetDTO(existingGame.Name, existingGame.ImageURL, existingGame.IsInstalled, existingGame.Id);
            return Result<GameGetDTO>.Ok(gameGetDTO);
        }

        public async Task<Result<PagedResult<GameGetDTO>>> GetPagedAsync(PaginationParams pagination)
        {
            var (items, total) = await _gameRepository.GetPagedGames(pagination);

            var result = new PagedResult<GameGetDTO>
            {
                Items = items.Select(game => new GameGetDTO(game.Name, game.ImageURL, game.IsInstalled, game.Id)).ToList(),
                TotalItems = total,
                Page = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            return Result<PagedResult<GameGetDTO>>.Ok(result);
        }

        public async Task<Result<GameUpdateDTO>> UpdateAsync(int id, GameUpdateDTO newGame)
        {
            var existingGame = await _gameRepository.GetByIdAsync(id);

            if (existingGame is null)
                return Result<GameUpdateDTO>.Failure("Game not found");

            var validation = _gameUpdateValidator.Validate(newGame);
            if (!validation.IsValid)
            {
                var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                return Result<GameUpdateDTO>.Failure(errors);
            }

            existingGame.Name = newGame.Name;
            existingGame.ImageURL = newGame.ImageURL;
            existingGame.IsInstalled = newGame.IsInstalled;

            await _gameRepository.UpdateAsync(existingGame);

            var updatedGame = new GameUpdateDTO(existingGame.Name, existingGame.ImageURL, existingGame.IsInstalled);
            return Result<GameUpdateDTO>.Ok(updatedGame);
        }

        public async Task<Result<GamePatchDTO>> PartialUpdateAsync(int id, GamePatchDTO newgame)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                return Result<GamePatchDTO>.Failure("Game not found.");

            var validation = _gamePatchValidator.Validate(newgame);
            if (!validation.IsValid)
            {
                var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                return Result<GamePatchDTO>.Failure(errors);
            }


            if (newgame.Name != null) game.Name = newgame.Name;
            if (newgame.ImageURL != null) game.ImageURL = newgame.ImageURL;
            if (newgame.IsInstalled != null) game.IsInstalled = newgame.IsInstalled.Value;

            await _gameRepository.UpdateAsync(game);

            return Result<GamePatchDTO>.Ok(new GamePatchDTO(game.Name, game.ImageURL, game.IsInstalled));
        }
    }
}
