using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaplesBackend.Data
{
    public class ClientOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)] // to do: set some size in every id
        public int ClientId { get; set; }
    }
}
