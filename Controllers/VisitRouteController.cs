using Backend_MyWay.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_MyWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitRouteController : Controller
    {
        public static MyWayDbContext context = new MyWayDbContext();
        [HttpGet]
        [Route("get/{visitId}")]
        public ActionResult<IEnumerable<VisitsRoute>> Get(int visitId)
        {
            try
            {
                var data = context.VisitsRoutes.Where(x => x.VisitId == visitId).ToList();
                if (data != null)
                {
                    return data;
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
    }
}