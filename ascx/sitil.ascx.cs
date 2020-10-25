using System;
using System.Web;

public partial class sitil : System.Web.UI.UserControl
{

protected void Page_Load(object sender, EventArgs e)
{
Session.Timeout = 1440;

if (Request.Cookies["ust"] == null)
{
HttpCookie cookie = new HttpCookie("ust");
cookie.Value = "acik";
cookie.Expires = DateTime.Now.AddYears(1);
Response.Cookies.Add(cookie);
}

if (Request.Cookies["menu"] == null)
{
HttpCookie cookie = new HttpCookie("menu");
cookie.Value = "acik";
cookie.Expires = DateTime.Now.AddYears(1);
Response.Cookies.Add(cookie);
}

/*
base.OnInit(e);
Response.CacheControl = "no-cache";
Response.AddHeader("Pragma", "no-cache");
Response.Expires = -1;

Response.Cache.SetCacheability(HttpCacheability.NoCache);
Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
Response.Cache.SetNoStore();
*/
}

}