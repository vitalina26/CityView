using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CityView
{
    public partial class Event
    {
        public Event()
        {
            EventComments = new HashSet<EventComment>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Подія")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display( Name = "Опис події")]
        public string DescriptionInfo { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Контакти")]
        public string Contacts { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Дата та час")]
        public DateTime? EventDay { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Місто")]
        public int CityId { get; set; }
        [Display(Name = "Місто")]

        public virtual City City { get; set; }
        public virtual ICollection<EventComment> EventComments { get; set; }
    }
}
