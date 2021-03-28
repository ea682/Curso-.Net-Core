using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApiDemo.Models
{
    public class OrdersForUpdateDto
    {
        [Required]
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShiippedDate { get; set; }
        public int ShipVia { get; set; }
        public decimal Freigth { get; set; }
        public string ShipName { get; set; }
        [MinLength(10, ErrorMessage = "ShipAddress too Short")]
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
    }
}
