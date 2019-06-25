using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StaplesBackend.Data;

namespace StaplesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivedOrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArchivedOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ArchivedOrders
        /// <summary>
        /// Returns all archived orders.
        /// </summary>
        /// <returns>All archived orders.</returns>
        [HttpGet]
        public IEnumerable<ArchivedOrder> GetArchivedOrders()
        {
            return _context.ArchivedOrders;
        }

        // GET: api/ArchivedOrders/5
        /// <summary>
        /// Returns archived order with the given ID.
        /// </summary>
        /// <param name="id">ID of archived order.</param>
        /// <returns>Archived order with the given ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArchivedOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var archivedOrder = await _context.ArchivedOrders.FindAsync(id);

            if (archivedOrder == null)
            {
                return NotFound();
            }

            return Ok(archivedOrder);
        }
    }
}