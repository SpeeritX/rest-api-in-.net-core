using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaplesBackend.Data
{
    public class Client
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Login { get; set; }

        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        public int Phone { get; set; }

        public IList<CurrentOrder> Orders { get; set; }

        public IList<ArchivedOrder> ArchivedOrders { get; set; }
    }
}

