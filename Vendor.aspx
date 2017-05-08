<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Vendor.aspx.cs" Inherits="Vendor_Vendor" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" >
    <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
    <title></title>
</head>
<body>
 <br />   <asp:Label ID="Label1" runat="server" Text="Welcome to the Vendor Page" Font-Bold="true"></asp:Label>  
     
    <asp:Table ID="table2" runat="server" Height="100%" Width="100%" Font-Bold="true">
        <asp:TableRow>
            <asp:TableCell>
                 <asp:table ID="Table1" runat="server">
                   <asp:TableRow>
                       <asp:TableCell>
                        <br />   <asp:Label ID="Label2" runat="server" Text="Vendor Master List"></asp:Label> 
                           <div id="ScrollList" style="height: 550px; width:1300px; overflow: auto">
                         <br />   <asp:GridView id="grdVendor"  BackColor="PowderBlue" Width="1300px" runat="server" AutoGenerateColumns="false" DataKeyNames="VendorID" OnSelectedIndexChanged="grdVendor_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="grdVendor_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                                <Columns>
                                <asp:CommandField ShowSelectButton="false"></asp:CommandField>
                               <asp:BoundField  DataField="VendorID" HeaderText="VendorID" ReadOnly="true" SortExpression="VendorID" />
                               <asp:BoundField  DataField="VendorName" HeaderText="VendorName" ReadOnly="true" SortExpression="VendorName" />
                               <asp:BoundField  DataField="Address" HeaderText="Address" ReadOnly="true" SortExpression="Address" />
                               <asp:BoundField  DataField="Phone" HeaderText="Phone" ReadOnly="true" SortExpression="Phone" />
                               <asp:BoundField  DataField="Alt_Phone" HeaderText="Alt_Phone" ReadOnly="true" SortExpression="Alt_Phone" />                                         
                                </Columns>
                            </asp:GridView>
                           </div>                            
                       </asp:TableCell>
                   </asp:TableRow>
                   <asp:TableRow>
                       <asp:TableCell>                   
                          
                           
                       </asp:TableCell>                       
                   </asp:TableRow>
               </asp:table>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top">
                <asp:Button Width="175px" ID="CreateVendor" runat="server" CssClass="btn" Text="Edit Vendor" OnClick="CreateVendor_Click" Font-Size="Small"/> 
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
              
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
    <asp:Label ID="lblResults" runat="server"></asp:Label>    
</body>
</html>
</asp:Content>


