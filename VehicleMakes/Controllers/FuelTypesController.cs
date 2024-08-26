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
    public class FuelTypesController : ControllerBase
    {
        private readonly VehicleMakesDbContext _context;

        public FuelTypesController(VehicleMakesDbContext context)
        {
            _context = context;
        }

        // GET: api/FuelTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelType>>> GetFuelTypes()
        {
            return await _context.FuelTypes.ToListAsync();
        }

        // GET: api/FuelTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelType>> GetFuelType(int id)
        {
            var fuelType = await _context.FuelTypes.FindAsync(id);

            if (fuelType == null)
            {
                return NotFound();
            }

            return fuelType;
        }

        // PUT: api/FuelTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuelType(int id, FuelType fuelType)
        {
            if (id != fuelType.FuelTypeId)
            {
                return BadRequest();
            }

            _context.Entry(fuelType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuelTypeExists(id))
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

        // POST: api/FuelTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FuelType>> PostFuelType(FuelType fuelType)
        {
            _context.FuelTypes.Add(fuelType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuelType", new { id = fuelType.FuelTypeId }, fuelType);
        }

        // DELETE: api/FuelTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuelType(int id)
        {
            var fuelType = await _context.FuelTypes.FindAsync(id);
            if (fuelType == null)
            {
                return NotFound();
            }

            _context.FuelTypes.Remove(fuelType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuelTypeExists(int id)
        {
            return _context.FuelTypes.Any(e => e.FuelTypeId == id);
        }
    }
}
