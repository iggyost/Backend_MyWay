using Backend_MyWay.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_MyWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : Controller
    {
        public static MyWayDbContext context = new MyWayDbContext();
        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<Country>> Get()
        {
            try
            {
                var data = context.Countries.ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
