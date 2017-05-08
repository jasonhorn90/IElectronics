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

public partial class Vendor_VendorManager : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillVendorList();
        }
    }

    private void FillVendorList()
    {
        cboEmployee.Items.Clear();

        // Define the Select statement.
        string selectSQL;
        selectSQL = "select vendor_id , vendorname , street,  city ,  state , zip , phone , alt_phone from vendor";

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
                newItem.Text = reader["vendorname"] + ", " + reader["street"] + ", " + reader["city"] + ", " + reader["state"] + ", " + reader["zip"] + ", " + reader["phone"] + ", " + reader["alt_phone"];
                newItem.Value = reader["vendor_id"].ToString();
                cboEmployee.Items.Add(newItem);
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

    protected void cboEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Define ADO.NET objects.

        string selectSQL;
        selectSQL = "SELECT * FROM Vendor ";
        selectSQL += "WHERE vendor_id='" + cboEmployee.SelectedItem.Value + "'";
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
            txtVendorName.Enabled = true;
            txtStreet.Enabled = true;
            txtCity.Enabled = true;
            txtState.Enabled = true;
            txtZip.Enabled = true;
            txtPhone.Enabled = true;
            txtAltPhone.Enabled = true;
            txtVendorID.Text = reader["vendor_id"].ToString();
            txtVendorName.Text = reader["VendorName"].ToString();
            txtStreet.Text = reader["Street"].ToString();
            txtCity.Text = reader["City"].ToString();
            txtState.Text = reader["State"].ToString();
            txtZip.Text = reader["Zip"].ToString();
            txtPhone.Text = reader["Phone"].ToString();
            txtAltPhone.Text = reader["Alt_Phone"].ToString();
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
        txtVendorID.Text = "";
        txtVendorName.Enabled = true;
        txtVendorName.Text = "";
        txtStreet.Enabled = true;
        txtStreet.Text = "";
        txtCity.Enabled = true;
        txtCity.Text = "";
        txtState.Enabled = true;
        txtState.Text = "";
        txtZip.Enabled = true;
        txtZip.Text = "";
        txtPhone.Enabled = true;
        txtPhone.Text = "";
        txtAltPhone.Enabled = true;
        txtAltPhone.Text = "";
        lblResults.Text = "Click Insert New to add the completed record.";

    }
    protected void cmdInsert_Click(object sender, EventArgs e)
    {
         // Perform user-defined checks.
        if (txtVendorName.Text == "" || txtStreet.Text == "" || txtCity.Text == "" || txtState.Text == "" || txtZip.Text == "" ||  txtPhone.Text == "")
        {
            lblResults.Text = "Records require an Vendor Name, All Address Fields and Phone nUmber.";
            return;
        }

        // Define ADO.NET objects.
        string insertSQL;
        insertSQL = "INSERT INTO Vendor (";
        insertSQL += "vendorname, street, city, state, zip, phone, alt_phone) ";       
        insertSQL += "VALUES (";
        insertSQL += ":vendorname, :street, :city, :state, :zip, :phone, :alt_phone)";        

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(insertSQL, con);

        // Add the parameters.
        cmd.Parameters.Add(":vendorname", txtVendorName.Text);
        cmd.Parameters.Add(":street", txtStreet.Text);
        cmd.Parameters.Add(":city", txtCity.Text);
        cmd.Parameters.Add(":state",txtState.Text);
        cmd.Parameters.Add(":zip", txtZip.Text);
        cmd.Parameters.Add(":phone", txtPhone.Text);
        cmd.Parameters.Add(":alt_phone",  txtAltPhone.Text);

        // Try to open the database and execute the update.
        int added = 0;
        try
        {
            con.Open();
            added = cmd.ExecuteNonQuery();
            lblResults.Text = added.ToString() + " record inserted.";
            Done.Visible = true;
            txtVendorName.Enabled = false;
            txtStreet.Enabled = false;
            txtCity.Enabled = false;
            txtState.Enabled = false;
            txtZip.Enabled = false;
            txtPhone.Enabled = false;
            txtAltPhone.Enabled = false;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error inserting record. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        // If the insert succeeded, refresh the author list.
        if (added > 0)
        {
            FillVendorList();
        }
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        // Define ADO.NET objects.
        string updateSQL;
        updateSQL = "UPDATE Vendor SET ";
        updateSQL += "vendorname = :vendorname, street = :street, city = :city, state = :state, zip = :zip, phone = :phone, alt_phone = :alt_phone ";
        updateSQL += "WHERE vendor_id = :vendoriginal";

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(updateSQL, con);

        // Add the parameters.        
        cmd.Parameters.Add(":vendorname", txtVendorName.Text);
        cmd.Parameters.Add(":street", txtStreet.Text);
        cmd.Parameters.Add(":city", txtCity.Text);
        cmd.Parameters.Add(":state", txtState.Text);
        cmd.Parameters.Add(":zip", txtZip.Text);
        cmd.Parameters.Add(":phone", txtPhone.Text);
        cmd.Parameters.Add(":alt_phone", txtAltPhone.Text);
        cmd.Parameters.Add(":vendoriginal", cboEmployee.SelectedItem.Value);

        // Try to open database and execute the update.
        int updated = 0;
        try
        {
            con.Open();
            updated = cmd.ExecuteNonQuery();
            lblResults.Text = updated.ToString() + " record updated.";
            Done.Visible = true;
            txtVendorName.Enabled = false;
            txtStreet.Enabled = false;
            txtCity.Enabled = false;
            txtState.Enabled = false;
            txtZip.Enabled = false;
            txtPhone.Enabled = false;
            txtAltPhone.Enabled = false;
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
            FillVendorList();
        }
    }
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        // Define ADO.NET objects.
        //   string deleteSQL;
        // deleteSQL = "DELETE FROM Employee ";
        //  deleteSQL += "WHERE employee_id=:employee_id";

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;

        cmd.CommandText = "DELETE_VENDOR";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("vendor_id", OracleDbType.Int32).Value = cboEmployee.SelectedItem.Value;
        // Try to open the database and delete the record.
        int deleted = 0;
        try
        {
            con.Open();
            deleted = cmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            lblResults.Text = "Record deleted.";
            Done.Visible = true;
            txtVendorName.Enabled = false;
            txtStreet.Enabled = false;
            txtCity.Enabled = false;
            txtState.Enabled = false;
            txtZip.Enabled = false;
            txtPhone.Enabled = false;
            txtAltPhone.Enabled = false;
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
            FillVendorList();
        }
    }
    protected void Done_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Vendor/Vendor.aspx");
    }
}