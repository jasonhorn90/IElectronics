using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for TableBuilder
/// </summary>
public class TableBuilder
{
	private StringBuilder builder = new StringBuilder();


        public string[] BodyData { get; set; }
        public int BodyRows { get; set; }


        public TableBuilder(int bodyRows, string[] bodyData)
        {
            BodyData = bodyData;
            BodyRows = bodyRows;
        }

        /// <summary>
        /// Since your table headers are static, and your table body
        /// is variable, we don't need to store the headers. Instead
        /// we need to know the number of rows and the information
        /// that goes in those rows.
        /// </summary>
        public TableBuilder(string[] tableInfo, int bodyRows)
        {
            BodyData = tableInfo;
            BodyRows = bodyRows;
        }

        public string BuildTable()
        {
            //BuildTableHead();
            BuildTableBody();
            return builder.ToString();
        }

        private void BuildTableHead()
        {
            builder.Append("<table>");
            builder.Append("<thead>");
            builder.Append("<tr>");
            AppendTableHeader("HeaderOne");
            AppendTableHeader("HeaderTwo");
            builder.Append("</tr>");
            builder.Append("</thead>");
        }

        private void BuildTableBody()
        {
          //  builder.Append("<tbody>");
          //  builder.Append("<tr>");
            // For every row we need added, append a <td>info</td>
            // to the table from the data we have
            for (int i = 0; i < BodyRows; i++)
            {
                AppendTableDefinition(BodyData[i]);

            }
           // builder.Append("</tr>");
          //  builder.Append("</table");
        }

        private void AppendTableHeader(string input)
        {
            AppendTag("th", input);
        }

        private void AppendTableDefinition(string input)
        {
            AppendTag("td", input);
        }

        private void AppendTag(string tag, string input)
        {
           // builder.Append("<" + tag + ">");
            builder.AppendLine(input);
            builder.AppendLine("");
           // builder.Append("</" + tag + ">");
        }
}