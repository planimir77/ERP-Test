namespace CustomERP.Web.ViewModels.Production.Sections
{
    using System;

    using AutoMapper;
    using CustomERP.Data.Models;
    using CustomERP.Services.Mapping;

    public class SectionIndexViewModel : IMapFrom<Section>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public int? SectionParentId { get; set; }

        public virtual Section SectionParent { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string CreatedFrom { get; set; }

#nullable enable
        public string? ModifiedFrom { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

#nullable enable
        public string? DeletedFrom { get; set; }
    }
}
