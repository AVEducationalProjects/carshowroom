using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public enum EngineType
    {
        [Display(Name = "����������")]
        Gas,
        [Display(Name = "���������")]
        Diesel
    }
}