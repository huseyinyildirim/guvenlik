using System;

public partial class alt : System.Web.UI.UserControl
{

protected void Page_Load(object sender, EventArgs e)
{
}

DateTime startTime = DateTime.Now;

protected override void OnPreRender(EventArgs e)
{
base.OnPreRender(e);
this.zaman.Text = (((DateTime.Now - startTime).TotalMilliseconds)/1000).ToString() + " sn.";
}

}
