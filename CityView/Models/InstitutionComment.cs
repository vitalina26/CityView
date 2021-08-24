using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CityView
{
    public partial class InstitutionComment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Коментар")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Дата створення")]
        public DateTime DateOfCreation { get; set; }
        [Display(Name = "Подія")]
        public int InstitutionId { get; set; }
        [Display(Name = "Подія")]
        public virtual Institution Institution { get; set; }
    }
}
