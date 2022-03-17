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

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(string id)
        {
            var customer = context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound("Asiakasta id:llä " + id + " ei löytynyt");
            }
            else
            {
                try
                {
                    context.Customers.Remove(customer);
                    context.SaveChanges();

                    return Ok("Poistettiin asiakas " + customer.CompanyName);
                }
                catch (Exception e)
                {
                    return BadRequest("Poisto ei onnistunut. Ongelma saattaa johtua siitä, jos asiakkaalla on tilauksia?");
                }
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult PutEdit(string id, [FromBody] Customers asiakas)
        {

            try
            {
                var customer = context.Customers.Find(id);
                if (customer != null)
                {
                    customer.CompanyName = asiakas.CompanyName;
                    customer.ContactName = asiakas.ContactName;
                    customer.ContactTitle = asiakas.ContactTitle;
                    customer.Country = asiakas.Country;
                    customer.Address = asiakas.Address;
                    customer.City = asiakas.City;
                    customer.PostalCode = asiakas.PostalCode;
                    customer.Phone = asiakas.Phone;
                    customer.Fax = asiakas.Fax;

                    context.SaveChanges();
                    return Ok("Muokattu asiakasta: " + customer.CompanyName);
                }
                else
                {
                    return NotFound("Päivitettävää asiakasta ei löytynyt!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen asiakasta päivitettäessä. Alla lisätietoa" + e);
            }

        }

    }
}
