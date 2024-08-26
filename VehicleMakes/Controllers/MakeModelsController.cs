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
    public class MakeModelsController : ControllerBase
    {
        private readonly VehicleMakesDbContext _context;

        public MakeModelsController(VehicleMakesDbContext context)
        {
            _context = context;
        }

        // GET: api/MakeModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MakeModel>>> GetMakeModels()
        {
            return await _context.MakeModels.ToListAsync();
        }

        // GET: api/MakeModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MakeModel>> GetMakeModel(int id)
        {
            var makeModel = await _context.MakeModels.FindAsync(id);

            if (makeModel == null)
            {
                return NotFound();
            }

            return makeModel;
        }

        // PUT: api/MakeModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMakeModel(int id, MakeModel makeModel)
        {
            if (id != makeModel.ModelId)
            {
                return BadRequest();
            }

            _context.Entry(makeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakeModelExists(id))
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

        // POST: api/MakeModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MakeModel>> PostMakeModel(MakeModel makeModel)
        {
            _context.MakeModels.Add(makeModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMakeModel", new { id = makeModel.ModelId }, makeModel);
        }

        // DELETE: api/MakeModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMakeModel(int id)
        {
            var makeModel = await _context.MakeModels.FindAsync(id);
            if (makeModel == null)
            {
                return NotFound();
            }

            _context.MakeModels.Remove(makeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MakeModelExists(int id)
        {
            return _context.MakeModels.Any(e => e.ModelId == id);
        }
    }
}
