using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DurbanJuly.Models
{
    public class EventModel : IValidatableObject
    {
        public long Id { get; set; }
        public int TournamentId { get; set; }

        public string  TournamentName { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int Number { get; set; }

        [Display(Name = "Event Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EventDateTime { get; set; } = DateTime.Now;

        [Display(Name = "Event End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EventEndDateTime { get; set; } = DateTime.Now.AddDays(1);

        public bool AutoClose { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EventEndDateTime < EventDateTime)
            {
                yield return new ValidationResult("End Date must be greater than StartDate");
            }
        }
    }
}
