using GameStore.Domain.Models;
using GameStore.Domain.RepositoriesInterfaces.IGameRepositories;
using GameStore.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            return game;
        }

        public async Task<Game?> GetByNameAsync(string name)
        {
            var game = await _context.Games
            .FirstOrDefaultAsync(c => c.Name == name);

            return game;
        }

        public async Task<(IEnumerable<Game> Items, int Total)> GetPagedGames(PaginationParams pagination)
        {
            var query = _context.Games.AsNoTracking();

            var total = await query.CountAsync();

            var items = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<Game> UpdateAsync(Game game)
        {
            var existingGame = await _context.Games
                .FirstOrDefaultAsync(c => c.Id == game.Id);

            _context.Entry(existingGame!).CurrentValues.SetValues(game);

            await _context.SaveChangesAsync();
            return existingGame!;
        }
    }
}
