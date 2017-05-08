using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using System.Web.Configuration;
using System.Web.Security;
using System.Data;


public partial class Home : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            BindPieChart();
            BindBarChart();
        }
    }    
   
    protected void Loginstatus2_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Login.aspx");
    }

    protected void BindPieChart()
    {
        string selectSQL;
       // selectSQL = "select l.lname as Branch_Location, count(pnum) as Total_Patients from Patient_visit pv inner join b_location l on l.lnum = pv.lnum group by l.lname";
        selectSQL = "select case sl.location_id when 1 then 'St. Louis' when 2 then 'St. Charles' when 3 then 'Bridgeton' when 4 then 'Kansas City' ELSE 'Kirkwood' end as Store_Location, count(st.sales_transaction_id) as Total_Customers from store_location sl INNER join item_detail iid on iid.location_id = sl.location_id inner join sales_transaction_detail std on std.item_detail_id = iid.item_detail_id inner join sales_transaction st on st.sales_transaction_id = std.sales_transaction_id group by sl.location_id";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsCustTraffic = new DataSet();
        DataTable dt = new DataTable();
        try
        {
            adapter.Fill(dsCustTraffic, "sales_transaction_detail");
            dt = dsCustTraffic.Tables[0];
           
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of names. ";
            lblResults.Text += err.Message;            
        }

        foreach (DataRow row in dt.Rows)
        {
           PieChart1.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
            {
                Category = row["Store_Location"].ToString(),
                Data = Convert.ToDecimal(row["Total_Customers"])
            });
        } 
        
    }

    protected void BindBarChart()
    {
        string selectSQL;
        Random random = new Random();
       

        // selectSQL = "select l.lname as Branch_Location, sum(i.invoiceamount) as Total_Revenue from b_location l inner join patient_visit pk on pk.lnum = l.lnum INNER JOIN Invoice i on i.invoiceid = pk.invid group by l.lname";
        selectSQL = "select e.employee_info as Employee_Name, sum(std.quantity * std.price) as Total_Sales from employee e inner join sales_transaction_detail std on std.employee_id = e.employee_id inner join sales_transaction st on st.sales_transaction_id  = std.sales_transaction_id where st.transaction_date between sysdate - 30 and SYSDATE group by e.employee_info ";
         OracleConnection con = new OracleConnection(connectionString);
         OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

         DataSet dsLocationRevenue = new DataSet();
         DataTable dt = new DataTable();
         try
         {
             adapter.Fill(dsLocationRevenue, "Sales_Transaction_Detail");
             dt = dsLocationRevenue.Tables[0];           
         }
         catch (Exception err)
         {
             lblResults.Text = "Error reading list of names. ";
             lblResults.Text += err.Message;           
         }   

         string[] x = new string[dt.Rows.Count];
         decimal[] y = new decimal[dt.Rows.Count];
         for (int i = 0; i < dt.Rows.Count; i++)
         {
             x[i] = dt.Rows[i][0].ToString();
             y[i] = Convert.ToInt32(dt.Rows[i][1]);
         }
         BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, BarColor="#00bfff", Name="Employee"});         
         BarChart1.CategoriesAxis = string.Join(",", x);

        
        // BarChart1.ChartTitle = string.Format("{0} Order Distribution", ddlCountries.SelectedItem.Value);
         if (x.Length > 3)
         {
             BarChart1.ChartWidth = (x.Length * 175).ToString();
         }    
         

    }
}