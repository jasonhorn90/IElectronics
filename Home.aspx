<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
    </head>
    <body >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
     <br />   <asp:Label ID="Label1" runat="server" Text="Welcome to the Home Page"></asp:Label>
        <asp:Table ID="table2" runat="server" Height="100%" Width="100%">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Table ID="Table1" runat="server" Width="100%">
                         <asp:TableRow>                             
                             <asp:TableCell> 
                                 <div id="piechart" style="width:300px" runat="server" >
                                     <ajaxToolkit:PieChart ID="PieChart1" BackColor="PowderBlue"  runat="server" Font-Bold="true" ChartHeight="400"  ChartTitle="Customer Traffic by Location"></ajaxToolkit:PieChart>
                                 </div>                   
                                    
                                <asp:Label ID="lblResults" runat="server"></asp:Label> 
                             </asp:TableCell>
                             <asp:TableCell Width="75%">
                                  <div id="Div1" style="width:auto" class="box2">
                                      <ajaxToolkit:BarChart ID="BarChart1" BackColor="PowderBlue" runat="server" Font-Bold="true" ChartHeight="400"  ChartTitle="High Performers ($ by rolling 30 Day Performance)"></ajaxToolkit:BarChart>
                                  </div>                                
                             </asp:TableCell>
                         </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>

                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>      
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>      
            
       
         
       
    </body>
   
    </html>
</asp:Content>

