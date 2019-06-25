using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StaplesBackend.Data
{
    public class Client
    {
        #region Public Properties

        [Key]
        public int ID { get; set; }

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
        [MaxLength(256)]
        public string Email { get; set; }

        public int Phone { get; set; }

        public IList<CurrentOrder> Orders { get; set; }

        public IList<ArchivedOrder> ArchivedOrders { get; set; }

        #endregion
    }

    public class ClientQuery
    {
        #region Public Properties

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }

        #endregion

        /// <summary>
        /// Checks if query matches the client.
        /// </summary>
        /// <param name="client">Client to check against query.</param>
        /// <returns>True if client matches query. Else false</returns>
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

