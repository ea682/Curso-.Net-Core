using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApiDemo.Controllers
{
    [Route("api/customers")]
    public class OrdersController : Controller
    {
        [HttpGet("{customerId}/orders")]
        public IActionResult GetOrders(int customerId)
        {
            var customer = Repository.Instance.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer.Orders);
        }

        [HttpGet("{customerId}/orders/{orderId}")]
        public IActionResult GetOrder(int customerId, int orderId)
        {
            var customer = Repository.Instance.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var order = customer.Orders.FirstOrDefault(c => c.OderId == orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
