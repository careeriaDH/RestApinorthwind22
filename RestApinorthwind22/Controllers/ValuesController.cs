using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApinorthwind22.Models;

namespace RestApinorthwind22.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("{key}")]
        public ActionResult GetDoc(string key)
        {
            northwindContext context = new northwindContext();

            List<Documentation> privateDocList = (from d in context.Documentation
                                                  where d.Keycode == key
                                                  select d).ToList();
            if (privateDocList.Count > 0)
            {
                return Ok(privateDocList);
            }
            else
            {
                return BadRequest("Antamallasi koodilla ei löydy dokumentaatiota, päiväys: " + DateTime.Now.ToString());
            }
        }
    }
}
