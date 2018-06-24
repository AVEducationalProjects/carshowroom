using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarShowRoom.Db;
using CarShowRoom.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarShowRoom.Controllers
{
    [Authorize(Roles = "admin")]
    public class CarModelsController : Controller
    {
        private readonly CRMContext _context;

        public CarModelsController(CRMContext context)
        {
            _context = context;
        }

        // GET: CarModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarModels.Include(x=>x.Vendor).AsNoTracking().ToListAsync());
        }

        // GET: CarModels/Create
        public IActionResult Create()
        {
            PopulateVendorsDropDownList();
            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EngineCapacity,EngineType,DriveUnitType,TransmissionType,VendorId")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateVendorsDropDownList(carModel.VendorId);
            return View(carModel);
        }

        // GET: CarModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels.SingleOrDefaultAsync(m => m.Id == id);
            if (carModel == null)
            {
                return NotFound();
            }

            PopulateVendorsDropDownList(carModel.VendorId);
            return View(carModel);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EngineCapacity,EngineType,DriveUnitType,TransmissionType,VendorId")] CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.Id))
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
            PopulateVendorsDropDownList(carModel.VendorId);
            return View(carModel);
        }

        // GET: CarModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels.Include(x=>x.Vendor).AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carModel = await _context.CarModels.SingleOrDefaultAsync(m => m.Id == id);
            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModels.Any(e => e.Id == id);
        }

        private void PopulateVendorsDropDownList(object selectedVendor = null)
        {
            var vendorQuery = _context.Vendors.OrderBy(x => x.Name);
            ViewBag.Vendors = new SelectList(vendorQuery.AsNoTracking(), "Id", "Name", selectedVendor);
        }
    }
}
