using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaplesBackend.Data
{
    public class ArchivedOrder:Order
    {
        #region Public Properties

        [DataType(DataType.DateTime)]
        public DateTime ArchivingDate { get; set; }

        #endregion

        #region Constructors

        public ArchivedOrder() : base()
        {
            Initiate();
        }
        public ArchivedOrder(Order order) : base(order)
        {
            Initiate();
        }

        #endregion

        private void Initiate()
        {
            ArchivingDate = DateTime.Now;
        }
    }
}
