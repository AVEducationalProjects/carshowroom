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
    public class BillsController : Controller
    {
        private readonly CRMContext _context;

        public BillsController(CRMContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {            
            return View(await _context.Bills.Include(x=>x.Order).OrderByDescending(x=>x.Date).ToListAsync());
        }

        // GET: Bills/Create
        public async Task<IActionResult> Create()
        {
            var orders = await _context.Orders.Include(x => x.Bills).Where(x => !x.Done).OrderBy(x => x.Date).ToListAsync();
            ViewData["Orders"] = orders.Where(x => !x.IsPaid()).ToList();
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,OrderId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                bill.Date = DateTime.Now;
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bill);
        }


    }
}
