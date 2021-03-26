using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApiDemo.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        [HttpGet]
        public IActionResult GetCustomers()
        {
            return new JsonResult(Repository.Instance.Customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var result =
                Repository.Instance.Customers
                .FirstOrDefault(c => c.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
            //return new JsonResult(result);
        }
    }
}
