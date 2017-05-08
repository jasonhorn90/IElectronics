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


public partial class Inventory_Inventory : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillInvGridView();
            // FillEmp_Sup_DDL();
        }
    }

    private void FillInvGridView()
    {
        grdInv.DataSource = GetDataSet().Tables["Item_Inventory"];
        grdInv.DataBind();
        Session["DataTable"] = grdInv.DataSource;
        grdBulkInv.DataSource = GetBulkDataSet().Tables["Bulk_Item_Detail"];
        grdBulkInv.DataBind();
        Session["DataTable"] = grdBulkInv.DataSource;
    }
    private DataSet GetDataSet()
    {
        string selectSQL;
        selectSQL = "select iid.item_detail_id as Item_Detail_ID, p.product_desc, ii.item_cond, iid.available_quantity, iid.reorder_quantity, case sl.location_id when 1 then 'St. Louis' when 2 then 'St. Charles' when 3 then 'Bridgeton' when 4 then 'Kansas City' ELSE 'Kirkwood' end as Store_Location, iid.item_location_address, i.is_discontinued from item_inventory ii inner join item_detail iid on iid.item_detail_id = ii.item_detail_id inner join item i on ii.item_detail_id = i.item_number inner join product p on p.product_code = i.product_code inner join store_location sl on sl.location_id = iid.location_id ";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsInv = new DataSet();
        try
        {
            adapter.Fill(dsInv, "Item_Inventory");
            return dsInv;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of items. ";
            lblResults.Text += err.Message;
            return null;
        }
    }

    private DataSet GetBulkDataSet()
    {
        string selectSQL;
        selectSQL = "select bid.bulk_item_detail_id as Bulk_Item_Detail_ID, p.product_desc as Description, bid.quantity_available as Available_Quantity, bid.reorder_quantity as Reorder_Quantity, case sl.location_id when 1 then 'St. Louis' when 2 then 'St. Charles' when 3 then 'Bridgeton' when 4 then 'Kansas City' ELSE 'Kirkwood' end as Store_Location, bid.item_location_address, bi.is_discontinued from bulk_item_detail bid inner join bulk_item bi on bi.bitem_number = bid.bitem_number inner join product p on p.product_code = bi.product_code inner join store_location sl on sl.location_id = bid.location_id";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsInv = new DataSet();
        try
        {
            adapter.Fill(dsInv, "Bulk_Item_Detail");
            return dsInv;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of items. ";
            lblResults.Text += err.Message;
            return null;
        }
    }

    protected void dgInv_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortGridView(e.SortExpression, false);
    }

    protected void dgInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SortGridView((string)Session["SortColumn"], true);
        grdInv.PageIndex = e.NewPageIndex;
        grdInv.DataBind();
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
        grdInv.DataSource = dataView;
        grdInv.DataBind();
        Session["DataTable"] = ((DataView)grdInv.DataSource).Table;
        Session["SortColumn"] = col;
    }

    protected void dgInv_SelectedIndexChanged(object sender, EventArgs e)
    {

        string selectSQL;
        selectSQL = "select iid.item_detail_id, p.product_desc, ii.item_cond, iid.available_quantity, iid.reorder_quantity, case sl.location_id when 1 then 'St. Louis' when 2 then 'St. Charles' when 3 then 'Bridgeton' when 4 then 'Kansas City' ELSE 'Kirkwood' end as Store_Location, iid.item_location_address, i.is_discontinued from item_inventory ii inner join item_detail iid on iid.item_detail_id = ii.item_detail_id inner join item i on ii.item_detail_id = i.item_number inner join product p on p.product_code = i.product_code inner join store_location sl on sl.location_id = iid.location_id";
        selectSQL += "WHERE ii.serial_number ='" + grdInv.SelectedRow.Cells[1].Text + "'";

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(selectSQL, con);
        OracleDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            lblResults.Text = ", " + reader["item_detail_id"];
            lblResults.Text = ", " + reader["product_desc"];
            lblResults.Text += ", " + reader["item_cond"] + "</b>";
            lblResults.Text += ", " + reader["available_quantity"] + "</b>";
            lblResults.Text += ", " + reader["reorder_quantity"] + "</b>";
            lblResults.Text += ", " + reader["Store_Location"] + "</b>";
            lblResults.Text += ", " + reader["item_location_address"] + "</b>";
            lblResults.Text += ", " + reader["is_discontinued"] + "</b>";            
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

    protected void dgInv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        /*
        string deleteSQL;
        deleteSQL = "delete from employee where employee_id = '" + grdInv.Rows[e.RowIndex].Cells[1].Text + "'";
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(deleteSQL, con);
        try
        {
            con.Open();
            int num = cmd.ExecuteNonQuery();
            lblResults.Text = num.ToString() + " records deleted.";
            FillInvGridView();
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
         
        */
    }

    protected void CreateInv_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Inventory/AddItem.aspx");
    }
    protected void StLouis_Click(object sender, EventArgs e)
    {
        getLocationInventory(1);
        getBulkLocationInventory(1);
    }
    protected void StCharles_Click(object sender, EventArgs e)
    {
        getLocationInventory(2);
        getBulkLocationInventory(2);
    }
    protected void Bridgeton_Click(object sender, EventArgs e)
    {
        getLocationInventory(3);
        getBulkLocationInventory(3);
    }
    protected void KC_Click(object sender, EventArgs e)
    {
        getLocationInventory(4);
        getBulkLocationInventory(4);
    }
    protected void getLocationInventory(int LocationNum)
    {

        grdInv.DataSource = GetLocationDataSet(LocationNum).Tables["Item_Inventory"];
        grdInv.DataBind();
        Session["DataTable"] = grdInv.DataSource;
    }

    protected void getBulkLocationInventory(int LocationNum)
    {

        grdBulkInv.DataSource = GetBulkLocationDataSet(LocationNum).Tables["bulk_item_detail"];
        grdBulkInv.DataBind();
        Session["DataTable"] = grdBulkInv.DataSource;
    }

    private DataSet GetLocationDataSet(int location_num)
    {
        string selectSQL;
        selectSQL = "select iid.item_detail_id, p.product_desc, ii.item_cond, iid.available_quantity, iid.reorder_quantity, case sl.location_id when 1 then 'St. Louis' when 2 then 'St. Charles' when 3 then 'Bridgeton' when 4 then 'Kansas City' ELSE 'Kirkwood' end as Store_Location, iid.item_location_address, i.is_discontinued from item_inventory ii inner join item_detail iid on iid.item_detail_id = ii.item_detail_id inner join item i on ii.item_detail_id = i.item_number inner join product p on p.product_code = i.product_code inner join store_location sl on sl.location_id = iid.location_id ";
        selectSQL += "where sl.location_id = " + location_num;
        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsInv = new DataSet();
        try
        {
            adapter.Fill(dsInv, "Item_Inventory");
            return dsInv;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of items. ";
            lblResults.Text += err.Message;
            return null;
        }
    }
    
   
    private DataSet GetBulkLocationDataSet(int location_num)
    {
        string selectSQL;
        selectSQL = "select bid.bulk_item_detail_id as Bulk_Item_Detail_ID, p.product_desc as Description, bid.quantity_available as Available_Quantity, bid.reorder_quantity as Reorder_Quantity, case sl.location_id when 1 then 'St. Louis' when 2 then 'St. Charles' when 3 then 'Bridgeton' when 4 then 'Kansas City' ELSE 'Kirkwood' end as Store_Location, bid.item_location_address, bi.is_discontinued from bulk_item_detail bid inner join bulk_item bi on bi.bitem_number = bid.bitem_number inner join product p on p.product_code = bi.product_code inner join store_location sl on sl.location_id = bid.location_id ";
        selectSQL += "where sl.location_id = " + location_num;
        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsBulkInv = new DataSet();
        try
        {
            adapter.Fill(dsBulkInv, "Bulk_Item_Detail");
            return dsBulkInv;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of items. ";
            lblResults.Text += err.Message;
            return null;
        }
    }
     

    protected void grdBulkInv_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdBulkInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void LowInvAlert_Click(object sender, EventArgs e)
    {
        
        
        string selectSQL;
        string[] bodyArray = new string[500];
           
        string body = "";
        string returnedString = "";
        selectSQL = "select iid.item_detail_id, i.product_code, trim(p.product_desc) as Description, iid.available_quantity, st.location_address from item_detail iid";
        selectSQL += " inner join item i on i.item_number = iid.item_number inner join";
        selectSQL += " product p on p.product_code = i.product_code inner join";
        selectSQL += " store_location st on st.location_id = iid.location_id";
        selectSQL += " where iid.available_quantity < iid.reorder_quantity";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);
        DataSet dataset = new DataSet();
        adapter.Fill(dataset, "item_detail");
        int rowcount = 0;
        foreach (DataRow row in dataset.Tables["item_detail"].Rows)
        {
          body  = " Item Detail ID:" + row["item_detail_id"] + " Product Code:" + row["product_code"] + " Description:" + row["Description"]
                + " Available Aquantity:" + row["available_quantity"]
                + " Location:" + row["location_address"];          
            rowcount++;
            bodyArray[rowcount] += body ;
        }

        TableBuilder builder = new TableBuilder(rowcount, bodyArray);
        returnedString =  builder.BuildTable();



        MailMessage o = new MailMessage();
        NetworkCredential netCred = new NetworkCredential();
        SmtpClient smtpobj = new SmtpClient();
        smtpobj.EnableSsl = false;
        smtpobj.Credentials = netCred;
        smtpobj.Send(o);
    }


}




