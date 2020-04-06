namespace CustomERP.Web.ViewModels.Production.Sections
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CustomERP.Data.Models;
    using CustomERP.Services.Mapping;

    public class SectionCreateInputModel : IMapFrom<Section>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Department field is required.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public IEnumerable<DepartmentDropDownViewModel> Departments { get; set; }

        [Display(Name = "Part of")]
        public int? SectionParentId { get; set; }

        public IEnumerable<SectionDropDownViewModel> Sections { get; set; }
    }
}
