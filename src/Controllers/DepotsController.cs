﻿using System;
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
    public class DepotsController : Controller
    {
        private readonly CRMContext _context;

        public DepotsController(CRMContext context)
        {
            _context = context;
        }

        // GET: Depots
        public async Task<IActionResult> Index()
        {
            return View(await _context.Depots.ToListAsync());
        }

        // GET: Depots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Depots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Depot depot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depot);
        }

        // GET: Depots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depot = await _context.Depots.SingleOrDefaultAsync(m => m.Id == id);
            if (depot == null)
            {
                return NotFound();
            }
            return View(depot);
        }

        // POST: Depots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Depot depot)
        {
            if (id != depot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepotExists(depot.Id))
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
            return View(depot);
        }

        // GET: Depots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depot = await _context.Depots
                .SingleOrDefaultAsync(m => m.Id == id);
            if (depot == null)
            {
                return NotFound();
            }

            return View(depot);
        }

        // POST: Depots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depot = await _context.Depots.SingleOrDefaultAsync(m => m.Id == id);
            _context.Depots.Remove(depot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepotExists(int id)
        {
            return _context.Depots.Any(e => e.Id == id);
        }
    }
}
