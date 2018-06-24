using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CarShowRoom.Db;
using CarShowRoom.Models;
using Microsoft.AspNetCore.Identity;
using CarShowRoom.ViewModels;

namespace CarShowRoom.Controllers
{
    public class ReportController : Controller
    {
        private CRMContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ReportController(CRMContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index([Bind("To", "From")] ReportViewModel data)
        {
            if (data.HasPeriod())
            {
                var clients = await _context.Clients
                    .Where(x => x.LastChange <= data.To.Value && x.LastChange >= data.From.Value)
                    .Include(x => x.Account).ToListAsync();


                data = new ReportViewModel
                {
                    From = data.From,
                    To = data.To,
                    TotalLead = clients.Count(x => x.Stage == Models.Stage.Lead),
                    TotalInterest = clients.Count(x => x.Stage == Models.Stage.Interest),
                    TotalDecision = clients.Count(x => x.Stage == Models.Stage.Decision),
                    TotalPurchase = clients.Count(x => x.Stage == Models.Stage.Purchase),
                    TotalContracted = clients.Count(x => x.Stage == Models.Stage.Contracted),
                    TotalDenied = clients.Count(x => x.Stage == Models.Stage.Denied),

                    AccountStats = _userManager.Users.Select(
                        a => new ReportAccountViewModel
                        {
                            AccountName = a.ToString(),
                            Lead = clients.Where(c => c.AccountId == a.Id).Count(x => x.Stage == Models.Stage.Lead),
                            Interest = clients.Where(c => c.AccountId == a.Id).Count(x => x.Stage == Models.Stage.Interest),
                            Decision = clients.Where(c => c.AccountId == a.Id).Count(x => x.Stage == Models.Stage.Decision),
                            Purchase = clients.Where(c => c.AccountId == a.Id).Count(x => x.Stage == Models.Stage.Purchase),
                            Contracted = clients.Where(c => c.AccountId == a.Id).Count(x => x.Stage == Models.Stage.Contracted),
                            Denied = clients.Where(c => c.AccountId == a.Id).Count(x => x.Stage == Models.Stage.Denied),
                        }).ToList()
                };

            }
            return View(data);

        }
    }
}