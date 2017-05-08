<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="Inventory_Inventory" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
        <title></title>
    </head>
    <body>
    <br />    <asp:Label ID="Label1" runat="server" Text="Welcome to the Inventory Page" Font-Bold="true"></asp:Label>  
     
    <asp:Table ID="table2" runat="server" Height="100%" Width="100%" Font-Bold="true"> 
        <asp:TableRow>
            <asp:TableCell>
                 <asp:table ID="Table1" runat="server">
                   <asp:tableRow>
                      <asp:tablecell>
                         <br /> <asp:Label ID="Label2" runat="server" Text="Large Items"></asp:Label> 
                          <div id="scrollList" style="height:250px; width:1300px; overflow:auto" >
                           <br />  <asp:GridView Width="1300px" id="grdInv" BackColor="PowderBlue"  runat="server" AutoGenerateColumns="false" DataKeyNames="Item_Detail_ID" OnSelectedIndexChanged="dgInv_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="dgInv_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                           <Columns> 
                               <asp:CommandField ShowSelectButton="false"></asp:CommandField>
                               <asp:BoundField  DataField="Item_Detail_ID" HeaderText="Item Detail ID" ReadOnly="true" SortExpression="Item_Detail_ID" />
                               <asp:BoundField  DataField="product_desc"  HeaderText="Product Description" ReadOnly="true" SortExpression="product_desc" />
                               <asp:BoundField  DataField="item_cond" HeaderText="Item Condition" ReadOnly="true" SortExpression="item_cond" />
                               <asp:BoundField  DataField="available_quantity" HeaderText="Available Quantity" ReadOnly="true" SortExpression="available_quantity" />  
                               <asp:BoundField  DataField="reorder_quantity" HeaderText="Reorder Quantity" ReadOnly="true" SortExpression="reorder_quantity" />
                               <asp:BoundField  DataField="Store_Location" HeaderText="Store Location" ReadOnly="true" SortExpression="Store_Location" />
                               <asp:BoundField  DataField="item_location_address" HeaderText="Item Location Address" ReadOnly="true" SortExpression="item_location_address" />
                               <asp:BoundField  DataField="is_discontinued" HeaderText="Discontinued?" ReadOnly="true" SortExpression="is_discontinued" />
                           </Columns>
                       </asp:GridView>
                          </div>      
                  
                 </asp:tablecell>  
                       </asp:tableRow> 
                   <asp:tableRow>
                        <asp:tablecell>
                           <br />  <asp:Label ID="Label3" runat="server" Text="Bulk Items"></asp:Label>  
                            <div id="scrollList2" style="height:250px; width:1300px; overflow:auto">
                              <br />   <asp:GridView Width="1300px" BackColor="PowderBlue"  id="grdBulkInv" runat="server" AutoGenerateColumns="false" DataKeyNames="bulk_item_detail_id" OnSelectedIndexChanged="grdBulkInv_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="grdBulkInv_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                           <Columns>
                               <asp:CommandField ShowSelectButton="false"></asp:CommandField>
                               <asp:BoundField  DataField="bulk_item_detail_id" HeaderText="Bulk Item Detail ID" ReadOnly="true" SortExpression="bulk_item_detail_id" />
                               <asp:BoundField  DataField="Description"  HeaderText="Description" ReadOnly="true" SortExpression="Description" />
                               <asp:BoundField  DataField="Available_Quantity" HeaderText="Available Quantity" ReadOnly="true" SortExpression="Available_Quantity" />
                               <asp:BoundField  DataField="Reorder_Quantity" HeaderText="Reorder Quantity" ReadOnly="true" SortExpression="Reorder_Quantity" /> 
                               <asp:BoundField  DataField="Store_Location" HeaderText="Store Location" ReadOnly="true" SortExpression="Store_Location" />
                               <asp:BoundField  DataField="item_location_address" HeaderText="Item Location Address" ReadOnly="true" SortExpression="item_location_address" />
                               <asp:BoundField  DataField="is_discontinued" HeaderText="Discontinued?" ReadOnly="true" SortExpression="is_discontinued" />
                           </Columns>
                       </asp:GridView> 
                            </div>
                        
                 </asp:tablecell>                          
                         
                   </asp:tableRow>

               </asp:table>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top"><br />
                <asp:Button Width="175px" ID="CreateInv" runat="server" CssClass="btn" Text="Products" OnClick="CreateInv_Click" Font-Size="Small" /> <br />
                        <asp:Button Width="175px" ID="StLouis" runat="server" CssClass="btn" Text="St. Louis" OnClick="StLouis_Click" Font-Size="Small" /> <br />
                        <asp:Button Width="175px" ID="StCharles" runat="server" CssClass="btn" Text="St. Charles" OnClick="StCharles_Click" Font-Size="Small" /><br />                       
                        <asp:Button Width="175px" ID="Bridgeton" runat="server" CssClass="btn" Text="Bridgeton" OnClick="Bridgeton_Click" Font-Size="Small" /> <br />                     
                        <asp:Button Width="175px" ID="KC" runat="server" CssClass="btn" Text="Kansas City" OnClick="KC_Click" Font-Size="Small" />  <br />
                        <asp:Button Width="175px" ID="LowInvAlert" runat="server" CssClass="btn" Text="Inv. Alert" OnClick="LowInvAlert_Click" ForeColor="Red"  Font-Size="Small"/> 
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
              
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
    <asp:Label ID="lblResults" runat="server"></asp:Label>    
    </body>
    </html>
</asp:Content>
