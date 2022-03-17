using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApinorthwind22.Models;

namespace RestApinorthwind22.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Customer> GetAll()
        {
            northwindContext context = new northwindContext();
            List<Customer> customers = context.Customers.ToList();
            return customers;
        }

        [HttpGet]
        [Route("{id}")]
        public Customer GetOneCustomer(string id)
        {
            northwindContext context = new northwindContext();
            Customer customer = context.Customers.Find(id);
            return customer;
        }

        [HttpGet]
        [Route("country/{key}")]
        public List<Customer> GetSomeCustomers(string key)
        {
            northwindContext context = new northwindContext();
            
            var someCustomers = from c in context.Customers
                                where c.Country == key
                                select c;

            return someCustomers.ToList();
        }
    }
}
