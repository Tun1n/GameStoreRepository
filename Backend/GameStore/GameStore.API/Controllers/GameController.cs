using GameStore.Application.DTO.GameDTO;
using GameStore.Application.IServicesInterfaces;
using GameStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers
{
    [Route("Games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameService _gameService;
        public GameController(ILogger<GameController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<GameGetDTO>>> GetGames([FromQuery] PaginationParams paginationParams )
        {
            _logger.LogInformation("Processing games...");
            var games = await _gameService.GetPagedAsync( paginationParams );

            return games.Success
            ? Ok(games.Data) 
            : BadRequest(new { message = games.Message });
        }

        [HttpGet("{gameName}")]
        public async Task<ActionResult<GameGetDTO>> GetGameByName(string gameName) {

            _logger.LogInformation($"Finding game with name: {gameName}...");
            var game = await _gameService.GetByNameAsync( gameName );

            return game.Success
            ? Ok(game.Data)
            : BadRequest(new { message = game.Message });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GameGetDTO>> GetGameById(int id)
        {

            _logger.LogInformation($"Finding game with id: {id}...");
            var game = await _gameService.GetByIdAsync(id);

            return game.Success
            ? Ok(game.Data)
            : BadRequest(new { message = game.Message });
        }

        [HttpPost()]
        public async Task<ActionResult<GameCreateDTO>> CreateGame([FromBody] GameCreateDTO gameCreateDTO)
        {
            _logger.LogInformation("Creating game {GameName}...", gameCreateDTO.Name);

            var gameCreated = await _gameService.AddAsync(gameCreateDTO);

            if (!gameCreated.Success)
            {
                _logger.LogError("Error to create game {GameName}: {Message}", gameCreateDTO.Name, gameCreated.Message);
                return BadRequest(new { message = gameCreated.Message });
            }

            _logger.LogInformation("Game {GameName} created", gameCreateDTO.Name);
            return Ok(gameCreated.Data);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GameUpdateDTO>> UpdateGame(int id, [FromBody] GameUpdateDTO gameUpdateDTO)
        {
            _logger.LogInformation($"Updating game: {gameUpdateDTO.Name}");

            var updated = await _gameService.UpdateAsync(id, gameUpdateDTO);

            if (!updated.Success)
            {
                _logger.LogError("Error to update {Id}: {Message}", id, updated.Message);
                return BadRequest(new { message = updated.Message });
            }

            _logger.LogInformation("Update!");
            return Ok(updated.Data);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteGame(int id)
        {
            _logger.LogInformation("Deleting game with {Id}...", id);

            var deleted = await _gameService.DeleteAsync(id);

            if (!deleted.Success)
            {
                _logger.LogError("Error to delete game {Id}: {Message}", id, deleted.Message);
                return NotFound(new { message = deleted.Message });
            }

            _logger.LogInformation("Game {Id} deleted", id);
            return Ok(new { message = "Game successfully deleted" });
        }
    }
}
