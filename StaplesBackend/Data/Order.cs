using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaplesBackend.Data
{
    public class Order
    {
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

        [Key]
        public int ID { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int Item { get; set; }

        [Required]
        public int Amount { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }
    }
}
