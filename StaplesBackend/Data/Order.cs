using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaplesBackend.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required]
        public string Value { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }
    }
}
