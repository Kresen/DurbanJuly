using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DurbanJuly.Models
{
    public class EventDetailModel
    {
        public long Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public int Number { get; set; }
        public decimal Odd { get; set; }
        public int FinishingPosition { get; set; }
        public bool FirstTimer { get; set; }
        public int EventDetailStatusId { get; set; }

        public long EventId { get; set; }
        public string StatusName { get; set; }

        public List<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();

    }
}
