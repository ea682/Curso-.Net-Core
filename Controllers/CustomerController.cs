using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindApiDemo.EFModelsclear;
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
        
        private IMapper MapperCustomer()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Customer, CustomerWithoutOrders>();
            });
            IMapper mapper = config.CreateMapper();
            
            return mapper;
        }
        private IMapper MapperCustomerDTO()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Customer, CustomerDTO>();
            });
            IMapper mapper = config.CreateMapper();

            return mapper;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers =
                _customerRepository
                .GetCustomers();

            var mapper = MapperCustomer();
            var results = mapper.Map<IEnumerable<CustomerWithoutOrders>>(customers);
            return new JsonResult(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(string id, bool includeOrdes)
        {
            //Inicializamos mapeo
            

            var customer = _customerRepository.GetCustomers(id, includeOrdes);

            if (customer == null)
            {
                return NotFound();

            }

            if (includeOrdes)
            {
                var mapperDTO = MapperCustomerDTO();
                var customerResultOnlyDTO = mapperDTO.Map<Customer>(customer);
                return Ok(customerResultOnlyDTO);
            }
            var mapper = MapperCustomer();
            var customerResultOnly = mapper.Map<CustomerWithoutOrders>(customer);
            return Ok(customerResultOnly);
            //return new JsonResult(result);
        }
    }
}
