using GameStore.Domain.Models;

namespace GameStore.Domain.RepositoriesInterfaces.IGameRepositories
{
    public interface IGameRepository
    {
        Task AddAsync(Game game);
        Task<Game> UpdateAsync(Game game);
        Task<Game> PartialUpdateAsync(Game game);
        Task DeleteAsync(int id);
        Task<Game?> GetByIdAsync(int id);
        Task<Game?> GetByNameAsync(string name);
        Task<(IEnumerable<Game> Items, int Total)> GetPagedGames(PaginationParams pagination);
    }
}
