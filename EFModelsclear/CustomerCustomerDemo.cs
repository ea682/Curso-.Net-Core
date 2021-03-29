﻿using System;
using System.Collections.Generic;

#nullable disable

namespace NorthwindApiDemo.EFModelsclear
{
    public partial class CustomerCustomerDemo
    {
        public string CustomerId { get; set; }
        public string CustomerTypeId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerDemographic CustomerType { get; set; }
    }
}