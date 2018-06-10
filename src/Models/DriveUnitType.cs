using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public enum DriveUnitType
    {
        [Display(Name = "Передний")]
        Front,
        [Display(Name = "Задний")]
        Back,
        [Display(Name = "Полный")]
        Full
    }
}