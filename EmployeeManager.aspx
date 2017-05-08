<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeManager.aspx.cs" Inherits="Employees_EmployeeManager" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
</head>
<body>
   <div>
        <div style="align-content:center; height: 100%; width: 100%; padding: 10px; background-color:powderblue" runat="server">
			<br />
			<asp:Label id="Label1" runat="server" Width="200px" Height="19px">Select:</asp:Label>
			<asp:DropDownList id="cboEmployee" runat="server" Width="187px" Height="21px" 
                AutoPostBack="True" onselectedindexchanged="cboEmployee_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
			<asp:Button id="cmdUpdate" runat="server" Text="Update" onclick="cmdUpdate_Click"></asp:Button>&nbsp;
			<asp:Button id="cmdDelete" runat="server" Text="Delete" onclick="cmdDelete_Click"></asp:Button>
			<br />
            <br />
			<asp:Label id="Label11" runat="server" Width="200px" Height="19px">Or:</asp:Label>
			<asp:Button id="cmdNew" runat="server" Width="91px" Height="24px" Text="Create New" onclick="cmdNew_Click"></asp:Button>&nbsp;
			<asp:Button id="cmdInsert" runat="server" Width="85px" Height="24px" Text="Insert New" onclick="cmdInsert_Click"></asp:Button>
		</div>
		<br />
		<div style="height: 100%; width: 100%; padding: 10px; vertical-align:top; background-color:powderblue">		
				
			<asp:Label id="Label10" runat="server" Width="200px">Unique ID:</asp:Label>
			<asp:TextBox id="txtEmployeeID" runat="server" Width="250px" Enabled="False"></asp:TextBox> 
			<br />
			
			<asp:Label id="Label2" runat="server" Width="200px">Employee Type:</asp:Label>
			<asp:TextBox id="txtEmployee_Type" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />
			
			<asp:Label id="Label3" runat="server" Width="200px">Employee Name:</asp:Label>
			<asp:TextBox id="txtEmployee_Info" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />
			
			<asp:Label id="Label4" runat="server" Width="200px">Pay Rate:</asp:Label>
			<asp:TextBox id="txtPay_Rate" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />			
			<br />
			
			<asp:Label id="lblResults" runat="server" Width="575px" Height="121px" Font-Bold="True"></asp:Label>
            <asp:Button ID="Done" Visible="false" runat="server" Width="91px" Height="24px" Text="Done" OnClick="Done_Click" />
		</div>
    </div>
</body>
</html>
</asp:Content>


