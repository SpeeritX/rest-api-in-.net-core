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

    public class ClientQuery
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }

        public bool Match(Client client)
        {
            if (Login != null && client.Login != null 
                && !string.Equals(Login, client.Login, StringComparison.OrdinalIgnoreCase))
                return false;
            if (FirstName != null && client.FirstName != null 
                && !string.Equals(FirstName, client.FirstName, StringComparison.OrdinalIgnoreCase))
                return false;
            if (LastName != null && client.LastName != null
                && !string.Equals(LastName, client.LastName, StringComparison.OrdinalIgnoreCase))
                return false;
            if (Email != null && client.Email != null 
                && !string.Equals(Email, client.Email, StringComparison.OrdinalIgnoreCase))
                return false;
            if (Phone != 0 && client.Phone != 0 && Phone != client.Phone)
                return false;
            return true;
        }
    }
}

