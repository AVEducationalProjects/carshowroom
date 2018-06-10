using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public enum EngineType
    {
        [Display(Name = "Бензиновый")]
        Gas,
        [Display(Name = "Дизельный")]
        Diesel
    }
}