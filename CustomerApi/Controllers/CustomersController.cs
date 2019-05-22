using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerApi.Data;
using CustomerApi.Infrastructure;
using SharedModels;

namespace CustomerApi.Controllers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        IRepository<Customer> repository;
        IServiceGateway<Product> productServiceGateway;
        IMessagePublisher messagePublisher;

        public CustomersController(IRepository<Customer> repos, IServiceGateway<Product> gateway, IMessagePublisher publisher)
        {
            repository = repos;
            productServiceGateway = gateway;
            messagePublisher = publisher;
        }
        // GET api/customers
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return repository.GetAll();
        }

        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/customers
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
