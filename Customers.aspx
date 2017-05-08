<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="Customers_Customers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
    <title></title>
</head>
<body>
 <br />    <asp:Label ID="Label1" runat="server" Text="Welcome to the Customer Page" Font-Bold="true"></asp:Label>   
     
    <asp:Table ID="table2" runat="server" Height="100%" Width="100%" Font-Bold="true">
        <asp:TableRow>
            <asp:TableCell>
                <asp:table ID="Table1" runat="server">
                   <asp:tablerow>
                      <asp:tablecell>
                        <br />  <asp:Label ID="Label2" runat="server" Text="Customer Master List"></asp:Label> 
                           <div id="ScrollList" style="height: 250px; width:1300px; overflow: auto">
                              <br />   <asp:GridView Width="1300px" BackColor="PowderBlue"   id="grdCustomer" runat="server" AutoGenerateColumns="false" DataKeyNames="customer_id" OnSelectedIndexChanged="grdCustomer_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="grdCustomer_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                           <Columns>
                               <asp:CommandField ShowSelectButton="true"></asp:CommandField>
                               <asp:BoundField  DataField="customer_id" HeaderText="Customer ID" ReadOnly="true" SortExpression="customer_id" />
                               <asp:BoundField  DataField="First_Name" HeaderText="First Name" ReadOnly="true" SortExpression="fname" />
                               <asp:BoundField  DataField="Last_Name" HeaderText="Last Name" ReadOnly="true" SortExpression="lname" />
                               <asp:BoundField  DataField="Address" HeaderText="Address" ReadOnly="true" SortExpression="Address" />
                               <asp:BoundField  DataField="Address_Type" HeaderText="Address Type" ReadOnly="true" SortExpression="Address_Type" />
                               <asp:BoundField  DataField="phone" HeaderText="Phone" ReadOnly="true" SortExpression="phone" /> 
                               <asp:BoundField  DataField="alt_phone" HeaderText="Alt Phone" ReadOnly="true" SortExpression="alt_phone" />
                               <asp:BoundField  DataField="other_info" HeaderText="Other Info" ReadOnly="true" SortExpression="other_info" />
                           </Columns>
                       </asp:GridView> 
                           </div>                      
                 </asp:tablecell>                 
               
                   </asp:tablerow>
                    <asp:TableRow>
                        <asp:TableCell>
                         <br />   <asp:Label ID="Label3" runat="server" Text="Customer Credit Information"></asp:Label> 
                            <div id="ScrollList2" style="height: 250px; width:1300px; overflow: auto">
                          <br />      <asp:GridView width="1300px" BackColor="PowderBlue"  id="grdCredit" runat="server" AutoGenerateColumns="false" DataKeyNames="credit_id" OnSelectedIndexChanged="grdCredit_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="grdCredit_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                           <Columns>
                               <asp:CommandField ShowSelectButton="false"></asp:CommandField>
                               <asp:BoundField  DataField="credit_id" HeaderText="Credit ID" ReadOnly="true" SortExpression="credit_id" />
                               <asp:BoundField  DataField="customer_id" HeaderText="Customer ID" ReadOnly="true" SortExpression="customer_id" />
                               <asp:BoundField  DataField="card_name" HeaderText="Card Name" ReadOnly="true" SortExpression="card_name" />
                               <asp:BoundField  DataField="card_number" HeaderText="Card Number" ReadOnly="true" SortExpression="card_number" />
                               <asp:BoundField  DataField="other_info" HeaderText="Other Info" ReadOnly="true" SortExpression="other_info" />                               
                           </Columns>
                       </asp:GridView> 
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
               </asp:table>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top">
              <asp:Button ID="CreateCustomer" runat="server" CssClass="btn" Text="Customer Edit" OnClick="CreateCustomer_Click" Font-Size="Small" /> 
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
               
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
    <asp:Label ID="lblResults" runat="server"></asp:Label>    
    </body>
</body>
</html>
</asp:Content>