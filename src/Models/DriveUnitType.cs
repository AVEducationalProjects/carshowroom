using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public enum DriveUnitType
    {
        [Display(Name = "��������")]
        Front,
        [Display(Name = "������")]
        Back,
        [Display(Name = "������")]
        Full
    }
}