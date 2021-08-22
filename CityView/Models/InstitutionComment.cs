using System;
using System.Collections.Generic;

#nullable disable

namespace CityView
{
    public partial class InstitutionComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int InstitutionId { get; set; }

        public virtual Institution Institution { get; set; }
    }
}
