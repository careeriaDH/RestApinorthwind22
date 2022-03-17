using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApinorthwind22.Models;

namespace RestApinorthwind22.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static readonly northwindContext context = new northwindContext();
        
        [HttpGet]
        public ActionResult GetAll()
        {
            var customers = context.Customers;
            return Ok(customers);

        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneCustomer(string id)
        {
            try
            {
                var asiakas = context.Customers.Find(id);

                if (asiakas == null)
                {
                    return NotFound("Asiakasta id:llä " + id + " ei löytynyt.");
                }

                return Ok(asiakas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("country/{key}")]
        public List<Customers> GetSomeCustomers(string key)
        {
            
            var someCustomers = from c in context.Customers
                                where c.Country == key
                                select c;

            return someCustomers.ToList();
        }

        [HttpPost] //<--filtteri joka sallii vain post metodit
        [Route("")]
        public ActionResult PostCreateNew([FromBody] Customers asiakas)
        {
            try
            {
                context.Customers.Add(asiakas);
                context.SaveChanges();
                return Ok("Lisättiin asiakas " + asiakas.CompanyName); //palauttaa vastaluodun uuden asiakkaan avaimen arvon
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen asiakasta lisättäessä" + e);
            } 
            
        }
    }
}
