using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarShowRoom.Db;
using CarShowRoom.Models;
using CarShowRoom.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CarShowRoom.Controllers
{
    [Authorize(Roles = "admin")]
    public class ClientsController : Controller
    {
        private readonly CRMContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientsController(CRMContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var list = await _context.Clients.ToListAsync();
            FillStats(list);
            return View(list);
        }

        private void FillStats(IList<Client> list)
        {
            if (list.Count == 0)
            {
                ViewBag.Stats = new Dictionary<Stage, int>
                {
                    {Stage.Lead, 100},
                    {Stage.Interest, 0},
                    {Stage.Decision, 0},
                    {Stage.Purchase, 0 },
                    {Stage.Contracted, 0},
                    {Stage.Denied, 0}
                };
            }
            else
            {
                ViewBag.Stats = new Dictionary<Stage, int>
                {
                    {Stage.Lead, list.Count(x=>x.Stage==Stage.Lead)*100/list.Count},
                    {Stage.Interest, list.Count(x=>x.Stage==Stage.Interest)*100/list.Count},
                    {Stage.Decision, list.Count(x=>x.Stage==Stage.Decision)*100/list.Count},
                    {Stage.Purchase, list.Count(x=>x.Stage==Stage.Purchase)*100/list.Count},
                    {Stage.Contracted, list.Count(x=>x.Stage==Stage.Contracted)*100/list.Count},
                    {Stage.Denied, list.Count(x=>x.Stage==Stage.Denied)*100/list.Count}
                };
            }
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(x => x.Account)
                .Include(x => x.Cars)
                    .ThenInclude(x => x.CarModel)
                        .ThenInclude(x => x.Vendor)
                .Include(x => x.Cars)
                    .ThenInclude(x => x.Color)
                .Include(x => x.Cars)
                    .ThenInclude(x => x.Depot)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            ViewBag.Orders = await _context.Orders
                .Where(x => x.ClientId == id.Value && !x.IsSell)
                .Include(o => o.Car).ThenInclude(o => o.CarModel).ThenInclude(o => o.Vendor)
                .Include(o => o.Car).ThenInclude(o => o.Color)
                .Include(o => o.Client)
                .Include(o => o.Services).ThenInclude(o => o.Service)
                .Include(o => o.Parts).ThenInclude(o => o.PartType)
                .Include(o => o.Bills)
                .OrderByDescending(x => x.Done).ThenBy(x => x.Date)
                .ToListAsync();

            return View(client);
        }

        // GET: Clients/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Accounts = new SelectList(_userManager.Users,"Id", null);
            return View(new Client
            {
                AccountId = (await _userManager.GetUserAsync(User)).Id
            });
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,Address,Phone,Email,Stage,AccountId")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Accounts = new SelectList(_userManager.Users, "Id", null);

            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,Address,Phone,Email,Stage,AccountId")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existedClient = await _context.Clients.SingleAsync(x => x.Id == id);
                    existedClient.MiddleName = client.MiddleName;
                    existedClient.FirstName = client.FirstName;
                    existedClient.LastName = client.LastName;
                    existedClient.Phone = client.Phone;
                    existedClient.Address = client.Address;
                    existedClient.Email = client.Email;
                    existedClient.AccountId = client.AccountId;
                    existedClient.UpdateStage(client.Stage);


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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

            ViewBag.Accounts = new SelectList(_userManager.Users, "Id", null);

            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet, ActionName("NextStage")]
        public async Task<IActionResult> NextStage(int id)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);

            if (client.Stage != Stage.Denied && client.Stage != Stage.Contracted)
            {
                client.UpdateStage(client.Stage + 1);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCarOrder(int id)
        {
            ViewBag.AvailableCars = await _context.Cars
                .Include(x => x.CarModel)
                .Include(x => x.CarModel.Vendor)
                .Include(x => x.Color)
                .Include(x => x.Depot)
                .Where(x => x.ClientId == null)
                .ToListAsync();

            ViewData["CarModelId"] = new SelectList(_context.CarModels.Include(c => c.Vendor).AsNoTracking(), "Id", null);
            ViewData["ColorId"] = new SelectList(_context.CarColors, "Id", "Name");
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Name");

            return View(new Car { ClientId = id, Year = DateTime.Now.Year });
        }

        public async Task<IActionResult> SelectCar(int id, int carId)
        {
            var car = await _context.Cars.Include(x => x.CarModel).SingleAsync(x => x.Id == carId);
            car.ClientId = id;

            await CreateSellOrder(car);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCarOrder([Bind("Id,VIN,Year,Price,ColorId,TestDrive,CarModelId,PartnerId,ClientId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();

                await CreateSellOrder(car);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Details), new { Id = car.ClientId });
            }
            ViewData["CarModelId"] = new SelectList(_context.CarModels.Include(c => c.Vendor).AsNoTracking(), "Id", null);
            ViewData["ColorId"] = new SelectList(_context.CarColors, "Id", "Name", car.ColorId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Name", car.PartnerId);
            return View(car);
        }


        private async Task CreateSellOrder(Car car)
        {
            var order = new Order
            {
                CarId = car.Id,
                Car = car,
                IsSell = true,
                ClientId = car.ClientId.Value,
                Parts = new List<PartOrderItem>(),
                Services = new List<ServiceOrderItem>()
            };

            order.UpdatePrice();

            _context.Orders.Add(order);

            var client = await _context.Clients.SingleAsync(x => x.Id == car.ClientId.Value);
            client.UpdateStage(Stage.Purchase);
        }

        public async Task<IActionResult> DismissOrder(int id, int carId)
        {
            var car = await _context.Cars.SingleAsync(x => x.Id == carId);
            car.ClientId = null;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = id });
        }

        public async Task<IActionResult> CreateOrder(int id)
        {
            var order = new Order()
            {
                ClientId = id,
                IsSell = false,
            };

            var client = await _context.Clients
                .Include(x => x.Cars).ThenInclude(x => x.CarModel).ThenInclude(x => x.Vendor)
                .Include(x => x.Cars).ThenInclude(x => x.Color)
                .SingleAsync(x => x.Id == id);
            ViewBag.Cars = new SelectList(client.Cars, "Id", null);
            ViewBag.Services = (await _context.Services.ToListAsync())
                .Select(x => new ServiceCheck { Id = x.Id, Name = x.Name, Price = x.Price, Checked = false }).ToList();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([Bind("Date", "CarId", "ClientId")]Order order, List<ServiceCheck> services)
        {
            foreach (var svc in services.Where(x => x.Checked))
            {
                var service = await _context.Services.SingleAsync(x => x.Id == svc.Id);
                order.Services.Add(new ServiceOrderItem { Service = service, Order = order });
            }

            order.UpdatePrice();
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { Id = order.ClientId });
        }
    }
}
