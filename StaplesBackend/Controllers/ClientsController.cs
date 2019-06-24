using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaplesBackend.Data;

namespace StaplesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Client Api

        // GET: api/Clients
        /// <summary>
        /// Returns all clients.
        /// </summary>
        /// <returns>All clients.</returns>
        [HttpGet] 
        public IEnumerable<Client> GetClients()
        {
            return _context.Clients;
        }


        // GET: api/Clients/5
        /// <summary>
        /// Returns client with the given ID.
        /// </summary>
        /// <param name="id">ID of the client.</param>
        /// <returns>Client with the given ID.</returns>
        [HttpGet("{id}")] 
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }


        // GET: api/Clients/search?login=somelogin&firstname=john
        /// <summary>
        /// Searches clients for those who match the query.
        /// </summary>
        /// <param name="query">Searching pattern.</param>
        /// <returns>Clients fitting the given query.</returns>
        [HttpGet("search")] 
        public IActionResult SearchClients([FromQuery] ClientQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clients = _context.Clients.Where(c => query.Match(c));
            if (clients == null)
            {
                return NotFound();
            }

            return Ok(clients);
        }


        // PUT: api/Clients/5
        /// <summary>
        /// Updates data of the given client.
        /// </summary>
        /// <param name="id">ID of the client to update.</param>
        /// <param name="client">Client with new data. ID is required.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")] 
        public async Task<IActionResult> PutClient([FromRoute] int id, [FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ID)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Clients
        /// <summary>
        /// Creates new client.
        /// </summary>
        /// <param name="client">Client object containing all data.</param>
        /// <returns>Created client.</returns>
        [HttpPost] 
        public async Task<IActionResult> PostClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.ID }, client);
        }

        #endregion

        #region ClientOrders Api
        //Api for orders of the given client

        // GET: api/Clients/5/orders
        /// <summary>
        /// Returns all orders of the given client.
        /// </summary>
        /// <param name="id">ID of the client</param>
        /// <returns>All orders of the given client.</returns>
        [HttpGet("{id}/orders")] 
        public async Task<IActionResult> GetOrdersOfClientAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await _context.Clients.Include(c => c.Orders).SingleAsync(c => c.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client.Orders);
        }


        // GET: api/Clients/5/archivedorders
        /// <summary>
        /// Returns all archived orders of the given client.
        /// </summary>
        /// <param name="id">ID of the client</param>
        /// <returns>All archived orders of the given client.</returns>
        [HttpGet("{id}/archivedorders")]
        public async Task<IActionResult> GetArchivedOrdersOfClientAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await _context.Clients.Include(c => c.ArchivedOrders).SingleAsync(c => c.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client.ArchivedOrders);
        }


        // POST: api/Clients/5/addorder
        /// <summary>
        /// Adds order to the given client.
        /// </summary>
        /// <param name="id">ID of the client making the order.</param>
        /// <param name="order">Order to add.</param>
        /// <returns>No content.</returns>
        [HttpPost("{id}/addorder")] 
        public async Task<IActionResult> AddOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ClientExists(id))
            {
                return NotFound();
            }

            order.ClientId = id;
            var newOrder = new CurrentOrder(order);

            _context.CurrentOrders.Add(newOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Clients/5/orders/3
        /// <summary>
        /// Deletes order with the given ID. Copy of the order is saved as archived order.
        /// </summary>
        /// <param name="clientId">ID of the client.</param>
        /// <param name="orderId">ID of the order to delete.</param>
        /// <returns>Deleted order.</returns>
        [HttpDelete("{clientId}/orders/{orderId}")] 
        public async Task<IActionResult> DeleteOrder([FromRoute] int clientId, [FromRoute] int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.CurrentOrders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            if (order.ClientId != clientId)
            {
                return BadRequest();
            }
            ArchivedOrder archivedOrder = new ArchivedOrder(order);

            _context.ArchivedOrders.Add(archivedOrder);
            _context.CurrentOrders.Remove(order);

            await _context.SaveChangesAsync();

            return Ok(order);
        }

        #endregion


        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }
    }
}