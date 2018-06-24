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
    public class OrdersController : Controller
    {
        private readonly CRMContext _context;

        public OrdersController(CRMContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = _context.Orders
                .Include(o => o.Car).ThenInclude(o => o.CarModel).ThenInclude(o => o.Vendor)
                .Include(o => o.Car).ThenInclude(o => o.Color)
                .Include(o => o.Client)
                .Include(o => o.Services).ThenInclude(o => o.Service)
                .Include(o => o.Parts).ThenInclude(o => o.PartType)
                .Include(o => o.Bills)
                .OrderByDescending(x => x.Done).ThenBy(x => x.Date);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Car).ThenInclude(o => o.CarModel).ThenInclude(o => o.Vendor)
                .Include(o => o.Car).ThenInclude(o => o.Color)
                .Include(o => o.Client)
                .Include(o => o.Services).ThenInclude(o => o.Service)
                .Include(o => o.Parts).ThenInclude(o => o.PartType)
                .Include(o => o.Bills)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id)
        {
            var order = await _context.Orders.Include(x => x.Car).Include(x=>x.Client).SingleOrDefaultAsync(m => m.Id == id);
            order.Done = true;
            if (order.IsSell)
            {
                order.Client.Stage = Stage.Contracted;
                order.Car.Sold = true;
                order.Car.TestDrive = false;
                order.Car.DepotId = null;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
