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
    public class SubModelsController : ControllerBase
    {
        private readonly VehicleMakesDbContext _context;

        public SubModelsController(VehicleMakesDbContext context)
        {
            _context = context;
        }

        // GET: api/SubModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubModel>>> GetSubModels()
        {
            return await _context.SubModels.ToListAsync();
        }

        // GET: api/SubModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubModel>> GetSubModel(int id)
        {
            var subModel = await _context.SubModels.FindAsync(id);

            if (subModel == null)
            {
                return NotFound();
            }

            return subModel;
        }

        // PUT: api/SubModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubModel(int id, SubModel subModel)
        {
            if (id != subModel.SubModelId)
            {
                return BadRequest();
            }

            _context.Entry(subModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubModelExists(id))
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

        // POST: api/SubModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubModel>> PostSubModel(SubModel subModel)
        {
            _context.SubModels.Add(subModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubModel", new { id = subModel.SubModelId }, subModel);
        }

        // DELETE: api/SubModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubModel(int id)
        {
            var subModel = await _context.SubModels.FindAsync(id);
            if (subModel == null)
            {
                return NotFound();
            }

            _context.SubModels.Remove(subModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubModelExists(int id)
        {
            return _context.SubModels.Any(e => e.SubModelId == id);
        }
    }
}
