using System;

public partial class ust : System.Web.UI.UserControl
{
protected void Page_Load(object sender, EventArgs e)
{

if (Request.QueryString["terket"] == "ok")
{
Session.Clear();
Session.RemoveAll();
Session.Abandon();
Response.Redirect("default.aspx");
}

}
}
