using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace CityView
{
    public partial class Institution
    {
        public Institution()
        {
            InstitutionComments = new HashSet<InstitutionComment>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Заклад")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Опис Закладу")]
        public string DescriptionInfo { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Контакти")]
        public string Contacts { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }
        [Display(Name = "Місто")]
        public int? CityId { get; set; }
        [Display(Name = "Місто")]
        public virtual City City { get; set; }
        public virtual ICollection<InstitutionComment> InstitutionComments { get; set; }
    }
}
