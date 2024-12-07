using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlamourTime.Domain.Entities;
using GlamourTime.Infrastructure.Contexts;

namespace GlamourTime.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly GlamourTimeDbContext _context;

        public AppointmentsController(GlamourTimeDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var glamourTimeDbContext = _context.Appointments.Include(a => a.Client).Include(a => a.Service).Include(a => a.Stylist);
            return View(await glamourTimeDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Service)
                .Include(a => a.Stylist)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstName");
            ViewData["ServiceId"] = new SelectList(_context.SalonServices, "ServiceId", "ServiceName");
            ViewData["StylistId"] = new SelectList(_context.Stylists, "StylistId", "FirstName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,ClientId,StylistId,ServiceId,AppointmentDate,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstName", appointment.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.SalonServices, "ServiceId", "ServiceName", appointment.ServiceId);
            ViewData["StylistId"] = new SelectList(_context.Stylists, "StylistId", "FirstName", appointment.StylistId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstName", appointment.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.SalonServices, "ServiceId", "ServiceName", appointment.ServiceId);
            ViewData["StylistId"] = new SelectList(_context.Stylists, "StylistId", "FirstName", appointment.StylistId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,ClientId,StylistId,ServiceId,AppointmentDate,Status")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
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
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstName", appointment.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.SalonServices, "ServiceId", "ServiceName", appointment.ServiceId);
            ViewData["StylistId"] = new SelectList(_context.Stylists, "StylistId", "FirstName", appointment.StylistId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Service)
                .Include(a => a.Stylist)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
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
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
