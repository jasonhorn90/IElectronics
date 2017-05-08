<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Employees.aspx.cs" Inherits="Employees" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head >
        <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
        <title></title>
    </head>
    <body>
     <br />  <asp:Label ID="Label1" runat="server" Text="Welcome to the Employee Page" Font-Bold="true"></asp:Label>   
     
    <asp:Table runat="server" ID="table2" Height="100%" Width="100%" Font-Bold="true">
        <asp:TableRow>
            <asp:TableCell>
                <asp:table ID="Table1" runat="server">
                   <asp:tablerow>
                      <asp:tablecell>
                        <br />  <asp:Label ID="Label2" runat="server" Text="Employees"></asp:Label> 
                          <div id="ScrollList" style="height: 250px; width:1300px; overflow: auto">
                              <br />  <asp:GridView Width="1300px" id="grdEmployee" BackColor="PowderBlue"  runat="server" AutoGenerateColumns="false" DataKeyNames="employee_id" OnSelectedIndexChanged="dgEmployee_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="dgEmployee_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                              <Columns>                            
                               
                               <asp:CommandField ShowSelectButton="true"></asp:CommandField>
                               <asp:BoundField  DataField="employee_id" HeaderText="employee_id" ReadOnly="true" SortExpression="employee_id" />
                               <asp:BoundField  DataField="employee_name" HeaderText="Employee Name" ReadOnly="true" SortExpression="employee_name" />
                               <asp:BoundField  DataField="employee_role" HeaderText="Employee Role" ReadOnly="true" SortExpression="employee_role" />
                               <asp:BoundField  DataField="pay_rate" HeaderText="Pay Rate" ReadOnly="true" SortExpression="pay_rate" />                
                              
                                                                                           
                           </Columns>
                       </asp:GridView> 
                          </div>                       
                 </asp:tablecell>                               
                   </asp:tablerow>
                    <asp:TableRow>
                        <asp:TableCell>
                          <br />  <asp:Label ID="Label3" runat="server" Text="Employee Timecards"></asp:Label> 
                           <div id="ScrollList2" style="height: 250px; width:1300px; overflow: auto">
                            <br />    <asp:GridView  Width="1300px" BackColor="PowderBlue"   id="grdEmpTimecard" runat="server" AutoGenerateColumns="false" DataKeyNames="timecard_id" OnSelectedIndexChanged="grdEmpTimecard_SelectedIndexChanged"
                             allowpaging="false" OnPageIndexChanging="grdEmpTimecard_PageIndexChanging" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" RowStyle-Font-Bold="true" RowStyle-Font-Size="Medium">
                           <Columns>                            
                               
                               
                               <asp:BoundField  DataField="timecard_id" HeaderText="Timecard ID" ReadOnly="true" SortExpression="timecard_id" />
                               <asp:BoundField  DataField="employee_id" HeaderText="Employee ID" ReadOnly="true" SortExpression="employee_id" />
                               <asp:BoundField  DataField="location_id" HeaderText="Store Location" ReadOnly="true" SortExpression="location_id" />
                               <asp:BoundField  DataField="time_in" HeaderText="Time in" ReadOnly="true" SortExpression="time_in" />
                               <asp:BoundField  DataField="time_out" HeaderText="Time out" ReadOnly="true" SortExpression="time_out" />   
                               <asp:BoundField  DataField="total_hrs" HeaderText="Total Hours" ReadOnly="true" SortExpression="total_hrs" />   
                               <asp:BoundField  DataField="total_pay" HeaderText="Total Pay" ReadOnly="true" SortExpression="total_pay" />                 
                              
                                                                                           
                           </Columns>
                       </asp:GridView> 
                           </div>                        
                       </asp:TableCell>   
                    </asp:TableRow>                        
               </asp:table>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top">               
                <asp:Button Width="150px" ID="CreateEmp" runat="server" CssClass="btn" Text="EDIT EMP" OnClick="CreateEmp_Click" Font-Size="Small" /> 
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
               
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
    <asp:Label ID="lblResults" runat="server"></asp:Label>    
    </body>
    </html>
</asp:Content>