using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleMakes;
using VehicleMakes.authorization;
using VehicleMakes.Model.Model;

namespace VehicleMakes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodiesController : ControllerBase
    {
        private readonly VehicleMakesDbContext _context;

        public BodiesController(VehicleMakesDbContext context)
        {
            _context = context;
        }

        // GET: api/Bodies
        [HttpGet]
      //  [CheckPermission(Permission.Read)]
        public async Task<ActionResult<IEnumerable<Body>>> GetBodies()
        {
            return await _context.Bodies.Include(b => b.VehicleDetails).ToListAsync();
        }

        // GET: api/Bodies/5
        [HttpGet("{id}")]
       // [CheckPermission(Permission.Read)]

        public async Task<ActionResult<Body>> GetBody(int id)
        {
            var body = await _context.Bodies.FindAsync(id);

            if (body == null)
            {
                return NotFound();
            }

            return body;
        }

        // PUT: api/Bodies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBody(int id, Body body)
        {
            if (id != body.BodyId)
            {
                return BadRequest();
            }

            _context.Entry(body).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodyExists(id))
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

        // POST: api/Bodies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Body>> PostBody(Body body)
        {
            _context.Bodies.Add(body);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBody", new { id = body.BodyId }, body);
        }

        // DELETE: api/Bodies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBody(int id)
        {
            var body = await _context.Bodies.FindAsync(id);
            if (body == null)
            {
                return NotFound();
            }

            _context.Bodies.Remove(body);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BodyExists(int id)
        {
            return _context.Bodies.Any(e => e.BodyId == id);
        }
    }
}
