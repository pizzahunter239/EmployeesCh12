using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesCh12.Models;

namespace EmployeesCh12.Controllers
{
    public class DepartmentLocationsController : Controller
    {
        private readonly EmployeeContext _context;

        public DepartmentLocationsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: DepartmentLocations
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.DepartmentLocations.Include(d => d.Department).Include(d => d.Location);
            return View(await employeeContext.ToListAsync());
        }

        // GET: DepartmentLocations/Details/5
        public async Task<IActionResult> Details(int? DepartmentID, int? LocationID)
        {
            if (DepartmentID == null || _context.DepartmentLocations == null || LocationID == null)
            {
                return NotFound();
            }

            var departmentLocation = await _context.DepartmentLocations
                .Include(d => d.Department)
                .Include(d => d.Location)
                .FirstOrDefaultAsync(m => m.DepartmentID == DepartmentID && m.LocationID == LocationID);
            if (departmentLocation == null)
            {
                return NotFound();
            }

            return View(departmentLocation);
        }

        // GET: DepartmentLocations/Create
        public IActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(
                _context.Departments
                .Select(d => new { d.ID, DisplayName = d.ID + " - " + d.Name }),
                "ID",
                "DisplayName"
            );
            ViewBag.LocationID = new SelectList(
                _context.Locations
                .Select(l => new { l.ID, DisplayName = l.ID + " - " + l.Address }),
                "ID",
                "DisplayName"
            );
            return View();
        }

        // POST: DepartmentLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentID,LocationID")] DepartmentLocation departmentLocation)
        {
            bool exists = await _context.DepartmentLocations
                .AnyAsync(dl => dl.DepartmentID == departmentLocation.DepartmentID
                            && dl.LocationID == departmentLocation.LocationID);
            if (exists)
            {
                ModelState.AddModelError(string.Empty, "This department is already assigned to the selected Location.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(departmentLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DepartmentID = new SelectList(
                _context.Departments
                .Select(d => new { d.ID, DisplayName = d.ID + " - " + d.Name }),
                "ID",
                "DisplayName"
            );
            ViewBag.LocationID = new SelectList(
                _context.Locations
                .Select(l => new { l.ID, DisplayName = l.ID + " - " + l.Address }),
                "ID",
                "DisplayName"
            );
            return View(departmentLocation);
        }



        // GET: DepartmentLocations/Delete/5
        public async Task<IActionResult> Delete(int? DepartmentID, int? LocationID)
        {
            if (DepartmentID == null || _context.DepartmentLocations == null || LocationID == null)
            {
                return NotFound();
            }

            var departmentLocation = await _context.DepartmentLocations
                .Include(d => d.Department)
                .Include(d => d.Location)
                .FirstOrDefaultAsync(m => m.DepartmentID == DepartmentID && m.LocationID == LocationID);
            if (departmentLocation == null)
            {
                return NotFound();
            }

            return View(departmentLocation);
        }

        // POST: DepartmentLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int DepartmentID, int LocationID)
        {
            if (_context.DepartmentLocations == null)
            {
                return Problem("Entity set 'EmployeeContext.DepartmentLocations' is null.");
            }
            var departmentLocation = await _context.DepartmentLocations.FindAsync(DepartmentID, LocationID);
            if (departmentLocation != null)
            {
                _context.DepartmentLocations.Remove(departmentLocation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentLocationExists(int id)
        {
            return _context.DepartmentLocations.Any(e => e.DepartmentID == id);
        }
    }
}
