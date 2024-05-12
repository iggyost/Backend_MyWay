using Backend_MyWay.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_MyWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsViewController : Controller
    {
        public static MyWayDbContext context = new MyWayDbContext();
        [HttpGet]
        [Route("get/{visitId}")]
        public ActionResult<IEnumerable<VisitsView>> Get(int visitId)
        {
            try
            {
                var data = context.VisitsViews.Where(x => x.VisitId == visitId).ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
