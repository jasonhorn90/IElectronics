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

public partial class Reports_Reports : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    private void FillReportGridView()
    {
        GridView1.DataSource = GetReportDataSet().Tables["sales_transaction"];
        GridView1.DataBind();
        Session["DataTable"] = GridView1.DataSource;
    }

    private DataSet GetReportDataSet()
    {
        string selectSQL;
        selectSQL = "select distinct b.item_number, trim(p.product_desc) as product_desc, a.NumSold * aa.quantity as TotalSold, st.transaction_date from";
        selectSQL += " (select item_detail_id, count(item_detail_id) as NumSold from Sales_Transaction_Detail group by item_detail_id ) a join";
        selectSQL += " (select item_detail_id, quantity from Sales_Transaction_Detail) aa on";
        selectSQL += " aa.item_detail_id = a.item_detail_id join";
        selectSQL += " (select std_id, sales_transaction_id, item_detail_id from sales_Transaction_Detail) aaa on";
        selectSQL += " aaa.item_detail_id = aa.item_detail_id join";
        selectSQL += " (select item_number, item_detail_id from item_detail) b on";
        selectSQL += " b.item_detail_id = a.item_detail_id join";
        selectSQL += " (select item_number, product_code from item) i on";
        selectSQL += " i.item_number = b.item_number join";
        selectSQL += " (select product_code, product_desc from product) p on";
        selectSQL += " p.product_code = i.product_code join";
        selectSQL += " (select sales_transaction_id, transaction_date from sales_transaction) st on";
        selectSQL += " st.sales_transaction_id = aaa.sales_transaction_id inner join";
        selectSQL += " sales_transaction sst on sst.sales_transaction_id = st.sales_transaction_id join product pp on pp.product_code = p.product_code";
        selectSQL += " WHERE st.transaction_date between '" + TextBox1.Text + "' and '" + TextBox2.Text + "'";

        OracleConnection con = new OracleConnection(connectionString);
        OracleDataAdapter adapter = new OracleDataAdapter(selectSQL, con);

        DataSet dsInv = new DataSet();
        try
        {
            adapter.Fill(dsInv, "sales_transaction");
            return dsInv;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading list of items. ";
            lblResults.Text += err.Message;
            return null;
        }
    }
    protected void Go_Click(object sender, EventArgs e)
    {
        FillReportGridView();
    }
}