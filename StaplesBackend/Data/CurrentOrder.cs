using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaplesBackend.Data
{
    public class CurrentOrder:Order
    {
        #region Public Properties

        [Required]
        public Status Status { get; set; }

        #endregion

        #region Constructors

        public CurrentOrder() : base()
        {
            Initiate();
        }

        public CurrentOrder(Order order) : base(order)
        {
            Initiate();
        }

        #endregion


        private void Initiate()
        {
            Status = Status.Waiting;
        }
    }

    public enum Status { Waiting, Processing, Completed }
}