using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    public class OrdersController : Controller
    {
        private ICustomerRepository _customerRepository;
        public OrdersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        private IMapper MapperOrders()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrdersDTO>();
            });
            IMapper mapper = config.CreateMapper();

            return mapper;
        }

        private IMapper MapperOrder()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, Order>();
            });
            IMapper mapper = config.CreateMapper();

            return mapper;
        }


        [HttpGet("{customerId}/orders")]
        public IActionResult GetOrders(string customerId)
        {
            if (!_customerRepository.CustumerExists(customerId))
            {
                return NotFound();
            }
            var orders =
                _customerRepository.GetOrdes(customerId);

            var mapperOrdersDTO = MapperOrders();
            var orderResult = mapperOrdersDTO.Map<IEnumerable<OrdersDTO>>(orders);

            return Ok(orderResult);
        }

        [HttpGet("{customerId}/orders/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(string customerId, int id)
        {

            if (!_customerRepository.CustumerExists(customerId))
            {
                return NotFound();
            }

            var order =
                _customerRepository.GetOrder(customerId, id);

            if (order == null)
            {
                return NotFound();
            }

            var mapperOrdersDTO = MapperOrders();
            var orderResult = mapperOrdersDTO.Map<Order>(order);

            return Ok(orderResult);
        }
        [HttpPost("{customerId}/orders")]
        public IActionResult CreateOrder(string customerId, [FromBody] OrdersForCreationDTO order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderWitchout>();
            });
            IMapper mapper = config.CreateMapper();
            var orderResult = mapper.Map<Order>(order);

            _customerRepository.AddOrder(customerId, orderResult);

            if (!_customerRepository.Save())
            {
                return StatusCode(500, "Place verify your data");
            }

            var mapperOrders = MapperOrders();
            var resultMapperOrdersDTO = mapperOrders.Map<OrdersDTO>(orderResult);


            return CreatedAtRoute("GetOrder",
                new 
                { 
                    customerId = customerId,
                    id = resultMapperOrdersDTO.OrderId 
                },
                orderResult
                );
        }

        [HttpPut("{customerId}/orders/{id}")]
        public IActionResult UpdateOrder(int customerId, int id, [FromBody] OrdersForCreationDTO order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Repository.Instance.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var orderFromRepository =
                customer.Orders.FirstOrDefault(o => o.OrderId == id);
            if (orderFromRepository == null)
            {
                return NotFound();
            }


            orderFromRepository.CustomerId = order.CustomerId;
            orderFromRepository.EmployeeId = order.EmployeeId;
            orderFromRepository.OrderDate = order.OrderDate;
            orderFromRepository.RequiredDate = order.RequiredDate;
            orderFromRepository.ShiippedDate = order.ShiippedDate;
            orderFromRepository.ShipVia = order.ShipVia;
            orderFromRepository.Freigth = order.Freigth;
            orderFromRepository.ShipName = order.ShipName;
            orderFromRepository.ShipAddress = order.ShipAddress;
            orderFromRepository.ShipCity = order.ShipCity;
            orderFromRepository.ShipRegion = order.ShipRegion;
            orderFromRepository.ShipPostalCode = order.ShipPostalCode;
            orderFromRepository.ShipCountry = order.ShipCountry;

            return NoContent();
        }

        [HttpPatch("{customerId}/orders/{id}")]
        public IActionResult UpdateOrder(int customerId, int id, [FromBody] JsonPatchDocument<OrdersForUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Repository.Instance.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var orderFromRepository =
                customer.Orders.FirstOrDefault(o => o.OrderId == id);
            if (orderFromRepository == null)
            {
                return NotFound();
            }

            var orderToUpdate = new OrdersForUpdateDto()
            {
                CustomerId = orderFromRepository.CustomerId,
                EmployeeId = orderFromRepository.EmployeeId,
                OrderDate = orderFromRepository.OrderDate,
                RequiredDate = orderFromRepository.RequiredDate,
                ShiippedDate = orderFromRepository.ShiippedDate,
                ShipVia = orderFromRepository.ShipVia,
                Freigth = orderFromRepository.Freigth,
                ShipName = orderFromRepository.ShipName,
                ShipAddress = orderFromRepository.ShipAddress,
                ShipCity = orderFromRepository.ShipCity,
                ShipRegion = orderFromRepository.ShipRegion,
                ShipPostalCode = orderFromRepository.ShipPostalCode,
                ShipCountry = orderFromRepository.ShipCountry
            };

            patchDocument.ApplyTo(orderToUpdate);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            orderFromRepository.CustomerId = orderToUpdate.CustomerId;
            orderFromRepository.EmployeeId = orderToUpdate.EmployeeId;
            orderFromRepository.OrderDate = orderToUpdate.OrderDate;
            orderFromRepository.RequiredDate = orderToUpdate.RequiredDate;
            orderFromRepository.ShiippedDate = orderToUpdate.ShiippedDate;
            orderFromRepository.ShipVia = orderToUpdate.ShipVia;
            orderFromRepository.Freigth = orderToUpdate.Freigth;
            orderFromRepository.ShipName = orderToUpdate.ShipName;
            orderFromRepository.ShipAddress = orderToUpdate.ShipAddress;
            orderFromRepository.ShipCity = orderToUpdate.ShipCity;
            orderFromRepository.ShipRegion = orderToUpdate.ShipRegion;
            orderFromRepository.ShipPostalCode = orderToUpdate.ShipPostalCode;
            orderFromRepository.ShipCountry = orderToUpdate.ShipCountry;

            return NoContent();
        }

        [HttpDelete("{customerId}/orders/{id}")]
        public IActionResult DeleteOrder(int customerId, int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Repository.Instance.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var orderFromRepository =
                customer.Orders.FirstOrDefault(o => o.OrderId == id);
            if (orderFromRepository == null)
            {
                return NotFound();
            }
            customer.Orders.Remove(orderFromRepository);

            return NoContent();
        }
    }
}
