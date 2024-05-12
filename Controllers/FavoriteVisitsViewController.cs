using Backend_MyWay.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_MyWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteVisitsViewController : Controller
    {
        public static MyWayDbContext context = new MyWayDbContext();
        [HttpGet]
        [Route("get/{userId}")]
        public ActionResult<IEnumerable<FavoriteVisitsView>> Get(int userId)
        {
            try
            {
                var data = context.FavoriteVisitsViews.Where(x => x.UserId == userId).ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("save/{userId}/{visitId}")]
        public ActionResult<IEnumerable<FavoriteVisitsView>> Save(int userId, int visitId)
        {
            try
            {
                var data = context.TrendingVisitsViews.Where(x => x.VisitId == visitId).FirstOrDefault();
                if (data != null)
                {
                    Favorite newFavorite = new Favorite()
                    {
                        UserId = userId,
                        VisitId = data.VisitId,
                    };
                    context.Favorites.Add(newFavorite);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("remove/{userId}/{visitId}")]
        public ActionResult<IEnumerable<FavoriteVisitsView>> Remove(int userId, int visitId)
        {
            try
            {
                var data = context.Favorites.Where(x => x.UserId == userId && x.VisitId == visitId).FirstOrDefault();
                context.Favorites.Remove(data);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}