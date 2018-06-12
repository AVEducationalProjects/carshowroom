using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public enum Stage
    {
        [Display(Name = "�����")]
        Lead,
        [Display(Name = "�������� �������")]
        Interest,
        [Display(Name = "����������� �������")]
        Decision,
        [Display(Name = "�������")]
        Purchase,
        [Display(Name = "������ ���������")]
        Contracted,
        [Display(Name = "�����")]
        Denied
    }
}