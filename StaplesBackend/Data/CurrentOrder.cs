using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaplesBackend.Data
{
    public class CurrentOrder:Order
    {
        public CurrentOrder():base()
        {
            Status = Status.Waiting;
        }

        public CurrentOrder(Order order) : base(order)
        {
            Status = Status.Waiting;
        }

        [Required]
        public Status Status { get; set; }
    }

    public enum Status { Waiting, Processing, Completed }
}