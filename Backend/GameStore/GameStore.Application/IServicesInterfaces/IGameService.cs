using GameStore.Application.DTO.GameDTO;
using GameStore.Application.Pagination;
using GameStore.Domain.Models;

namespace GameStore.Application.IServicesInterfaces
{
    public interface IGameService
    {
        Task<Result<GameCreateDTO>> AddAsync(GameCreateDTO game);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<GameUpdateDTO>> UpdateAsync(int id, GameUpdateDTO newgame);
        Task<Result<GameGetDTO>> GetByNameAsync(string name);
        Task<Result<GameGetDTO>> GetByIdAsync(int id);
        Task<Result<PagedResult<GameGetDTO>>> GetPagedAsync(PaginationParams pagination);
    }
}
