﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Identity;

namespace HospitalAppointmentSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HospitalDataContext _context;
        private readonly UserManager<User> _userManager;

        public AppointmentsController(HospitalDataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
              return _context.appointments != null ? 
                          View(await _context.appointments.ToListAsync()) :
                          Problem("Entity set 'HospitalDataContext.appointments'  is null.");
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointments
                .FirstOrDefaultAsync(m => m.appointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewBag.Doctors = _context.doctors.Select(i => new SelectListItem
            {
                Text = i.doctorName,
                Value = i.doctorId.ToString(),
            });
            ViewBag.TimeRanges = new List<SelectListItem>
            {
                new SelectListItem { Text = "9-10", Value = "9-10" },
                new SelectListItem { Text = "10-11", Value = "10-11" },
                new SelectListItem { Text = "11-12", Value = "11-12" },
                new SelectListItem { Text = "12-13", Value = "12-13" },
                new SelectListItem { Text = "13-14", Value = "13-14" }
            };
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("appointmentId,appointmentDate,appointmentTime,doctorId")] AppointmentView appointmentView)
        {
            if (ModelState.IsValid) 
            {
                var doctorx = _context.doctors.FirstOrDefault(i=>appointmentView.doctorId == i.doctorId);
                var userx = await _userManager.GetUserAsync(User);
                var appointment = new Appointment
                {
                    user = userx,
                    doctor = doctorx,
                    appointmentDate = appointmentView.appointmentDate,
                    appointmentTime = appointmentView.appointmentTime,
                };
                _context.appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("appointmentId,appointmentDate,appointmentTime")] Appointment appointment)
        {
            if (id != appointment.appointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.appointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointments
                .FirstOrDefaultAsync(m => m.appointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.appointments == null)
            {
                return Problem("Entity set 'HospitalDataContext.appointments'  is null.");
            }
            var appointment = await _context.appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> testView()
        {
            return View();
        }
        private bool AppointmentExists(int id)
        {
          return (_context.appointments?.Any(e => e.appointmentId == id)).GetValueOrDefault();
        }
    }
}
