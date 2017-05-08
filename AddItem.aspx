<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddItem.aspx.cs" Inherits="Inventory_AddItem" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
    <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
    <title></title>
</head>
<body>
   
        <div style="height: 100%; width:100%; padding: 10px; background-color:powderblue""  >
			<br />
			<asp:Label id="Label1" runat="server" Width="200px" Height="19px">Product Name:</asp:Label>
			<asp:DropDownList id="cboProduct" runat="server" Width="187px" Height="21px" 
                AutoPostBack="True" onselectedindexchanged="cboProduct_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
			<asp:Button id="cmdUpdate" runat="server" Text="Update" onclick="cmdUpdate_Click"></asp:Button>&nbsp;
			<asp:Button id="cmdDelete" runat="server" Text="Delete" onclick="cmdDelete_Click"></asp:Button>
			<br />
            <br />
			<asp:Label id="Label11" runat="server" Width="200px" Height="19px">Or:</asp:Label>
			<asp:Button id="cmdNew" runat="server" Width="91px" Height="24px" Text="Create New" onclick="cmdNew_Click"></asp:Button>&nbsp;
			<asp:Button id="cmdInsert" runat="server" Width="85px" Height="24px" Text="Insert New" onclick="cmdInsert_Click"></asp:Button>
		</div>
		<br />
		<div style=" height: 321px; width: 100%; padding: 10px; background-color:powderblue"" >		
				
			<asp:Label id="Label10" runat="server" Width="200px">Description:</asp:Label>
			<asp:TextBox id="txtDescription" runat="server" Width="250px" Enabled="False"></asp:TextBox> 
			<br />
            			
			<asp:Label id="lblResults" runat="server" Width="575px" Height="121px" Font-Bold="True"></asp:Label>
            <asp:Button ID="Done" Visible="false" runat="server" Width="91px" Height="24px" Text="Done" OnClick="Done_Click" />
		</div>
       
</body>
</html>
</asp:Content>


