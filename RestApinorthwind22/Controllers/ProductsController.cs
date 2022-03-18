using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApinorthwind22.Models;

namespace RestApinorthwind22.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly northwindContext context = new northwindContext();

        [HttpGet] //Haetaan kaikki tuotteet
        public ActionResult GetAll()
        {
            var tuotteet = context.Products;
            return Ok(tuotteet);

        }

        [HttpGet] //Haetaan tuote id:n avulla
        [Route("{id}")] 
        public ActionResult GetOneProduct(int id)
        {
            try
            {
                var tuote = context.Products.Find(id);

                if (tuote == null)
                {
                    return NotFound("Tuotetta id:llä " + id + " ei löytynyt.");
                }

                return Ok(tuote);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet] //Hakee tuotteet kategoriaid:n perusteella 
        [Route("category/{id}")]
        public IEnumerable<Product> GetSomeProducts(int id)
        {

            var someProducts = from p in context.Products
                                where p.CategoryId == id
                                select p;

            return someProducts.ToList();
        }

        [HttpPost] //Luodaan uusi product
        [Route("")]
        public ActionResult PostCreateNew([FromBody] Product tuote)
        {
            try
            {
                context.Products.Add(tuote);
                context.SaveChanges();
                return Ok("Lisättiin tuote " + tuote.ProductName); //palauttaa vastaluodun uuden tuotteen nimen
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen tuotetta lisättäessä" + e);
            }

        }

        [HttpPut]
        [Route("update/{id}")]
        public ActionResult PutEditProduct(int id, [FromBody] Product tuotem)
        {

            try
            {
                var tuote = context.Products.Find(id);
                if (tuote != null)
                {
                    tuote.ProductName = tuotem.ProductName;
                    tuote.SupplierId = tuotem.SupplierId;
                    tuote.CategoryId = tuotem.CategoryId;
                    tuote.QuantityPerUnit = tuotem.QuantityPerUnit;
                    tuote.UnitPrice = tuotem.UnitPrice;
                    tuote.UnitsInStock = tuotem.UnitsInStock;
                    tuote.UnitsOnOrder = tuotem.UnitsOnOrder;
                    tuote.ReorderLevel = tuotem.ReorderLevel;
                    tuote.Discontinued = tuotem.Discontinued;

                    context.SaveChanges();
                    return Ok("Muokattu tuotetta: " + tuote.ProductName);
                }
                else
                {
                    return NotFound("Päivitettävää tuotetta ei löytynyt!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen tuotetta päivitettäessä. Alla lisätietoa" + e);
            }

        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public ActionResult Remove(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound("Tuotetta id:llä " + id + " ei löytynyt");
            }
            else
            {
                try
                {
                    context.Products.Remove(product);
                    context.SaveChanges();

                    return Ok("Poistettiin tuote " + product.ProductName);
                }
                catch (Exception e)
                {
                    return BadRequest("Poisto ei onnistunut. " + e);
                }
            }
        }
    }
}
