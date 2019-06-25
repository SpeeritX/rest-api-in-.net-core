using System;
using System.ComponentModel.DataAnnotations;


namespace StaplesBackend.Data
{
    public class Order
    {
        #region Public Properties

        [Key]
        public int ID { get; set; }

        [Required]
        public int? ClientId { get; set; }

        [Required]
        public int? Item { get; set; }

        [Required]
        public int? Amount { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }

        #endregion

        #region Constructors

        public Order()
        {
            OrderTime = DateTime.Now;
        }

        public Order(Order order)
        {
            ClientId = order.ClientId;
            Item = order.Item;
            Amount = order.Amount;
            OrderTime = order.OrderTime;
        }

        #endregion
    }
}
