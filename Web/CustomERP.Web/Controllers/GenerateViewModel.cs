using System.Text;

namespace CustomERP.Web.Controllers
{
    using System;
    using System.Linq;

    public class GenerateViewModel
    {
        public string GenerateCalendarTable(int year, int month, int date)
        {
            while (date - 4 > 0)
            {
                date -= 4;
            }

            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int offset = (int)firstDayOfMonth.DayOfWeek - 1;
            int maxValue = DateTime.DaysInMonth(year, month);
            int columnCount = 7;
            int rowCount = (int)Math.Ceiling((offset + maxValue) / (decimal)columnCount);

            var result = new StringBuilder();

            result.Append("<table class=\"calendar\">");

            for (int row = 1; row <= rowCount; row++)
            {
                result.Append("<tr>");
                for (int n = 1; n <= columnCount; n++)
                {
                    var temp = (columnCount * (row - 1)) + n - offset;


                    var day = temp >= 1 && temp <= maxValue ? temp.ToString() : " ";
                    result.Append("<td>");
                    result.Append(day);
                    result.Append("</td>");
                    //Console.Write(day + " ");
                }

                result.Append("</tr>");
            }

            result.Append("</table>");

            return result.ToString().TrimEnd();
            //"<table class=\"calendar\">" +
            //Enumerable.Range(1, rowCount).Select(r =>
            //    "<tr>" +
            //    Enumerable.Range((columnCount * (r - 1)) + 1 - offset, columnCount)
            //        .Select(n => "<td>" + (n >= 1 && n <= maxValue ? n.ToString() : " ") + "</td>")
            //        .Aggregate((html, cell) => html + cell) +
            //    "</tr>"
            //).Aggregate((html, row) => html + row) +
            //"</table>";
        }
    }
}
