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
using System.Net.Mail;
using System.Net;

public partial class Sales_SalesTransactions : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillSalesHeaderGridView();            
        }
    }

    private void FillSalesHeaderGridView()
    {
        grdSalesHeader.DataSource = GetSalesHeaderDataSet().Tables["Sales_Transaction"];
        grdSalesHeader.DataBind();
        Session["DataTable"] = grdSalesHeader.DataSource;
    }
    private void FillSalesDetailGridView()
    {

        grdSalesDetail.DataSource = GetSalesDetailDataSet().Tables["Sales_Transaction_Detail"];
        grdSalesDetail.DataBind();
        Session["DataTable"] = grdSalesDetail.DataSource;
    }

    private DataSet GetSalesHeaderDataSet()
    {
        string selectSQL;
        selectSQL = "select st.sales_transaction_id as Transaction_ID, st.transaction_date as Sales_Date, e.employee_info as Employee_Name, st.other_info from sales_transaction st inner join employee e on st.employee_id = e.employee_id order by st.sales_transaction_id";
        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsInv = new DataSet();
        try
        {
            adapter.Fill(dsInv, "Sales_Transaction");
            return dsInv;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of items. ";
            lblResults.Text += err.Message;
            return null;
        }
    }

   

    private DataSet GetSalesDetailDataSet()
    {
        
        string selectSQL;
        selectSQL = "select std.std_id, std.sales_transaction_id, coalesce(i.item_number, bi.bitem_number) as Item_Number , coalesce(p.product_desc, pp.product_desc) as Description, std.quantity as Quantity, std.price as Price, std.quantity * std.price as Total from sales_transaction_detail std left outer join item_detail iid on iid.item_detail_id = std.item_detail_id left outer  join item i on i.item_number = iid.item_number left outer  join product p on p.product_code = i.product_code left outer join bulk_item_detail bid on bid.bulk_item_detail_id = std.bulk_item_detail_id left outer  join bulk_item bi on bi.bitem_number = bid.bitem_number left outer join product pp on pp.product_code = bi.product_code where std.sales_transaction_id = ";
        selectSQL += grdSalesHeader.SelectedRow.Cells[1].Text ;        

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsInv = new DataSet();
        try
        {
            adapter.Fill(dsInv, "Sales_Transaction_detail");
            return dsInv;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of items. ";
            lblResults.Text += err.Message;
            return null;
        }
    }
   

    protected void dgSalesHeader_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortSalesHeader(e.SortExpression, false);
    }

    protected void grdSalesHeader_SelectedIndexChanged(object sender, EventArgs e)
    {

        
        FillSalesDetailGridView();
    }
    protected void grdSalesHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }


    private void SortSalesHeader(string col, bool paging)
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
        grdSalesHeader.DataSource = dataView;
        grdSalesHeader.DataBind();
        Session["DataTable"] = ((DataView)grdSalesHeader.DataSource).Table;
        Session["SortColumn"] = col;
    }
    protected void grdSalesDetail_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdSalesDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void empComm_Click(object sender, EventArgs e)
    {
        string selectSQL;
        string[] bodyArray = new string[500];
        string body = "";
        string returnedString = "";

        selectSQL = "select com.employee_id, e.employee_info as Salesperson, to_char(com.amount, 'L99G999D99MI') as Amount, to_char(com.commission_date, 'dd-MM-yy') Sales_Date from sales_commission com";
        selectSQL += " inner join employee e on e.employee_id = com.employee_id where com.commission_date between sysdate - 7 and sysdate";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);
        DataSet dataset = new DataSet();
        adapter.Fill(dataset, "sales_commission");
        int rowcount = 0;

        foreach (DataRow row in dataset.Tables["sales_commission"].Rows)
        {
            body = " Employee ID: " + row["employee_id"] + " SalesPerson: " + row["Salesperson"] + " Amount: " + row["amount"]
                  + " Commisson Date: " + row["Sales_Date"];
            rowcount++;
            bodyArray[rowcount] += body;
        }

        TableBuilder builder = new TableBuilder(rowcount, bodyArray);
        returnedString = builder.BuildTable();

        MailMessage o = new MailMessage();
        NetworkCredential netCred = new NetworkCredential();
        SmtpClient smtpobj = new SmtpClient();
        smtpobj.EnableSsl = false;
        smtpobj.Credentials = netCred;
        smtpobj.Send(o);

    }
}