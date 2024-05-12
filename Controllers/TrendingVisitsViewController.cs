using Backend_MyWay.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_MyWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrendingVisitsViewController : Controller
    {
        public static MyWayDbContext context = new MyWayDbContext();
        [HttpGet]
        [Route("get/{countryId}")]
        public ActionResult<IEnumerable<TrendingVisitsView>> Get(int countryId)
        {
            try
            {
                var data = context.TrendingVisitsViews.Where(x => x.CountryId == countryId).ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("get/all")]
        public ActionResult<IEnumerable<TrendingVisitsView>> GetAll()
        {
            try
            {
                var data = context.TrendingVisitsViews.ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("search/{countryId}/{text}")]
        public ActionResult<IEnumerable<TrendingVisitsView>> Search(int countryId, string text)
        {
            try
            {
                var data = context.TrendingVisitsViews.Where(x => x.CountryId == countryId && x.Name.Contains(text)).ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
