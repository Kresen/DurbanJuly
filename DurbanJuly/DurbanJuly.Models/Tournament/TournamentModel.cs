using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DurbanJuly.Models
{
    public class TournamentModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public long EventCount { get; set; }
    }
}
