using CarShowRoom.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowRoom.Extensions
{
    public static class HtmlExt
    {
        public static string GetStageColor<TModel>(this IHtmlHelper<TModel> Html, Stage stage)
        {
            var dict = new Dictionary<Stage, string> {
                {Stage.Lead, "#6c757d" },
                {Stage.Interest, "#17a2b8" },
                {Stage.Decision, "#ffc107" },
                {Stage.Purchase, "#007bff" },
                {Stage.Contracted, "#28a745" },
                {Stage.Denied, "#dc3545" }
            };

            return dict[stage];
        }
    }
}
