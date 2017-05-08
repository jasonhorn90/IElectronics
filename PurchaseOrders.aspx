<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrders.aspx.cs" Inherits="Purchasing_PurchaseOrders" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
    <title></title>
</head>
<body>
  <br />  <asp:Label ID="Label1" runat="server" Text="Welcome to the Purchase Order Page" Font-Bold="true"></asp:Label>  
     
    <asp:Table ID="table2" runat="server" Height="100%" Width="100%" Font-Bold="true">
        <asp:TableRow>
            <asp:TableCell>
                 <asp:table ID="Table1" runat="server">
                   <asp:TableRow>
                       <asp:TableCell>
                           <br /><asp:Label ID="Label2" runat="server" Text="Purchase Order Header"></asp:Label> 
                           <div id="ScrollList" style="height: 250px; width:1300px; overflow: auto">
                           <br /> <asp:GridView id="grdPurchHeader" BackColor="PowderBlue"  Height="350px" Width="1300px" runat="server" AutoGenerateColumns="false" DataKeyNames="PurchaseID" OnSelectedIndexChanged="grdPurchHeader_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="grdPurchHeader_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                                <Columns>
                                <asp:CommandField ShowSelectButton="true"></asp:CommandField>
                               <asp:BoundField  DataField="PurchaseID" HeaderText="Purchase ID" ReadOnly="true" SortExpression="PurchaseID" />
                               <asp:BoundField  DataField="Vendor" HeaderText="Vendor" ReadOnly="true" SortExpression="Vendor" />
                               <asp:BoundField  DataField="PO_Date" HeaderText="Purchase Order Date" ReadOnly="true" SortExpression="PO_Date" />
                               <asp:BoundField  DataField="other_info" HeaderText="Other Info" ReadOnly="true" SortExpression="other_info" />                                         
                                </Columns>
                            </asp:GridView>
                           </div>                            
                       </asp:TableCell>
                   </asp:TableRow>
                   <asp:TableRow>
                       <asp:TableCell>
                         <br />  <asp:Label ID="Label3" runat="server" Text="Purchase Order Detail"></asp:Label> 
                           <div id="ScrollList2" style="height: 250px; width:1300px; overflow: auto">
                            <br />   <asp:GridView id="grdPurchDetail" BackColor="PowderBlue"  Height="150" Width="1300px" runat="server" AutoGenerateColumns="false" DataKeyNames="PODetail_ID" OnSelectedIndexChanged="grdPurchDetail_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="grdPurchDetail_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                                <Columns>
                               <asp:BoundField  DataField="PODetail_ID" HeaderText="PO Detail ID" ReadOnly="true" SortExpression="PurchaseID" />
                               <asp:BoundField  DataField="Purchase_Order_ID" HeaderText="Purchase Order ID" ReadOnly="true" SortExpression="Purchase_Order_ID" />
                               <asp:BoundField  DataField="Item_Number" HeaderText="Item Number" ReadOnly="true" SortExpression="Item_Number" />
                               <asp:BoundField  DataField="Description" HeaderText="Description" ReadOnly="true" SortExpression="Description" />
                               <asp:BoundField  DataField="Quantity" HeaderText="Quantity" ReadOnly="true" SortExpression="Quantity" /> 
                               <asp:BoundField  DataField="Price" HeaderText="Price" ReadOnly="true" SortExpression="Price" />                                          
                               <asp:BoundField  DataField="Total" HeaderText="Total" ReadOnly="true" SortExpression="Total" />                                              
                                </Columns>
                            </asp:GridView>
                           </div>
                           
                       </asp:TableCell>                       
                   </asp:TableRow>
               </asp:table>
            </asp:TableCell>            
        </asp:TableRow>
    </asp:Table>
              
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
    <asp:Label ID="lblResults" runat="server"></asp:Label>    
</body>
</html>
</asp:Content>