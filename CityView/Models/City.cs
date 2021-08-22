using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace CityView
{
    public partial class City
    {
        public City()
        {
            Events = new HashSet<Event>();
            Institutions = new HashSet<Institution>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "ПОЛЕ НЕ ПОВИННО БУТИ ПОРОЖНІМ")]
        [Display(Name = "Місто")]
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Institution> Institutions { get; set; }
    }
}
