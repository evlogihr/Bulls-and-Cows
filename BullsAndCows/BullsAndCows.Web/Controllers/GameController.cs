namespace BullsAndCows.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using BullsAndCows.Data;
    using BullsAndCows.Services;
    using BullsAndCows.Services.Contracts;

    [Authorize]
    [RoutePrefix("api/Game")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GameController : ApiController
    {
        private IGameService gameService;

        public GameController()
        {
            this.gameService = new GameService(ApplicationDbContext.Create());
        }

        public GameController(IGameService service)
        {
            this.gameService = service;
        }

        [HttpPost]
        public IHttpActionResult StartNewGame()
        {
            var userId = User.Identity.GetUserId();
            var num = this.gameService.StartGame(userId);

            return Ok(num);
        }

        [HttpGet]
        [Route("Active")]
        public IHttpActionResult GetActiveGames()
        {
            var userId = User.Identity.GetUserId();
            var result = this.gameService.GetUserActiveGameIds(userId);

            return Ok(result);
        }

        [HttpGet]
        [Route("Active")]
        public IHttpActionResult GetActiveGame(Guid gameId)
        {
            var result = this.gameService.GetGame(gameId);

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult Guess(Guid gameId, string guess)
        {
            if (string.IsNullOrWhiteSpace(guess) || guess.Length != 4)
            {
                return Content(HttpStatusCode.BadRequest, "The number should contain 4 digits");
            }

            var userId = User.Identity.GetUserId();
            var result = this.gameService.Guess(userId, gameId, guess);

            return Ok(result);
        }
    }
}
