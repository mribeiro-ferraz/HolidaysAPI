using HolidaysApi.Helper.Constants;
using HolidaysApi.Helper.Enums;
using HolidaysApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidaysApi.Controllers
{
    [Route("api/holidays")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly HolidaysContext _context;

        public HolidaysController(HolidaysContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All Holidays
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Holidays>>> GetAllHolidays()
        {
            return await _context.Holidays.ToListAsync();
        }

        /// <summary>
        /// Get Holidays By Date
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        [HttpGet("{month}/{year}")]
        public async Task<ActionResult<IEnumerable<Holidays>>> GetAllHolidaysByDate(int month, int year)
        {
            var holidays = await _context.Holidays.Where(f => f.Date.Year == year && f.Date.Month == month).ToListAsync();

            if (holidays == null || holidays.Count <= 0)
            {
                return NotFound();
            }

            return holidays;
        }

        /// <summary>
        /// Update Holiday
        /// </summary>
        /// <param name="id"></param>
        /// <param name="holidays"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHolidays(long id, Holidays holidays)
        {
            if (id != holidays.Id)
            {
                return BadRequest();
            }

            if (!Enum.IsDefined(typeof(HolidayType), holidays.Type))
            {
                return new NotFoundObjectResult(Errors.TypeNotFound);
            }

            _context.Entry(holidays).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HolidaysExists(id))
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

        /// <summary>
        /// Insert Holiday
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Holidays>> PostHolidays(Holidays holidays)
        {
            if (!Enum.IsDefined(typeof(HolidayType), holidays.Type))
            {
                return new NotFoundObjectResult(Errors.TypeNotFound);
            }

            _context.Holidays.Add(holidays);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllHolidays), new { id = holidays.Id }, holidays);
        }

        /// <summary>
        /// Delete Holiday
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHolidays(long id)
        {
            var holidays = await _context.Holidays.FindAsync(id);
            if (holidays == null)
            {
                return NotFound();
            }

            _context.Holidays.Remove(holidays);
            await _context.SaveChangesAsync();

            return NoContent();
        }
         
        private bool HolidaysExists(long id)
        {
            return _context.Holidays.Any(e => e.Id == id);
        }
    }
}
