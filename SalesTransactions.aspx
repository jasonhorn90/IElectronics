<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SalesTransactions.aspx.cs" Inherits="Sales_SalesTransactions" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
    <title></title>    

</head>
<body>
<br />    <asp:Label ID="Label3" runat="server" Text="Welcome to the Sales Transactions Page" Font-Bold="true"></asp:Label>    
    
               <asp:table ID="Table1" runat="server" Height="100%" Width="100%" Font-Bold="true">
                   <asp:TableRow>
                       <asp:TableCell>
                           <asp:table ID="table2" runat="server">
                               <asp:TableRow>
                                   <asp:TableCell>
                                     <br />   <asp:Label ID="Label1" runat="server" Text="Sales Transactions"></asp:Label><br />   
                                      <br />  <div id="scrollList" style="height:250px; width:1300px; overflow:auto">
                               <asp:GridView id="grdSalesHeader" BackColor="PowderBlue"  Width="1300px" runat="server" AutoGenerateColumns="false" DataKeyNames="Transaction_ID" OnSelectedIndexChanged="grdSalesHeader_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="grdSalesHeader_PageIndexChanging"  PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                                <Columns>
                                <asp:CommandField ShowSelectButton="true"></asp:CommandField>
                               <asp:BoundField  DataField="Transaction_ID" HeaderText="Transaction ID" ReadOnly="true" SortExpression="Transaction_ID" />
                               <asp:BoundField  DataField="Sales_Date" HeaderText="Sales Date" ReadOnly="true" SortExpression="Sales_Date" />
                               <asp:BoundField  DataField="Employee_Name" HeaderText="Employee Name" ReadOnly="true" SortExpression="Employee_Name" />
                               <asp:BoundField  DataField="other_info" HeaderText="Other Info" ReadOnly="true" SortExpression="other_info" />                                         
                                </Columns>
                            </asp:GridView>  
                           </div>                            
                       </asp:TableCell>
                   </asp:TableRow>
                   <asp:TableRow>
                       <asp:TableCell>
                           <br />  <asp:Label ID="Label2" runat="server" Text="Sales Transaction Detail"></asp:Label><br /> 
                       </asp:TableCell>
                   </asp:TableRow>
                   <asp:TableRow>
                       <asp:TableCell> 
                           <div id="scrollList2" style="height:250px; width:1300px; overflow:auto">
                          <br />     <asp:GridView id="grdSalesDetail" BackColor="PowderBlue"  Width="1300px" runat="server" AutoGenerateColumns="false" DataKeyNames="STD_ID" OnSelectedIndexChanged="grdSalesDetail_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="grdSalesDetail_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AlternatingRowStyle-Height="30px" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                               <Columns>
                                
                               <asp:BoundField  DataField="STD_ID" HeaderText="Transaction Detail ID" ReadOnly="true" SortExpression="STD_ID" />
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
                       <asp:TableCell VerticalAlign="Top">
                         <asp:Button Width="180px" ID="empComm" runat="server" CssClass="btn" Text="Comm. Report" OnClick="empComm_Click" Font-Size="Small" ForeColor="Red"/> 
                       </asp:TableCell>                    
                   </asp:TableRow>
               </asp:table>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">       
    </asp:ScriptManager>
    

        
    <asp:Label ID="lblResults" runat="server"></asp:Label>    
</body>
</html>
</asp:Content>