using System.Collections.Generic;

namespace CustomERP.Web.ViewModels.Production.Sections
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CustomERP.Data.Models;
    using CustomERP.Services.Mapping;

    public class SectionEditInputModel : IMapFrom<Section>
    {
        //public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int CurrentDepartmentId { get; set; }

        [ForeignKey(nameof(CurrentDepartmentId))]
        public Department Department { get; set; }

        [Display(Name = "Part of")]
        public int? CurrentSectionParentId { get; set; }

        [ForeignKey(nameof(CurrentSectionParentId))]
        public virtual Section SectionParent { get; set; }

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
