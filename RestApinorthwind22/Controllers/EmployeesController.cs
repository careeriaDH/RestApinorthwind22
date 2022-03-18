using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApinorthwind22.Models;

namespace RestApinorthwind22.Controllers
{
    [Route("employee/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private static readonly northwindContext context = new northwindContext();

        [HttpGet] //Haetaan kaikki tuotteet
        public ActionResult GetAll()
        {
            var tt = context.Employees;
            return Ok(tt);

        }

        [HttpGet] //Haetaan tuote id:n avulla
        [Route("{id}")]
        public ActionResult GetOneTt(int id)
        {
            try
            {
                var tt = context.Employees.Find(id);

                if (tt == null)
                {
                    return NotFound("Työntekijää id:llä " + id + " ei löytynyt.");
                }

                return Ok(tt);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet] //Hakee työntekijät tittelin perusteella
        [Route("title/{key}")]
        public List<Employee> GetSomeTt(string key)
        {

            var someTt = from e in context.Employees
                                where e.Title == key
                                select e;

            return someTt.ToList();
        }

        [HttpPost] //Luodaan uusi työntekijä
        [Route("")]
        public ActionResult PostCreateNew([FromBody] Employee tt)
        {
            try
            {
                context.Employees.Add(tt);
                context.SaveChanges();
                return Ok("Lisättiin työntekijä " + tt.LastName); //palauttaa vastaluodun uuden työntekijän sukunimen
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen tuotetta lisättäessä" + e);
            }

        }

        [HttpPut]
        [Route("update/{id}")]
        public ActionResult PutEditTt(int id, [FromBody] Employee ttm)
        {

            try
            {
                var tt = context.Employees.Find(id);
                if (tt != null)
                {
                    tt.LastName = ttm.LastName;
                    tt.FirstName = ttm.FirstName;
                    tt.Title = ttm.Title;
                    tt.TitleOfCourtesy = ttm.TitleOfCourtesy;
                    tt.BirthDate = ttm.BirthDate;
                    tt.HireDate = ttm.HireDate;
                    tt.Address = ttm.Address;
                    tt.City = ttm.City;
                    tt.Region = ttm.Region;
                    tt.PostalCode = ttm.PostalCode;
                    tt.Country = ttm.Country;
                    tt.HomePhone = ttm.HomePhone;

                    context.SaveChanges();
                    return Ok("Muokattu tuotetta: " + tt.LastName);
                }
                else
                {
                    return NotFound("Päivitettävää työntekijää ei löytynyt!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen työntekijän tietoja päivitettäessä. Alla lisätietoa" + e);
            }

        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public ActionResult Remove(int id)
        {
            var tyte = context.Employees.Find(id);
            if (tyte == null)
            {
                return NotFound("Työntekijää id:llä " + id + " ei löytynyt");
            }
            else
            {
                try
                {
                    context.Employees.Remove(tyte);
                    context.SaveChanges();

                    return Ok("Poistettiin työntekijän tiedot " + tyte.LastName);
                }
                catch (Exception e)
                {
                    return BadRequest("Poisto ei onnistunut." + e);
                }
            }
        }
    }
}
