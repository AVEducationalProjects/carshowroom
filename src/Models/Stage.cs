using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public enum Stage
    {
        [Display(Name = "Новый")]
        Lead,
        [Display(Name = "Проявлен интерес")]
        Interest,
        [Display(Name = "Принимается решение")]
        Decision,
        [Display(Name = "Покупка")]
        Purchase,
        [Display(Name = "Сделка завершена")]
        Contracted,
        [Display(Name = "Отказ")]
        Denied
    }
}