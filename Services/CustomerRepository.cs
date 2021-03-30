using Microsoft.EntityFrameworkCore;
using NorthwindApiDemo.EFModelsclear;
using NorthwindApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApiDemo.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private NorthwindContext _context;

        public CustomerRepository(NorthwindContext context)
        {
            _context = context;
        }

        public void AddOrder(string customerId, Order order)
        {
            var customer =
                GetCustomers(customerId, false);
            customer.Orders.Add(order);
        }
        public bool CustumerExists(string customerId)
        {
            return
                _context.Customers.Any(c => c.CustomerId == customerId);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return 
                _context.Customers.OrderBy(c => c.CompanyName)
                .ToList();
        }

        public Customer GetCustomers(string customerId, bool includeOrdes)
        {
            if (includeOrdes)
            {
                return _context.Customers
                    .Include(c => c.Orders)
                    .Where(c => c.CustomerId == customerId)
                    .FirstOrDefault();
            }

            return _context.Customers
                .Where(c => c.CustomerId == customerId)
                .FirstOrDefault();
        }

        public Order GetOrder(string customerId, int orderId)
        {
            return _context.Orders
                .Where(c => c.CustomerId == customerId && c.OrderId == orderId)
                .FirstOrDefault();
        }

        public IEnumerable<Order> GetOrdes(string customerId)
        {
            return _context.Orders
                .Where(c => c.CustomerId == customerId)
                .ToList();
        }

        public bool Save()
        {
            return(_context.SaveChanges() >= 0);
        }
    }
}
