using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShowRoom.Db;
using CarShowRoom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarShowRoom.Controllers
{
    public class DepotOperationsController : Controller
    {
        private readonly CRMContext _context;

        public DepotOperationsController(CRMContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var carsQuery = _context.Cars
                .Include(c => c.CarModel)
                .Include(c => c.CarModel.Vendor)
                .Include(c => c.Client)
                .Include(c => c.Color)
                .Include(c => c.Depot)
                .Include(c => c.Partner)
                .Where(x => !x.Sold).AsNoTracking();

            ViewBag.Cars = await carsQuery.ToListAsync();

            return View();
        }

        // GET: Cars/Create
        public IActionResult CreateCar()
        {
            ViewData["CarModelId"] = new SelectList(_context.CarModels.Include(c => c.Vendor).AsNoTracking(), "Id", null);
            ViewData["ColorId"] = new SelectList(_context.CarColors, "Id", "Name");
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCar([Bind("Id,VIN,Year,Price,ColorId,TestDrive,CarModelId,PartnerId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarModelId"] = new SelectList(_context.CarModels.Include(c => c.Vendor).AsNoTracking(), "Id", null);
            ViewData["ColorId"] = new SelectList(_context.CarColors, "Id", "Name", car.ColorId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Name", car.PartnerId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> EditCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.SingleOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarModelId"] = new SelectList(_context.CarModels.Include(c => c.Vendor).AsNoTracking(), "Id", null);
            ViewData["ColorId"] = new SelectList(_context.CarColors, "Id", "Name", car.ColorId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Name", car.PartnerId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCar(int id, [Bind("Id,VIN,Year,Price,ColorId,TestDrive,CarModelId,PartnerId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }
            try
            {
                var existedCar = await _context.Cars.SingleOrDefaultAsync(x => x.Id == id);

                if (ModelState.IsValid)
                {
                    existedCar.VIN = car.VIN;
                    existedCar.Year = car.Year;
                    existedCar.Price = car.Price;
                    existedCar.ColorId = car.ColorId;
                    existedCar.TestDrive = car.TestDrive;
                    existedCar.CarModelId = car.CarModelId;
                    existedCar.PartnerId = car.PartnerId;

                    _context.Update(existedCar);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                ViewData["CarModelId"] = new SelectList(_context.CarModels.Include(c => c.Vendor).AsNoTracking(), "Id", null);
                ViewData["ColorId"] = new SelectList(_context.CarColors, "Id", "Name", car.ColorId);
                ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Name", car.PartnerId);
                return View(car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> DeleteCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarModel)
                .Include(c => c.CarModel.Vendor)
                .Include(c => c.Color)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("DeleteCar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCarConfirmed(int id)
        {
            var car = await _context.Cars.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> AssignCarDepot(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarModel)
                .Include(c => c.CarModel.Vendor)
                .Include(c => c.Partner)
                .Include(c => c.Color).SingleOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            ViewData["DepotId"] = new SelectList(_context.Depots, "Id", "Name", car.DepotId);

            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignCarDepot(int id, [Bind("Id,DepotId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            try
            {
                var existedCar = await _context.Cars.SingleOrDefaultAsync(x => x.Id == id);

                existedCar.DepotId = car.DepotId;
                _context.Update(existedCar);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}