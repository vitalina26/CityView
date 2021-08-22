using System;
using System.Collections.Generic;

#nullable disable

namespace CityView
{
    public partial class EventComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
