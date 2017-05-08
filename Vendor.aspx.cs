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

public partial class Vendor_Vendor : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillVendorGridView();
        }
    }

    private void FillVendorGridView()
    {
        grdVendor.DataSource = GetVendorDataSet().Tables["Vendor"];
        grdVendor.DataBind();
        Session["DataTable"] = grdVendor.DataSource;
    }

    private DataSet GetVendorDataSet()
    {
        string selectSQL;
        selectSQL = "select vendor_id as VendorID, vendorname as VendorName, street || ' ' || city || ', ' || state || ' ' || zip as Address, phone as Phone, alt_phone as Alt_Phone  from vendor";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsInv = new DataSet();
        try
        {
            adapter.Fill(dsInv, "Vendor");
            return dsInv;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of items. ";
            lblResults.Text += err.Message;
            return null;
        }
    }

    protected void grdVendor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SortVendor((string)Session["SortColumn"], true);
        grdVendor.PageIndex = e.NewPageIndex;
        grdVendor.DataBind();
    }

    private void SortVendor(string col, bool paging)
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
        grdVendor.DataSource = dataView;
        grdVendor.DataBind();
        Session["DataTable"] = ((DataView)grdVendor.DataSource).Table;
        Session["SortColumn"] = col;
    }


    protected void CreateVendor_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Vendor/VendorManager.aspx");
    }
    protected void grdVendor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }   
}