using Microsoft.AspNetCore.Mvc;
using NorthwindApiDemo.Models;
using NorthwindApiDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApiDemo.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers =
                _customerRepository
                .GetCustomers();

            var results = new List<CustomerWithoutOrders>();
            foreach (var customer in customers)
            {
                results.Add(new CustomerWithoutOrders()
                {
                    CustomerId = customer.CustomerId,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Conuntry = customer.Country,
                    Phone = customer.Phone,
                    Fax = customer.Fax
                });
            }
            return new JsonResult(results);
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
