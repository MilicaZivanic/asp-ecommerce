
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


        // POST api/<FakeDataController>
        [HttpPost]
        public void Post()
        {
            var usersFaker = new Faker<User>();
            usersFaker.RuleFor(x => x.FirstName, f => f.Name.FirstName());
            usersFaker.RuleFor(x => x.LastName, f => f.Name.LastName());
            usersFaker.RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName));
            usersFaker.RuleFor(x => x.Password, f => f.Internet.Password());
            var users = usersFaker.Generate(30);

            var addressesFaker = new Faker<UserAddress>();
            addressesFaker.RuleFor(x => x.Address, f => f.Address.StreetAddress());
            addressesFaker.RuleFor(x => x.City, f => f.Address.City());
            addressesFaker.RuleFor(x => x.Country, f => f.Address.Country());
            addressesFaker.RuleFor(x => x.PostalCode, f => f.Address.ZipCode());
            addressesFaker.RuleFor(x => x.Telephone, f => f.Person.Phone);
            addressesFaker.RuleFor(x => x.User, f => f.PickRandom(users));
            var addresses = addressesFaker.Generate(15);

            var paymentsFaker = new Faker<UserPayment>();
            paymentsFaker.RuleFor(x => x.PaymentType, f => f.Finance.TransactionType());
            paymentsFaker.RuleFor(x => x.AccountNumber, f => f.Random.Number(1000000, 9999999));
            paymentsFaker.RuleFor(x => x.Provider, f => f.Finance.AccountName());
            paymentsFaker.RuleFor(x => x.Expiry, f => f.Date.Future());
            paymentsFaker.RuleFor(x => x.User, f => f.PickRandom(users));
            var payments = paymentsFaker.Generate(15);

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

            var useCases = new List<UserUseCase>();
            for(var i = 1; i <= 31; i++)
            {
                useCases.Add(new UserUseCase
                {
                    UseCaseId = i,
                    User = users.First()
                });
            }

            _context.Users.AddRange(users);
            _context.UserUseCases.AddRange(useCases);
            _context.UserAddresses.AddRange(addresses);
            _context.UserPayments.AddRange(payments);
            _context.Categories.AddRange(categories);
            _context.Discounts.AddRange(discounts);
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

    }
}
