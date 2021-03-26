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
        public JsonResult GetCustomers()
        {
            return new JsonResult(new List<Object>()
            {
                new { CustomerId = 1, ContactName = "Anderson"},
                new { CustomerId = 2, ContactName = "Solaris"},
            }); ;
        }
    }
}
