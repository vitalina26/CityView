using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CityView
{
    public partial class EventComment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Коментар")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Дата створення")]
        public DateTime? DateOfCreation { get; set; }
        [Display(Name = "Подія")]
        public int EventId { get; set; }
        [Display(Name = "Подія")]
        public virtual Event Event { get; set; }
    }
}
