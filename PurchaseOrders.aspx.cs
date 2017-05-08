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

public partial class Purchasing_PurchaseOrders : System.Web.UI.Page
{
   string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillPurchHeaderGridView();
        }
    }

    private void FillPurchHeaderGridView()
    {
        grdPurchHeader.DataSource = GetPurchHeaderDataSet().Tables["Purchase_Order_Header"];
        grdPurchHeader.DataBind();
        Session["DataTable"] = grdPurchHeader.DataSource;
    }
    private void FillPurchDetailGridView()
    {
        grdPurchDetail.DataSource = GetPurchDetailDataSet().Tables["Purchase_Order_Detail"];
        grdPurchDetail.DataBind();
        Session["DataTable"] = grdPurchDetail.DataSource;
    }

    private DataSet GetPurchHeaderDataSet()
    {
        string selectSQL;
        selectSQL = "select p.purchaseorder_id as PurchaseID, v.vendorname as Vendor, p.po_date as PO_Date, other_info from purchase_order_header p inner join vendor v on p.vendor_id = v.vendor_id order by PurchaseID";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsInv = new DataSet();
        try
        {
            adapter.Fill(dsInv, "Purchase_Order_Header");
            return dsInv;
        }
        catch (Exception err)
        {
            return null;
        }
    }

    private DataSet GetPurchDetailDataSet()
    {
        string selectSQL;
        selectSQL = "select pod.poline_id as PODetail_ID, PO.purchaseorder_id as Purchase_Order_ID, coalesce(iid.item_number, bid.bitem_number) as Item_Number, i.product_code, coalesce(p.product_desc, pp.product_desc) as Description, pod.quantity, pod.price, pod.quantity * pod.price as Total from purchase_order_detail pod inner join purchase_order_header po on pod.purchaseorder_id = po.purchaseorder_id left outer join item_detail iid on pod.item_detail_id = iid.item_detail_id left outer join item i on i.item_number = iid.item_number  left outer join product p on p.product_code = i.product_code left outer join bulk_item_detail bid on bid.bulk_item_detail_id = pod.bulk_item_detail_id left outer  join bulk_item bi on bi.bitem_number = bid.bitem_number left outer join product pp on pp.product_code = bi.product_code where pod.purchaseorder_id = ";
        selectSQL += grdPurchHeader.SelectedRow.Cells[1].Text;

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsInv = new DataSet();
        try
        {
            adapter.Fill(dsInv, "Purchase_Order_Detail");
            return dsInv;
        }
        catch (Exception err)
        {
           return null;
        }
    }

    protected void dgPurchHeader_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortPurchHeader(e.SortExpression, false);
    }
    protected void grdPurchHeader_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPurchDetailGridView();
    }
    protected void grdPurchHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SortPurchHeader((string)Session["SortColumn"], true);
        grdPurchHeader.PageIndex = e.NewPageIndex;
        grdPurchHeader.DataBind();
    }

    private void SortPurchHeader(string col, bool paging)
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
        grdPurchHeader.DataSource = dataView;
        grdPurchHeader.DataBind();
        Session["DataTable"] = ((DataView)grdPurchHeader.DataSource).Table;
        Session["SortColumn"] = col;
    }
    protected void grdPurchDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectSQL;
        selectSQL = "select pod.poline_id as PODetail_ID, PO.purchaseorder_id as Purchase_Order_ID, iid.item_number as Item_Number, i.product_code, p.product_desc as Description, pod.quantity as Quantity, pod.price as Price, pod.quantity * pod.price as Total from purchase_order_detail pod inner join purchase_order_header po on pod.purchaseorder_id = po.purchaseorder_id inner join item_detail iid on pod.item_detail_id = iid.item_detail_id inner join item i on i.item_number = iid.item_number  inner join product p on p.product_code = i.product_code";
        selectSQL += " WHERE pod.purchaseorder_id ='" + grdPurchHeader.SelectedRow.Cells[1].Text + "'";

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(selectSQL, con);
        OracleDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            lblResults.Text = "<b>" + reader["PODetail_ID"];
            lblResults.Text = ", " + reader["Item_Number"];
            lblResults.Text += ", " + reader["Description"] + "</b>";
            lblResults.Text += ", " + reader["Quantity"] + "</b>";
            lblResults.Text += ", " + reader["Price"] + "</b>";
            lblResults.Text += ", " + reader["Total"] + "</b>";
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
    protected void grdPurchDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }    
}