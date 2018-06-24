using System;
using System.Collections.Generic;
using System.Linq;
using CarShowRoom.ViewModels;

namespace CarShowRoom.ViewModels
{
    public class ReportViewModel
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int TotalLead { get; set; }
        public int TotalInterest { get; set; }
        public int TotalDecision { get; set; }
        public int TotalPurchase { get; set; }
        public int TotalContracted { get; set; }
        public int TotalDenied { get; set; }
        public IList<ReportAccountViewModel> AccountStats { get; set; }

        public bool HasPeriod()
        {
            return From.HasValue && To.HasValue;
        }
    }
}