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

public partial class Customers_CustomerManager : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillCustomerList();
        }
    }

    private void FillCustomerList()
    {
        cboCustomer.Items.Clear();

        // Define the Select statement.
        string selectSQL;
        selectSQL = "select cus.customer_id,";
        selectSQL += " cus.fname as First_Name,";
        selectSQL += " cus.lname as Last_Name,";
        selectSQL += " ad.street,";
        selectSQL += " ad.city,";
        selectSQL += " ad.state,";
        selectSQL += " ad.zip,";
        selectSQL += " ad.address_type,";
        selectSQL += " cus.phone as Phone,";
        selectSQL += " cus.alt_phone as Alt_Phone,";
        selectSQL += " cus.other_info as Other_Info";
        selectSQL += " from customer cus";
        selectSQL += " inner join address1 ad on";
        selectSQL += " ad.customer_id = cus.customer_id";

             
             

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
                newItem.Text = reader["First_Name"] + ", " + reader["Last_Name"]
                    + ", " + reader["Street"] + ", " + reader["City"]
                    + ", " + reader["State"] + ", " + reader["Zip"]
                    + ", " + reader["Address_Type"] + ", " + reader["Phone"]
                    + ", " + reader["Alt_Phone"] + ", " + reader["Other_Info"];
                newItem.Value = reader["customer_id"].ToString();
                cboCustomer.Items.Add(newItem);
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
    protected void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Define ADO.NET objects.
        string selectSQL;
        string selectSQL2;
        selectSQL = "select cus.customer_id,";
        selectSQL += " cus.fname as First_Name,";
        selectSQL += " cus.lname as Last_Name,";
        selectSQL += " ad.street as Street,";
        selectSQL += " ad.city as City,";
        selectSQL += " ad.state as State,";
        selectSQL += " ad.zip as Zip,";
        selectSQL += " cus.phone as Phone,";
        selectSQL += " cus.alt_phone as Alt_Phone,";
        selectSQL += " cus.other_info as Other_Info";
        selectSQL += " from customer cus inner join";
        selectSQL += " address1 ad on ad.customer_id = cus.customer_id";
        selectSQL += " WHERE cus.customer_id = " + cboCustomer.SelectedItem.Value;
        selectSQL += " and ad.address_type = '" + "Home" + "'";

        selectSQL2 = "select ad.customer_id,  ad.street as Alt_Street,";
        selectSQL2 += " ad.city as Alt_City ,";
        selectSQL2 += " ad.state as Alt_State,";
        selectSQL2 += " ad.zip as Alt_Zip, ";
        selectSQL2 += " ad.address_type as Address_Type";        
        selectSQL2 += " from address1 ad";
        selectSQL2 += " WHERE ad.customer_id = " + cboCustomer.SelectedItem.Value;
        selectSQL2 += " and ad.address_type = '" + "Alternate" + "'";
       

        OracleConnection con = new OracleConnection(connectionString);
        OracleConnection con2 = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(selectSQL, con);
        OracleCommand cmd2 = new OracleCommand(selectSQL2, con2);
        OracleDataReader reader;
        OracleDataReader reader2;

        // Try to open database and read information.
        try
        {
            con.Open();
           
            reader = cmd.ExecuteReader();
          
            reader.Read();
          

            // Fill the controls.
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtStreet.Enabled = true;
            txtCity.Enabled = true;
            txtState.Enabled = true;
            txtZip.Enabled = true;
            txtPhone.Enabled = true;
            txtAltPhone.Enabled = true;
            txtOtherInfo.Enabled = true;
            txtAltStreet.Enabled = true;
            txtAltCity.Enabled = true;
            txtAltState.Enabled = true;
            txtALtZip.Enabled = true;
            txtCustomerID.Text = reader["customer_id"].ToString();
            txtFirstName.Text = reader["First_Name"].ToString();
            txtLastName.Text = reader["Last_Name"].ToString();
            txtStreet.Text = reader["Street"].ToString();
            txtCity.Text = reader["City"].ToString();
            txtState.Text = reader["State"].ToString();
            txtZip.Text = reader["Zip"].ToString();
            txtPhone.Text = reader["Phone"].ToString();
            txtAltPhone.Text = reader["Alt_Phone"].ToString();
            txtOtherInfo.Text = reader["Other_Info"].ToString();
           
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

        try
        {
            con2.Open();
            reader2 = cmd2.ExecuteReader();
            reader2.Read();

            txtAltStreet.Text = reader2["Alt_Street"].ToString();
            txtAltCity.Text = reader2["Alt_City"].ToString();
            txtAltState.Text = reader2["Alt_State"].ToString();
            txtALtZip.Text = reader2["Alt_Zip"].ToString();

            reader2.Close();
        }
        catch (Exception err)
        {
            lblResults.Text = "Error getting author. ";
            lblResults.Text += err.Message;
        }
        finally 
        {
            con2.Close();
        }
    }
    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        string updateSQL, updateSql2, updateSql3, updateSql4;
        int updated = 0;

        updateSQL = "Update Customer set";
        updateSQL += " fname = :first_name, lname = :last_name, phone = :phone, alt_phone = :alt_phone,";
        updateSQL += " other_info = :other_info WHERE customer_id = :customeroriginal";

        updateSql2 = "update address1 set";
        updateSql2 += " street = :street, city = :city, state = :state, zip = :zip";
        updateSql2 += " where customer_id = :customeroriginal and";
        updateSql2 += " address_type = '" + "Home" + "'";

        updateSql3 = "update address1 set";
        updateSql3 += " street = :street, city = :city, state = :state, zip = :zip";
        updateSql3 += " where customer_id = :customeroriginal and";
        updateSql3 += " address_type = '" + "Alternate" + "'";      


        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(updateSQL, con);

        cmd.Parameters.Add(":first_name", txtFirstName.Text);
        cmd.Parameters.Add(":last_name", txtLastName.Text);
        cmd.Parameters.Add(":phone", txtPhone.Text);
        cmd.Parameters.Add(":alt_phone", txtAltPhone.Text);
        cmd.Parameters.Add(":other_info", txtOtherInfo.Text);
        cmd.Parameters.Add(":customeroriginal", cboCustomer.SelectedItem.Value);


        try
        {
            con.Open();
            updated = cmd.ExecuteNonQuery();
            lblResults.Text = updated.ToString() + " record updated.";
            Done.Visible = true;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtPhone.Enabled = false;
            txtAltPhone.Enabled = false;
            txtOtherInfo.Enabled = false;
        }

        catch (Exception err)
        {
            lblResults.Text = "Error updating record. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        OracleConnection con4 = new OracleConnection(connectionString);
        OracleCommand cmd4 = new OracleCommand(updateSql2, con4);

        cmd4.Parameters.Add(":street", txtStreet.Text);
        cmd4.Parameters.Add(":city", txtCity.Text);
        cmd4.Parameters.Add(":state", txtState.Text);
        cmd4.Parameters.Add(":zip", txtZip.Text);
        cmd4.Parameters.Add(":customeroriginal", cboCustomer.SelectedItem.Value);

        try
        {
            con4.Open();
            updated = cmd4.ExecuteNonQuery();
            lblResults.Text = updated.ToString() + " record updated.";
            Done.Visible = true;
            txtStreet.Enabled = false;
            txtCity.Enabled = false;
            txtState.Enabled = false;
            txtZip.Enabled = false;            
        }

        catch (Exception err)
        {
            lblResults.Text = "Error updating record. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con4.Close();
        }



        if (txtAltStreet.Text == "")
        {
            OracleConnection con2 = new OracleConnection(connectionString);
            OracleCommand cmd2 = new OracleCommand();           

            cmd2.CommandText = "ADD_ADDRESS1";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("customer_id", OracleDbType.Int32).Value = cboCustomer.SelectedItem.Value;
            cmd2.Parameters.Add("address_type", OracleDbType.Varchar2).Value = "Alternate";
            cmd2.Parameters.Add("street", OracleDbType.Varchar2).Value = txtAltStreet.Text;
            cmd2.Parameters.Add("city", OracleDbType.Varchar2).Value = txtAltCity.Text;
            cmd2.Parameters.Add("zip", OracleDbType.Varchar2).Value = txtALtZip.Text;

            // Try to open the database and delete the record.
            int added = 0;
            try
            {
                con2.Open();
                added = cmd2.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                lblResults.Text = "Record added.";
                Done.Visible = true;
                txtAltStreet.Enabled = false;
                txtAltCity.Enabled = false;
                txtAltState.Enabled = false;
                txtALtZip.Enabled = false;
            }
            catch (Exception err)
            {
                lblResults.Text = "Error adding record. ";
                lblResults.Text += err.Message;
            }
            finally
            {
                con2.Close();
            }
        }

        else 
        {
            OracleConnection con3 = new OracleConnection(connectionString);
            OracleCommand cmd3 = new OracleCommand(updateSql3, con3);

            cmd3.Parameters.Add(":street", txtAltStreet.Text);
            cmd3.Parameters.Add(":city", txtAltCity.Text);
            cmd3.Parameters.Add(":state", txtAltState.Text);
            cmd3.Parameters.Add(":zip", txtALtZip.Text);            
            cmd3.Parameters.Add(":customeroriginal", cboCustomer.SelectedItem.Value);
            

            try
            {
                con3.Open();
                updated = cmd3.ExecuteNonQuery();
                lblResults.Text = updated.ToString() + " record updated.";
                Done.Visible = true;
                txtAltStreet.Enabled = false;
                txtAltCity.Enabled = false;
                txtAltState.Enabled = false;
                txtALtZip.Enabled = false;
            }

            catch (Exception err)
            {
                lblResults.Text = "Error updating record. ";
                lblResults.Text += err.Message;
            }
            finally
            {
                con3.Close();
            }

        }      


        if (updated > 0)
        {
            FillCustomerList();
        }
        

    }
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;

        cmd.CommandText = "DELETE_CUSTOMER";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("customer_id", OracleDbType.Int32).Value = cboCustomer.SelectedItem.Value;
        // Try to open the database and delete the record.
        int deleted = 0;
        try
        {
            con.Open();
            deleted = cmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            lblResults.Text = "Record deleted.";
            Done.Visible = true;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtStreet.Enabled = false;
            txtCity.Enabled = false;
            txtState.Enabled = false;
            txtZip.Enabled = false;
            txtPhone.Enabled = false;
            txtAltPhone.Enabled = false;
            txtOtherInfo.Enabled = false;
            lblALtStreet.Enabled = false;
            txtAltCity.Enabled = false;
            txtAltState.Enabled = false;
            txtALtZip.Enabled = false;
        }
        catch (Exception err)
        {
            lblResults.Text = "Error deleting record. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        // If the delete succeeded, refresh the author list.
        if (deleted > 0)
        {
            FillCustomerList();
        }
    }
    protected void cmdNew_Click(object sender, EventArgs e)
    {
        txtFirstName.Enabled = true;
        txtLastName.Enabled = true;
        txtStreet.Enabled = true;
        txtCity.Enabled = true;
        txtState.Enabled = true;
        txtZip.Enabled = true;
        txtPhone.Enabled = true;
        txtAltPhone.Enabled = true;
        txtOtherInfo.Enabled = true;
        lblALtStreet.Enabled = true;
        txtAltCity.Enabled = true;
        txtAltState.Enabled = true;
        txtALtZip.Enabled = true;
        txtCustomerID.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtStreet.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtZip.Text = "";
        txtPhone.Text = "";
        txtAltPhone.Text = "";
        txtOtherInfo.Text = "";
        txtAltStreet.Text = "";
        txtAltCity.Text = "";
        txtAltState.Text = "";
        txtALtZip.Text = "";
        lblResults.Text = "Click Insert New to add the completed record.";
    }
    protected void cmdInsert_Click(object sender, EventArgs e)
    {

    }
    protected void Done_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Customers/Customers.aspx");
    }
}