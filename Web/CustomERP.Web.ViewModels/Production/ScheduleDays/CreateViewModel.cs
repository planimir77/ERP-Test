namespace CustomERP.Web.ViewModels.Production.ScheduleDays
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using CustomERP.Data.Models;
    using CustomERP.Services.Mapping;

    [Display(Name = "Day")]
    public class CreateViewModel : IMapFrom<ScheduleDay>
    {
        public int Name { get; set; }

        public IEnumerable<DayNameDropDownViewModel> ScheduleDayNames { get; set; }

        [Required]
        public Mode WorkingMode { get; set; }

        public string Begins { get; set; }

        public int? Duration { get; set; }

        public int? IncludingRest { get; set; }

        public int ScheduleId { get; set; }
    }
}
