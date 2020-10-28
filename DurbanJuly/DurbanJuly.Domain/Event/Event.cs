using DurbanJuly.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DurbanJuly.Domain
{
   public  class Event : BaseEntity
    {
        [ForeignKey("TournamentId")]
        public long TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int Number { get; set; }

        public DateTime EventDateTime { get; set; }

        public DateTime EventEndDateTime { get; set; }

        public bool AutoClose { get; set; }

        public List<EventDetail> EventDetails { get; set; } = new List<EventDetail>();

    }
}
