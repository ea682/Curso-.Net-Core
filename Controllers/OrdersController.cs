using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NorthwindApiDemo.Models;
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

        [HttpGet("{customerId}/orders/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int customerId, int id)
        {
            var customer = Repository.Instance.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var order = customer.Orders.FirstOrDefault(c => c.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpPost("{customerId}/orders")]
        public IActionResult CreateOrder(int customerId, [FromBody] OrdersForCreationDTO order)
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

            var maxOrderId =
                Repository.Instance.Customers
                .SelectMany(c => c.Orders)
                .Max(o => o.OrderId);

            var finalOrder = new OrdersDTO()
            {
                OrderId = maxOrderId++,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShiippedDate = order.ShiippedDate,
                ShipVia = order.ShipVia,
                Freigth = order.Freigth,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry
            };

            customer.Orders.Add(finalOrder);

            return CreatedAtRoute("GetOrder",
                new { customerId = customer.Id,
                    id = finalOrder.OrderId },
                finalOrder
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
