﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class PasswordRecovery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
    {
        e.Cancel = true;
        PasswordRecovery1.SuccessText = e.Message.Body;
    }
    protected void PasswordRecovery1_SendMailError(object sender, SendMailErrorEventArgs e)
    {

    }
    protected void PasswordRecovery1_VerifyingAnswer(object sender, LoginCancelEventArgs e)
    {

    }
    protected void PasswordRecovery1_VerifyingUser(object sender, LoginCancelEventArgs e)
    {

    }
}