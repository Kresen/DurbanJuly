using DurbanJuly.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DurbanJuly.Domain
{
   public class EventDetail : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
        public int Number { get; set; }
        public decimal Odd { get; set; }
        public int FinishingPosition { get; set; }
        public bool FirstTimer { get; set; }
        [ForeignKey("EventDetailStatusId")]
        public int EventDetailStatusId { get; set; }
        public EventDetailStatus EventDetailStatus { get; set; }
        [ForeignKey("EventId")]
        public long EventId { get; set; }
        public Event Event { get; set; }
    }
}
