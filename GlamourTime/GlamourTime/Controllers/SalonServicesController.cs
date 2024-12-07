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
    public class SalonServicesController : Controller
    {
        private readonly GlamourTimeDbContext _context;

        public SalonServicesController(GlamourTimeDbContext context)
        {
            _context = context;
        }

        // GET: SalonServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalonServices.ToListAsync());
        }

        // GET: SalonServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salonService = await _context.SalonServices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (salonService == null)
            {
                return NotFound();
            }

            return View(salonService);
        }

        // GET: SalonServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalonServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,ServiceName,ServiceDescription,Price")] SalonService salonService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salonService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salonService);
        }

        // GET: SalonServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salonService = await _context.SalonServices.FindAsync(id);
            if (salonService == null)
            {
                return NotFound();
            }
            return View(salonService);
        }

        // POST: SalonServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,ServiceName,ServiceDescription,Price")] SalonService salonService)
        {
            if (id != salonService.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salonService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonServiceExists(salonService.ServiceId))
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
            return View(salonService);
        }

        // GET: SalonServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salonService = await _context.SalonServices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (salonService == null)
            {
                return NotFound();
            }

            return View(salonService);
        }

        // POST: SalonServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salonService = await _context.SalonServices.FindAsync(id);
            if (salonService != null)
            {
                _context.SalonServices.Remove(salonService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalonServiceExists(int id)
        {
            return _context.SalonServices.Any(e => e.ServiceId == id);
        }
    }
}
