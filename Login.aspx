<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <head xmlns="http://www.w3.org/1999/xhtml">
    <link href="../MediStyle.css" rel="Stylesheet" type="text/css" />
    <title></title>
       
</head>
    
        <asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="TableRow1" runat="server"  >
            <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Left">
                <div id="login" runat="server" style="width:100%; height:100%;" >
                     <asp:Login CssClass="login1"  ID="Login1" runat="server" PasswordRecoveryText="Forget your password?" PasswordRecoveryUrl="../PasswordRecovery.aspx" DestinationPageUrl="~/Home.aspx" LoginButtonStyle-Height="25px" LoginButtonStyle-Width="75px" LabelStyle-HorizontalAlign="Left" LabelStyle-VerticalAlign="Middle" TextBoxStyle-Width="250px" Orientation="Vertical" CheckBoxStyle-HorizontalAlign="Center" Font-Bold="False" LoginButtonStyle-BorderStyle="NotSet" LoginButtonStyle-BorderColor="Silver" log>
                <TitleTextStyle Font-Bold="True" ForeColor="Black" Font-Size="0.9em"/>
            </asp:Login>
                </div>
                           
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
   
    
    
</asp:Content>