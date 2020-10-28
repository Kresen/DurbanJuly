using DurbanJuly.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DurbanJuly.Domain
{
    public class Tournament : BaseEntity
    {
        [StringLength(200)]
        public string Name { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
    }
}
