using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaplesBackend.Data
{
    public class ArchivedOrder:Order
    {
        public ArchivedOrder() : base()
        {
            ArchivingDate = DateTime.Now;
        }

        public ArchivedOrder(Order order) : base(order)
        {
            ArchivingDate = DateTime.Now;
        }

        [DataType(DataType.DateTime)]
        public DateTime ArchivingDate { get; set; }
    }
}
