using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace StaffAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly StaffContext _context;

        public StaffsController(StaffContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Staff request)
        {

            if (request.Gender != 1 && request.Gender != 2)
            {
                return BadRequest("Invalid gender");
            }

            var newStaff = new Staff
            {
                StaffId = request.StaffId,
                FullName = request.FullName,
                BirthDay = request.BirthDay,
                Gender = request.Gender,
            };

            _context.Staff.Add(newStaff);
            await _context.SaveChangesAsync();

            return Ok(newStaff);
        }

        [HttpDelete("{StaffId}")]
        public IActionResult Delete(string StaffId)
        {
            var staff = _context.Staff.FirstOrDefault(x => x.StaffId == StaffId);

            if(staff != null)
            {
                _context.Staff.Remove(staff);
                _context.SaveChanges();
                return Ok("Deleted");
            }

            return NotFound("Not found");
        }

        [HttpGet("{StaffId}")]
        public async Task<IActionResult> Get(string StaffId)
        {
            var staff = await _context.Staff.FindAsync(StaffId);

            if(staff != null)
            {
                return Ok(staff);
            }

            return NotFound("Not found");
        }

        [HttpGet]
        public async Task<IActionResult> List(string? staffId, int? gender, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Staff.AsQueryable();

            if (!string.IsNullOrEmpty(staffId))
            {
                query = query.Where(s => s.StaffId == staffId);
            }

            if (gender.HasValue)
            {
                query = query.Where(s => s.Gender == gender.Value);
            }

            if (fromDate.HasValue && toDate.HasValue)
            {
                query = query.Where(s => s.BirthDay >= fromDate.Value && s.BirthDay <= toDate.Value);
            }

            var data = await query.ToListAsync();

            return Ok(data);
        }

       
        [HttpPut("{SaffId}")]
        public async Task<IActionResult> Update(string SaffId, [FromBody] StaffUpdate request)
        {
            if (request.Gender != 1 && request.Gender != 2)
            {
                return BadRequest("Invalid gender");
            }

            var data = await _context.Staff.FindAsync(SaffId);
            if(data != null)
            {
                data.FullName = request.FullName;
                data.BirthDay = request.BirthDay;
                data.Gender = request.Gender;
                _context.Staff.Update(data);
                await _context.SaveChangesAsync();

                return Ok(data);    
            }

            return NotFound("Not Found");   
        }



    }
}
