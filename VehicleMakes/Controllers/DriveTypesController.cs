using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleMakes;

namespace VehicleMakes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriveTypesController : ControllerBase
    {
        private readonly VehicleMakesDbContext _context;

        public DriveTypesController(VehicleMakesDbContext context)
        {
            _context = context;
        }

        // GET: api/DriveTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriveType>>> GetDriveTypes()
        {
            return await _context.DriveTypes.ToListAsync();
        }

        // GET: api/DriveTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DriveType>> GetDriveType(int id)
        {
            var driveType = await _context.DriveTypes.FindAsync(id);

            if (driveType == null)
            {
                return NotFound();
            }

            return driveType;
        }

        // PUT: api/DriveTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriveType(int id, DriveType driveType)
        {
            if (id != driveType.DriveTypeId)
            {
                return BadRequest();
            }

            _context.Entry(driveType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriveTypeExists(id))
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

        // POST: api/DriveTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DriveType>> PostDriveType(DriveType driveType)
        {
            _context.DriveTypes.Add(driveType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriveType", new { id = driveType.DriveTypeId }, driveType);
        }

        // DELETE: api/DriveTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriveType(int id)
        {
            var driveType = await _context.DriveTypes.FindAsync(id);
            if (driveType == null)
            {
                return NotFound();
            }

            _context.DriveTypes.Remove(driveType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriveTypeExists(int id)
        {
            return _context.DriveTypes.Any(e => e.DriveTypeId == id);
        }
    }
}
