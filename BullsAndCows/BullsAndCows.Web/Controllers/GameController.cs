namespace BullsAndCows.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using BullsAndCows.Services;
    using BullsAndCows.Data;

    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GameController : ApiController
    {
        private GameService gameService;

        public GameController()
        {
            this.gameService = new GameService(ApplicationDbContext.Create());
        }

        [HttpPost]
        public IHttpActionResult StartNewGame()
        {
            var userId = User.Identity.GetUserId();
            var num = this.gameService.StartGame(userId);

            return Ok(num);
        }

        [HttpGet]
        public IHttpActionResult Guess(string guess)
        {
            var userId = User.Identity.GetUserId();
            var result = this.gameService.Guess(userId, guess);

            return Json(result);
        }
    }
}
