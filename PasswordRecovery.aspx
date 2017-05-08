<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="PasswordRecovery" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" BackColor="#F7F7DE" BorderColor="#CCCC99"
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
        Font-Size="10pt" OnSendingMail="PasswordRecovery1_SendingMail" 
        onverifyinganswer="PasswordRecovery1_VerifyingAnswer" 
        onverifyinguser="PasswordRecovery1_VerifyingUser">
        <MailDefinition From="root@labwebs.webster.edu/jasonhorn90" Subject="Your Password">
        </MailDefinition>
        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
    </asp:PasswordRecovery>
</asp:Content>

