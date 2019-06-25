using System;
using System.ComponentModel.DataAnnotations;

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
