namespace CustomERP.Web.ViewModels.Production.Sections
{
    using System.ComponentModel.DataAnnotations;

    using CustomERP.Data.Models;
    using CustomERP.Services.Mapping;

    public class DepartmentDropDownViewModel : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}