using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public enum TransmissionType
    {
        [Display(Name = "������")]
        Manual,
        [Display(Name = "��������������")]
        Automatic
    }
}