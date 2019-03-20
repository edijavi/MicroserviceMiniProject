using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using SharedModels;
using RestSharp;
using EasyNetQ;
using OrderApi.Infrastructure;

namespace OrderApi.Controllers
{
    [Route("api/orders")]
    public class OrdersController : Controller
    {
		IRepository<Order> repository;
        IServiceGateway<Product> productServiceGateway;
        IMessagePublisher messagePublisher;

		public OrdersController(IRepository<Order> repos, IServiceGateway<Product> gateway, IMessagePublisher publisher)
        {
            repository = repos;
			productServiceGateway = gateway;
            messagePublisher = publisher;
        }

        // GET: api/orders
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return repository.GetAll();
        }

        // GET api/products/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/orders
        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
			/*Other Way.
            // Call ProductApi to get the product ordered
            RestClient c = new RestClient();
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            c.BaseUrl = new Uri("https://localhost:5001/api/products/");
            var request = new RestRequest(order.ProductId.ToString(), Method.GET);
            var response = c.Execute<Product>(request);
			*/

			var orderedProduct = productServiceGateway.Get(order.ProductId);


            if (order.Quantity <= orderedProduct.ItemsInStock - orderedProduct.ItemsReserved)
            {
                /*
				// reduce the number of items in stock for the ordered product,
                // and create a new order.
                orderedProduct.ItemsReserved += order.Quantity;
                var updateRequest = new RestRequest(orderedProduct.Id.ToString(), Method.PUT);
                updateRequest.AddJsonBody(orderedProduct);
                var updateResponse = c.Execute(updateRequest);
				*/
			
				try
					{
						// Publish OrderStatusChangedMessage. If this operation
						// fails, the order will not be created
						messagePublisher.PublishOrderStatusChangedMessage(order.ProductId, 
							order.Quantity, "orderCompleted");

						// Create order.
						order.Status = Order.OrderStatus.completed;
						var newOrder = repository.Add(order);
						return CreatedAtRoute("GetOrder", new { id = newOrder.Id }, newOrder);
					}
					catch
					{
						return StatusCode(500, "An error happened. Try again.");
					}				
			}
			else
			{
				// If the order could not be created, "return no content".
				return StatusCode(500, "Not enough items in stock.");
			}
        }

    }
}
