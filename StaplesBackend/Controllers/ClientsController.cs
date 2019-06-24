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


        // GET: api/Clients
        [HttpGet]
        public IEnumerable<Client> GetClients()
        {
            return _context.Clients;
        }


        // GET: api/Clients/5
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient([FromRoute] int id, [FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.Id)
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
        [HttpPost]
        public async Task<IActionResult> PostClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }


        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
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

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok(client);
        }


        #region ClientOrders
        //Api for orders of given client

        /// <summary>
        /// Returns all orders of the given client.
        /// </summary>
        /// <param name="id">ID of the client</param>
        /// <returns>All orders of given the client.</returns>
        [HttpGet("{id}/orders")] // GET: api/Clients/5/orders
        public async Task<IActionResult> GetOrdersOfClientAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await _context.Clients.Include(c => c.Orders).SingleAsync(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client.Orders);
        }


        /// <summary>
        /// Returns all archived orders of the given client.
        /// </summary>
        /// <param name="id">ID of the client</param>
        /// <returns>All archived orders of given the client.</returns>
        [HttpGet("{id}/archivedorders")] // GET: api/Clients/5/archivedorders
        public async Task<IActionResult> GetArchivedOrdersOfClientAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await _context.Clients.Include(c => c.ArchivedOrders).SingleAsync(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client.ArchivedOrders);
        }


        /// <summary>
        /// Adds order to given client.
        /// </summary>
        /// <param name="id">ID of client making the order.</param>
        /// <param name="order">Order to add.</param>
        /// <returns>No content.</returns>
        [HttpPost("{id}/addorder")] // POST: api/Clients/5/addorder
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
            _context.SaveChanges();

            return NoContent();
        }


        /// <summary>
        /// Deletes order with given id.
        /// </summary>
        /// <param name="clientId">Id of the client.</param>
        /// <param name="orderId">Id of the order to delete.</param>
        /// <returns>Return deleted order.</returns>
        [HttpDelete("{clientId}/orders/{orderId}")] // DELETE: api/Clients/5/orders/3
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
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}