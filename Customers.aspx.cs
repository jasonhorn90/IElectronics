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

public partial class Customers_Customers : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillCustomerGridView();
           
        }
    }

    private void FillCustomerGridView()
    {
        grdCustomer.DataSource = GetDataSet().Tables["Customer"];
        grdCustomer.DataBind();
        Session["DataTable"] = grdCustomer.DataSource;
    }

    private void FillCreditGridView()
    {
        grdCredit.DataSource = GetCreditDataSet().Tables["Credit"];
        grdCredit.DataBind();
        Session["DataTable"] = grdCredit.DataSource;
       
    }

    private DataSet GetDataSet()
    {
        string selectSQL;
        selectSQL = "SELECT cus.customer_id as  Customer_ID, cus.fname as First_Name, cus.lname as Last_Name, ad.street || ',' || ad.city || ', ' || ad.state || ', ' || ad.zip as Address, ad.address_type as Address_Type, cus.phone as Phone, cus.alt_phone as Alt_Phone, cus.other_info as Other_Info from customer cus inner join address1 ad on ad.customer_id  = cus.customer_id";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsEmployee = new DataSet();
        try
        {
            adapter.Fill(dsEmployee, "Customer");
            return dsEmployee;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of names. ";
            lblResults.Text += err.Message;
            return null;
        }
    }

    private DataSet GetCreditDataSet()
    {
        string selectSQL;
        selectSQL = "SELECT credit_id, customer_id, card_name, 'XXXX-XXXX-XXXX-' || substr(to_char(card_number), 13, 4) as Card_Number, other_info from credit ";
        selectSQL += "WHERE customer_id = " + grdCustomer.SelectedRow.Cells[1].Text;

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsEmployee = new DataSet();
        try
        {
            adapter.Fill(dsEmployee, "Credit");
            return dsEmployee;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of names. ";
            lblResults.Text += err.Message;
            return null;
        }
    }


    protected void dgCustomer_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortGridView(e.SortExpression, false);
    }

    protected void dgCredit_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortCreditGridView(e.SortExpression, false);
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
        grdCustomer.DataSource = dataView;
        grdCustomer.DataBind();
        Session["DataTable"] = ((DataView)grdCustomer.DataSource).Table;
        Session["SortColumn"] = col;
    }

    private void SortCreditGridView(string col, bool paging)
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
        grdCredit.DataSource = dataView;
        grdCredit.DataBind();
        Session["DataTable"] = ((DataView)grdCredit.DataSource).Table;
        Session["SortColumn"] = col;
    }

    protected void grdCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCreditGridView();
        
        /*
        string selectSQL;
        selectSQL = "SELECT * FROM customer ";
        selectSQL += "WHERE customer_id=" + grdCustomer.SelectedRow.Cells[1].Text;

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(selectSQL, con);
        OracleDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            lblResults.Text = "<b>" + reader["Customer_ID"];
            lblResults.Text = ", " + reader["First_Name"];
            lblResults.Text += ", " + reader["Last_Name"] + "</b>";
            lblResults.Text += ", " + reader["Address"] + "</b>";
            lblResults.Text += ", " + reader["Address_Type"] + "</b>";
            lblResults.Text += ", Phone " + reader["phone"] + "<br>";
            lblResults.Text += ", ALt Phone " + reader["alt_phone"] + "<br>";
            GetCreditDataSet();
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
        * */
    }

    protected void dgEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       /*
        string deleteSQL;
        deleteSQL = "delete from customer where customer_id = '" + grdCustomer.Rows[e.RowIndex].Cells[1].Text + "'";
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(deleteSQL, con);
        try
        {
            con.Open();
            int num = cmd.ExecuteNonQuery();
            lblResults.Text = num.ToString() + " records deleted.";
            FillCustomerGridView();
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
        * 
        *
        */
    }
    protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SortGridView((string)Session["SortColumn"], true);
        grdCustomer.PageIndex = e.NewPageIndex;
        grdCustomer.DataBind();
    }
    protected void CreateCustomer_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Customers/CustomerManager.aspx");
    }
    protected void grdCredit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdCredit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}