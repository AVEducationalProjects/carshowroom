using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarShowRoom.Db;
using CarShowRoom.Models;

namespace CarShowRoom.Controllers
{
    public class TestDrivesController : Controller
    {
        private readonly CRMContext _context;

        public TestDrivesController(CRMContext context)
        {
            _context = context;
        }

        // GET: TestDrives
        public async Task<IActionResult> Index()
        {
            var testDrives = _context.TestDrives
                .Where(t=>t.DateTime>= DateTime.Now.Date || !t.Complete)
                .Include(t => t.Car).ThenInclude(x=>x.CarModel).ThenInclude(x=>x.Vendor)
                .Include(x=>x.Car).ThenInclude(x=>x.Color)
                .Include(t => t.Client).OrderBy(x=>x.DateTime);

            return View(await testDrives.ToListAsync());
        }

        // GET: TestDrives/Create
        public async Task<IActionResult> Create()
        {
            var cars = await _context.Cars
                .Where(x=>x.TestDrive)
                .Include(x => x.CarModel).ThenInclude(x => x.Vendor)
                .Include(x => x.Color).AsNoTracking().ToListAsync();
            var clients = await _context.Clients.AsNoTracking().ToListAsync();
            ViewData["Cars"] = cars;
            ViewData["Clients"] = clients;
            return View();
        }

        // POST: TestDrives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,ClientId,CarId")] TestDrive testDrive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testDrive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var cars = await _context.Cars
                .Where(x => x.TestDrive)
                .Include(x => x.CarModel).ThenInclude(x => x.Vendor)
                .Include(x => x.Color).AsNoTracking().ToListAsync();
            var clients = await _context.Clients.AsNoTracking().ToListAsync();
            ViewData["Cars"] = cars;
            ViewData["Clients"] = clients;

            return View(testDrive);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDrive = await _context.TestDrives.Include(x=>x.Client)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (testDrive == null)
            {
                return NotFound();
            }

            testDrive.Complete = true;
            testDrive.Client.Stage = Stage.Decision;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: TestDrives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDrive = await _context.TestDrives
                .Include(t => t.Car)
                .Include(t => t.Client)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (testDrive == null)
            {
                return NotFound();
            }

            return View(testDrive);
        }

        // POST: TestDrives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testDrive = await _context.TestDrives.SingleOrDefaultAsync(m => m.Id == id);
            _context.TestDrives.Remove(testDrive);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestDriveExists(int id)
        {
            return _context.TestDrives.Any(e => e.Id == id);
        }
    }
}
