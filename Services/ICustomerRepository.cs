using NorthwindApiDemo.EFModelsclear;
using NorthwindApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApiDemo.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomers(string customerId, bool includeOrdes);
        IEnumerable<Order> GetOrdes(string customerId);
        Order GetOrder(string customerId, int orderId);

        bool CustumerExists(string customerId);

        void AddOrder(string customerId, Order order);
        bool Save();
    }
}
