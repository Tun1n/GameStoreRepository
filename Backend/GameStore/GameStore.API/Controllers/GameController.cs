using GameStore.Application.DTO.GameDTO;
using GameStore.Application.IServicesInterfaces;
using GameStore.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameGetDTO>>> GetGames([FromQuery] PaginationParams paginationParams )
        {
            var games = await _gameService.GetPagedAsync( paginationParams );

            return games.Success
            ? Ok(games.Data)
            : BadRequest(new { message = games.Message });
        }
    }
}
