using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public enum TransmissionType
    {
        [Display(Name = "Ручная")]
        Manual,
        [Display(Name = "Автоматическая")]
        Automatic
    }
}