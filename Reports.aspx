<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports_Reports" %>



<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">

 <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
    <title></title> 
    
    <script type="text/javascript">
        function ChangeCalendarView(sender, args) {
            sender._switchMode("years", true);
        }
</script>   
</head>
<body>    
 <br />   <asp:Table ID="table1" runat="server" Height="100%" Width="100%" >
        <asp:TableRow>                         
            <asp:TableCell HorizontalAlign="Left">  
               <asp:Label ID="calendar1lbl" runat="server" Text="Start Date: " />
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1" Format="dd-MMM-yy" />  
                 <asp:Label ID="calendar2lbl" runat="server" Text=" End Date: " />
                 <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox2" Format="dd-MMM-yy" />  <br /> 
                  <asp:Button Width="75px" Height="30px" ID="Go" runat="server" CssClass="btn" Text="Go" OnClick="Go_Click"/>                  
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Center">                  
                                   
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right">
                                             
            </asp:TableCell>

        </asp:TableRow>
        <asp:TableRow HorizontalAlign="Center" Height="100%" Width="100%">
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server" Text="Item Sales Frequency Dynamic Report"></asp:Label> 
                           <div id="ScrollList" style=" width:1300px; overflow: auto">
                               <br /><asp:GridView width="1300px" ID="GridView1" BackColor="PowderBlue"  runat="server" AutoGenerateColumns="False" DataKeyNames="item_number"
                                 AllowPaging="false"  PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                                <Columns>           
                                     <asp:BoundField DataField="item_number" ReadOnly="true"  HeaderText="Item Number" SortExpression="item_number" />
                                    <asp:BoundField DataField="product_desc" ReadOnly="true"  HeaderText="Product Description"  SortExpression="product_desc" />
                                    <asp:BoundField DataField="TotalSold" ReadOnly="true"  HeaderText="Total Sold"  SortExpression="TotalSold" /> 
                                    <asp:BoundField DataField="transaction_date" ReadOnly="true"  HeaderText="Transaction Date"  SortExpression="transaction_date" />                      
                                 </Columns>
                                </asp:GridView>
                           </div>
                
            </asp:TableCell></asp:TableRow></asp:Table><asp:Label ID="lblResults" runat="server"></asp:Label></body><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

</html>
</asp:Content>


