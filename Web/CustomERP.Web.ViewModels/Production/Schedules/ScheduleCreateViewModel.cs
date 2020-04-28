namespace CustomERP.Web.ViewModels.Production.Schedules
{
    using System.ComponentModel.DataAnnotations;

    using CustomERP.Data.Models;
    using CustomERP.Services.Mapping;

    public class ScheduleCreateViewModel : IMapFrom<Schedule>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int NumberOfDays { get; set; }
    }
}
