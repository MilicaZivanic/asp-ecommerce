
using Bogus;
using Domain;
using EfDataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeDataController : ControllerBase
    {

        private readonly EcomShopContext _context;

        public FakeDataController(EcomShopContext context)
        {
            _context = context;
        }

        // GET: api/<FakeDataController>
        [HttpGet]
        public void Get()
        {
            var categoriesFaker = new Faker<Category>();
            categoriesFaker.RuleFor(x => x.Name, f => f.Random.Word());
            categoriesFaker.RuleFor(x => x.Description, f => f.Lorem.Text());
            var categories = categoriesFaker.Generate(10);

            var discountsFaker = new Faker<Discount>();
            discountsFaker.RuleFor(x => x.Name, f => f.Random.Word());
            discountsFaker.RuleFor(x => x.Description, f => f.Lorem.Text());
            discountsFaker.RuleFor(x => x.DiscountPercent, f => f.Random.Number(1, 100));
            var discounts = discountsFaker.Generate(10);

            var productsFaker = new Faker<Product>();
            productsFaker.RuleFor(x => x.Price, f => f.Random.Decimal(1, 10000));
            productsFaker.RuleFor(x => x.Name, f => f.Commerce.ProductName());
            productsFaker.RuleFor(x => x.Description, f => f.Lorem.Text());
            productsFaker.RuleFor(x => x.Quantity, f => f.Random.Number(1, 100));
            productsFaker.RuleFor(x => x.Category, f => f.PickRandom(categories));
            productsFaker.RuleFor(x => x.Discount, f => f.PickRandom(discounts));
            var products = productsFaker.Generate(30);

            _context.Categories.AddRange(categories);
            _context.Discounts.AddRange(discounts);
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        // GET api/<FakeDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FakeDataController>
        [HttpPost]
        public void Post()
        {

        }

        // PUT api/<FakeDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FakeDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
