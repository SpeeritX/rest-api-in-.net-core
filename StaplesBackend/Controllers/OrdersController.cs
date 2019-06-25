using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StaplesBackend.Data;

namespace StaplesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/Orders
        /// <summary>
        /// Returns all orders.
        /// </summary>
        /// <returns>All orders.</returns>
        [HttpGet] 
        public IEnumerable<CurrentOrder> GetOrders()
        {
            return _context.CurrentOrders;
        }


        // GET: api/Orders/5
        /// <summary>
        /// Returns order with the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order with the given ID</returns>
        [HttpGet("{id}")] 
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.CurrentOrders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        // POST: api/Orders
        /// <summary>
        /// Creates new order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Created order.</returns>
        [HttpPost] 
        public async Task<IActionResult> PostOrder([FromBody] CurrentOrder order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CurrentOrders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.ID }, order);
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Deletes order with the given ID. Copy of the order is saved as archived order.
        /// </summary>
        /// <param name="id">>ID of the order to delete.</param>
        /// <returns>Deleted order.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.CurrentOrders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ArchivedOrder archivedOrder = new ArchivedOrder(order);

            _context.ArchivedOrders.Add(archivedOrder);
            _context.CurrentOrders.Remove(order);

            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}