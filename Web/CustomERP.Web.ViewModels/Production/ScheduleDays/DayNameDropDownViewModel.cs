namespace CustomERP.Web.ViewModels.Production.ScheduleDays
{ public class DayNameDropDownViewModel
    {
        public int Name { get; set; }

        public string DayName
        {
            get { return "Day " + this.Name; }
        }
    }
}