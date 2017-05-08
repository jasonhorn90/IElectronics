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


public partial class Employees : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillEmployeeGridView();
            // FillEmp_Sup_DDL();
        }
    }

    private void FillEmployeeGridView()
    {
        grdEmployee.DataSource = GetDataSet().Tables["Employee"];
        grdEmployee.DataBind();
        Session["DataTable"] = grdEmployee.DataSource;
    }

    private void FillEmployeeTimeCardGridView()
    {
        grdEmpTimecard.DataSource = GetTimecardDataSet().Tables["Timecard"];
        grdEmpTimecard.DataBind();
        Session["DataTable"] = grdEmpTimecard.DataSource;
    }

    private DataSet GetDataSet()
    {
        string selectSQL;
        selectSQL = "SELECT employee_id as Employee_ID, employee_info as Employee_Name, employee_type as Employee_Role, pay_rate as Pay_Rate FROM employee ";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsEmployee = new DataSet();
        try
        {
            adapter.Fill(dsEmployee, "Employee");
            return dsEmployee;
        }
        catch (Exception err)
        {
           return null;
        }
    }

    private DataSet GetTimecardDataSet()
    {
        string selectSQL;
        selectSQL = "select timecard_id, employee_id, location_id, time_in, time_out, total_hrs, total_pay from timecard ";
        selectSQL += "WHERE employee_id = " + grdEmployee.SelectedRow.Cells[1].Text;
        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsEmployee = new DataSet();
        try
        {
            adapter.Fill(dsEmployee, "Timecard");
            return dsEmployee;
        }
        catch (Exception err)
        {
           return null;
        }
    }

    protected void dgEmployee_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortGridView(e.SortExpression, false);
    }

    protected void dgEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SortGridView((string)Session["SortColumn"], true);
        grdEmployee.PageIndex = e.NewPageIndex;
        grdEmployee.DataBind();
    }

    private void SortGridView(string col, bool paging)
    {
        DataView dataView = new DataView((DataTable)Session["DataTable"]);
        if (!paging)
        {
            dataView.Sort = col + " " + ((string)Session["SortDirection"] == "Ascending" ? "desc" : "asc");
            Session["SortDirection"] = (string)Session["SortDirection"] == "Ascending" ? "Descending" : "Ascending";
        }
        else
        {
            if (Session["SortDirection"] != null)
            {
                dataView.Sort = col + " " + ((string)Session["SortDirection"] == "Ascending" ? "asc" : "desc");
            }
        }
        grdEmployee.DataSource = dataView;
        grdEmployee.DataBind();
        Session["DataTable"] = ((DataView)grdEmployee.DataSource).Table;
        Session["SortColumn"] = col;
    }

    private void SortTimecardGridView(string col, bool paging)
    {
        DataView dataView = new DataView((DataTable)Session["DataTable"]);
        if (!paging)
        {
            dataView.Sort = col + " " + ((string)Session["SortDirection"] == "Ascending" ? "desc" : "asc");
            Session["SortDirection"] = (string)Session["SortDirection"] == "Ascending" ? "Descending" : "Ascending";
        }
        else
        {
            if (Session["SortDirection"] != null)
            {
                dataView.Sort = col + " " + ((string)Session["SortDirection"] == "Ascending" ? "asc" : "desc");
            }
        }
        grdEmpTimecard.DataSource = dataView;
        grdEmpTimecard.DataBind();
        Session["DataTable"] = ((DataView)grdEmpTimecard.DataSource).Table;
        Session["SortColumn"] = col;
    }

    protected void dgEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        string selectSQL;
        selectSQL = "SELECT * FROM employee ";
        selectSQL += "WHERE employee_id='" + grdEmployee.SelectedRow.Cells[1].Text + "'";

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(selectSQL, con);
        OracleDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
         /*   lblResults.Text = "<b>" + reader["employee_id"];
            lblResults.Text = ", " + reader["employee_type"];
            lblResults.Text += ", " + reader["employee_info"] + "</b>";
            lblResults.Text += " Pay Rate " + reader["pay_rate"] + "<br>";
            reader.Close();
          * */
            FillEmployeeTimeCardGridView();
        }
        catch (Exception err)
        {
           
        }
        finally
        {
            con.Close();
        }
    }

    protected void dgEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string deleteSQL;
        deleteSQL = "delete from employee where employee_id = '" + grdEmployee.Rows[e.RowIndex].Cells[1].Text + "'";
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(deleteSQL, con);
        try
        {
            con.Open();
            int num = cmd.ExecuteNonQuery();
            lblResults.Text = num.ToString() + " records deleted.";
            FillEmployeeGridView();
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of names. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }

    protected void CreateEmp_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Employees/EmployeeManager.aspx");
    }
    protected void dgEmployeeTimecard_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortTimecardGridView(e.SortExpression, false);
    }
    protected void grdEmpTimecard_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectSQL;
        selectSQL = "SELECT * FROM timecard ";
        selectSQL += "WHERE timecard_id='" + grdEmpTimecard.SelectedRow.Cells[1].Text + "'";

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(selectSQL, con);
        OracleDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            lblResults.Text = "Timecard" + reader["timecard_id"];
            lblResults.Text = ", employee " + reader["employee_id"];
            lblResults.Text += ", location" + reader["location_id"] + "</b>";
            lblResults.Text += " ,time in " + reader["time_in"] + "<br>";
            lblResults.Text += ", time out " + reader["time_out"] + "<br>";
            lblResults.Text += ", total hours " + reader["total_hours"] + "<br>";
            lblResults.Text += " total pay " + reader["total_pay"] + "<br>";
            reader.Close();
        }
        catch (Exception err)
        {
            lblResults.Text = "Error getting info. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }
    protected void grdEmpTimecard_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SortTimecardGridView((string)Session["SortColumn"], true);
        grdEmpTimecard.PageIndex = e.NewPageIndex;
        grdEmpTimecard.DataBind();
    }
}