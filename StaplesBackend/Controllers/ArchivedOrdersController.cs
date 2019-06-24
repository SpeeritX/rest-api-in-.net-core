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
    public class ArchivedOrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArchivedOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ArchivedOrders
        [HttpGet]
        public IEnumerable<ArchivedOrder> GetArchivedOrders()
        {
            return _context.ArchivedOrders;
        }

        // GET: api/ArchivedOrders/5
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