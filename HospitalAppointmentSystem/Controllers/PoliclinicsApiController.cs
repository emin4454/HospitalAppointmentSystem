﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliclinicsApiController : ControllerBase
    {
        private readonly HospitalDataContext _context;

        public PoliclinicsApiController(HospitalDataContext context)
        {
            _context = context;
        }

        // GET: api/PoliclinicsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policlinic>>> Getpoliclinics()
        {
          if (_context.policlinics == null)
          {
              return NotFound();
          }
            var result = await _context.policlinics
                .Select(p => new { p.policlinicId, p.policlinicName })
                .ToListAsync();
            return Ok(result);
        }

        // GET: api/PoliclinicsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Policlinic>> GetPoliclinic(int id)
        {
          if (_context.policlinics == null)
          {
              return NotFound();
          }
            var policlinic = await _context.policlinics.FindAsync(id);

            if (policlinic == null)
            {
                return NotFound();
            }

            return policlinic;
        }

        // PUT: api/PoliclinicsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoliclinic(int id, Policlinic policlinic)
        {
            if (id != policlinic.policlinicId)
            {
                return BadRequest();
            }

            _context.Entry(policlinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliclinicExists(id))
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

        // POST: api/PoliclinicsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Policlinic>> PostPoliclinic(Policlinic policlinic)
        {
          if (_context.policlinics == null)
          {
              return Problem("Entity set 'HospitalDataContext.policlinics'  is null.");
          }
            _context.policlinics.Add(policlinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoliclinic", new { id = policlinic.policlinicId }, policlinic);
        }

        // DELETE: api/PoliclinicsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoliclinic(int id)
        {
            if (_context.policlinics == null)
            {
                return NotFound();
            }
            var policlinic = await _context.policlinics.FindAsync(id);
            if (policlinic == null)
            {
                return NotFound();
            }

            _context.policlinics.Remove(policlinic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PoliclinicExists(int id)
        {
            return (_context.policlinics?.Any(e => e.policlinicId == id)).GetValueOrDefault();
        }
    }
}
