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

public partial class Inventory_AddItem : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillProductList();
        }
    }

    private void FillProductList()
    {
        cboProduct.Items.Clear();

        // Define the Select statement.
        string selectSQL;
        selectSQL = "select product_code, product_desc from product";

        // Define the ADO.NET objects.
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(selectSQL, con);
        OracleDataReader reader;

        // Try to open database and read information.
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            // For each item, add the name to the displayed
            // list box text, and store the ssn in the Value property.
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["product_desc"].ToString();
                newItem.Value = reader["product_code"].ToString();
                cboProduct.Items.Add(newItem);
            }
            reader.Close();
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
    protected void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Define ADO.NET objects.

        string selectSQL;
        selectSQL = "SELECT * FROM product ";
        selectSQL += "WHERE product_code='" + cboProduct.SelectedItem.Value + "'";
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(selectSQL, con);
        OracleDataReader reader;

        // Try to open database and read information.
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();

            // Fill the controls.
            txtDescription.Enabled = true;          
            txtDescription.Text = reader["product_desc"].ToString();            
            reader.Close();
            lblResults.Text = "";
        }
        catch (Exception err)
        {
            lblResults.Text = "Error getting author. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }

    protected void cmdNew_Click(object sender, EventArgs e)
    {
        txtDescription.Text = "";
        txtDescription.Enabled = true;
              
        lblResults.Text = "Click Insert New to add the completed record.";
    }
    protected void cmdInsert_Click(object sender, EventArgs e)
    {
        // Perform user-defined checks.
        if (txtDescription.Text == "" )
        {
            lblResults.Text = "Records require an Description.";
            return;
        }

        // Define ADO.NET objects.
        string insertSQL;
        int cde;
        insertSQL = "INSERT INTO product (";
        insertSQL += "product_desc) ";
        insertSQL += "VALUES (";
        insertSQL += ":product_desc)";
        

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(insertSQL, con);        

        // Add the parameters.
        cmd.Parameters.Add(":product_desc", txtDescription.Text);           
       

        // Try to open the database and execute the update.
        int added = 0;
        try
        {
            con.Open();
            added = cmd.ExecuteNonQuery();
            lblResults.Text = added.ToString() + " product inserted.";
            Done.Visible = true;
            txtDescription.Enabled = false;           
           
        }
        catch (Exception err)
        {
            lblResults.Text = "Error inserting product. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        } 
        


        // If the insert succeeded, refresh the author list.
        if (added > 0 )
        {
            FillProductList();
        }
    }
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        // Define ADO.NET objects.       

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;

        cmd.CommandText = "DELETE_PRODUCT";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("product_code", OracleDbType.Int32).Value = cboProduct.SelectedItem.Value;
        // Try to open the database and delete the record.
        int deleted = 0;
        try
        {
            con.Open();
            deleted = cmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            lblResults.Text = "Record deleted.";
            Done.Visible = true;
            txtDescription.Enabled = false;            
        }
        catch (Exception err)
        {
            lblResults.Text = "Error deleting author. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        // If the delete succeeded, refresh the author list.
        if (deleted > 0)
        {
            FillProductList();
        }
    }   
    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        // Define ADO.NET objects.
        string updateSQL;
        updateSQL = "UPDATE Product SET ";
        updateSQL += "product_desc = :product_desc ";
        updateSQL += "WHERE product_code = :productoriginal";

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(updateSQL, con);

        // Add the parameters.        
        cmd.Parameters.Add(":product_desc", txtDescription.Text);

        cmd.Parameters.Add(":productoriginal", cboProduct.SelectedItem.Value);

        // Try to open database and execute the update.
        int updated = 0;
        try
        {
            con.Open();
            updated = cmd.ExecuteNonQuery();
            lblResults.Text = updated.ToString() + " record updated.";
            Done.Visible = true;
            txtDescription.Enabled = false;            
        }
        catch (Exception err)
        {
            lblResults.Text = "Error updating author. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        // If the updated succeeded, refresh the author list.
        if (updated > 0)
        {
            FillProductList();
        }
    }
    protected void Done_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Inventory/Inventory.aspx");
    }
    protected void cboItem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
}