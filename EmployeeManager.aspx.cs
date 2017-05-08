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

public partial class Employees_EmployeeManager : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyOracle"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillEmployeeList();
        }
    }

    private void FillEmployeeList()
    {
        cboEmployee.Items.Clear();

        // Define the Select statement.
        string selectSQL;
        selectSQL = "SELECT employee_id, employee_type, employee_info, pay_rate FROM employee";

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
                newItem.Text = reader["employee_type"] + ", " + reader["employee_info"] + ", " + reader["pay_rate"];
                newItem.Value = reader["employee_id"].ToString();
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
        selectSQL = "SELECT * FROM Employee ";
        selectSQL += "WHERE employee_id='" + cboEmployee.SelectedItem.Value + "'";
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
            txtPay_Rate.Enabled = true;            
            txtEmployee_Type.Enabled = true;
            txtEmployee_Info.Enabled = true;
            txtEmployeeID.Text = reader["employee_id"].ToString();
            txtEmployee_Type.Text = reader["employee_type"].ToString();
            txtEmployee_Info.Text = reader["employee_info"].ToString();
            txtPay_Rate.Text = reader["pay_rate"].ToString();           
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
        txtEmployeeID.Text = "";       
        txtEmployee_Type.Text = "";
        txtEmployee_Type.Enabled = true;
        txtEmployee_Info.Text = "";
        txtEmployee_Info.Enabled = true;
        txtPay_Rate.Text = "";
        txtPay_Rate.Enabled = true;
        lblResults.Text = "Click Insert New to add the completed record.";
    }
    protected void cmdInsert_Click(object sender, EventArgs e)
    {
        // Perform user-defined checks.
        if (txtEmployee_Type.Text == "" || txtEmployee_Info.Text == "" || txtPay_Rate.Text == "")
        {
            lblResults.Text = "Records require an Employee Type, Employee Name and Pay Rate.";
            return;
        }

        // Define ADO.NET objects.
        string insertSQL;
        insertSQL = "INSERT INTO Employee (";
        insertSQL += "employee_type, employee_info, pay_rate)";       
        insertSQL += "VALUES (";
        insertSQL += ":employee_type, :employee_info, :pay_rate)";        

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(insertSQL, con);

        // Add the parameters.
        cmd.Parameters.Add(":employee_type", txtEmployee_Type.Text);
        cmd.Parameters.Add(":employee_info", txtEmployee_Info.Text);
        cmd.Parameters.Add(":pay_rate", txtPay_Rate.Text);        

        // Try to open the database and execute the update.
        int added = 0;
        try
        {
            con.Open();
            added = cmd.ExecuteNonQuery();
            lblResults.Text = added.ToString() + " record inserted.";
            Done.Visible = true;
            txtEmployee_Info.Enabled = false;
            txtEmployee_Type.Enabled = false;
            txtPay_Rate.Enabled = false;
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
            FillEmployeeList();
        }
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        // Define ADO.NET objects.
        string updateSQL;
        updateSQL = "UPDATE Employee SET ";
        updateSQL += "employee_type = :employeetype, employee_info = :employeeinfo, pay_rate = :payrate ";        
        updateSQL += "WHERE employee_id = :empIDoriginal";

        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(updateSQL, con);

        // Add the parameters.        
        cmd.Parameters.Add(":employeetype", txtEmployee_Type.Text);
        cmd.Parameters.Add(":employeeinfo", txtEmployee_Info.Text);
        cmd.Parameters.Add(":payrate", txtPay_Rate.Text);       
        cmd.Parameters.Add(":empIDoriginal", cboEmployee.SelectedItem.Value);

        // Try to open database and execute the update.
        int updated = 0;
        try
        {
            con.Open();
            updated = cmd.ExecuteNonQuery();
            lblResults.Text = updated.ToString() + " record updated.";
            Done.Visible = true;
            txtEmployee_Info.Enabled = false;
            txtEmployee_Type.Enabled = false;
            txtPay_Rate.Enabled = false;
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
            FillEmployeeList();
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
       
        cmd.CommandText = "DELETE_EMPLOYEE";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("employee_id", OracleDbType.Int32).Value = cboEmployee.SelectedItem.Value;
        // Try to open the database and delete the record.
        int deleted = 0;
        try
        {
            con.Open();
            deleted = cmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            lblResults.Text = "Record deleted.";
            Done.Visible = true;
            txtEmployee_Info.Enabled = false;
            txtEmployee_Type.Enabled = false;
            txtPay_Rate.Enabled = false;
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
            FillEmployeeList();
        }
    }
    protected void Done_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Employees/Employees.aspx");
    }
}