namespace CustomERP.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CustomERP.Data.Common.Models;

    public class ScheduleDay : BaseDeletableModel<int>
    {
        [Required]
        public int Name { get; set; }

        [Required]
        public Mode WorkingMode { get; set; }

        [RegularExpression("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")]
        public string Begins { get; set; }

        [Range(0, 12)]
        public int? Duration { get; set; }

        [Range(0, 60)]
        public int? IncludingRest { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

    }
}
