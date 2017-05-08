<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CustomerManager.aspx.cs" Inherits="Customers_CustomerManager" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
       <div>
        <div style=" height: 50%; width: 100%; padding: 10px; background-color:powderblue">
			<br />
			<asp:Label id="Label1" runat="server" Width="99px" Height="19px">Select:</asp:Label>
			<asp:DropDownList id="cboCustomer" runat="server" Width="187px" Height="21px" 
                AutoPostBack="True" onselectedindexchanged="cboCustomer_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
			<asp:Button id="cmdUpdate" runat="server" Text="Update" onclick="cmdUpdate_Click"></asp:Button>&nbsp;
			<asp:Button id="cmdDelete" runat="server" Text="Delete" onclick="cmdDelete_Click"></asp:Button>
			<br />
            <br />
			<asp:Label id="Label11" runat="server" Width="99px" Height="19px">Or:</asp:Label>
			<asp:Button id="cmdNew" runat="server" Width="91px" Height="24px" Text="Create New" onclick="cmdNew_Click"></asp:Button>&nbsp;
			<asp:Button id="cmdInsert" runat="server" Width="85px" Height="24px" Text="Insert New" onclick="cmdInsert_Click"></asp:Button>
		</div>
		<br />
		<div style=" height: 50%; width: 100%; padding: 10px; vertical-align:top; background-color:powderblue"" >		
			<asp:Table runat="server" ID="addtbl">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label id="Label10" runat="server" Width="150px">Unique ID:</asp:Label>
			<asp:TextBox id="txtCustomerID" runat="server" Width="250px" Enabled="False"></asp:TextBox>&nbsp; 
			<br />			
            <asp:Label id="Label2" runat="server" Width="150px">First Name:</asp:Label>
			<asp:TextBox id="txtFirstName" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />			
			<asp:Label id="Label3" runat="server" Width="150px">Last Name:</asp:Label>
			<asp:TextBox id="txtLastName" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />			
			<asp:Label id="Label4" runat="server" Width="150px">Street:</asp:Label>
			<asp:TextBox id="txtStreet" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />
            <asp:Label id="Label6" runat="server" Width="150px">City:</asp:Label>
			<asp:TextBox id="txtCity" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />
            <asp:Label id="Label7" runat="server" Width="150px">State:</asp:Label>
			<asp:TextBox id="txtState" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />
            <asp:Label id="Label8" runat="server" Width="150px">Zip:</asp:Label>
			<asp:TextBox id="txtZip" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />			
			<asp:Label id="Label5" runat="server" Width="150px">Phone:</asp:Label>
			<asp:TextBox id="txtPhone" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />
            <asp:Label id="Label9" runat="server" Width="150px">Alt Phone:</asp:Label>
			<asp:TextBox id="txtAltPhone" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />				
            <asp:Label id="Label12" runat="server" Width="150px">Other Info:</asp:Label>
			<asp:TextBox id="txtOtherInfo" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />			
			<br />
                    </asp:TableCell>
                    <asp:TableCell>
                         <asp:Label ID="lblAlternateAddress" runat="server" Width="250px">Alternate Address:</asp:Label>
            <br />
            <asp:Label ID="lblALtStreet" runat="server" width="150px">Street</asp:Label>
            <asp:TextBox id="txtAltStreet" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />
            <asp:Label id="Label13" runat="server" Width="150px">City:</asp:Label>
			<asp:TextBox id="txtAltCity" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />
            <asp:Label id="Label14" runat="server" Width="150px">State:</asp:Label>
			<asp:TextBox id="txtAltState" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />
            <asp:Label id="Label15" runat="server" Width="150px">Zip:</asp:Label>
			<asp:TextBox id="txtALtZip" runat="server" Width="250px" Enabled="False"></asp:TextBox><br />						
			<asp:Label id="lblResults" runat="server" Width="575px" Height="121px" Font-Bold="True"></asp:Label>
            <asp:Button ID="Done" Visible="false" runat="server" Width="91px" Height="24px" Text="Done" OnClick="Done_Click" />
                    </asp:TableCell>
                </asp:TableRow>
			</asp:Table>	
			
           
		</div>
    </div>
</body>
</html>
</asp:Content>


