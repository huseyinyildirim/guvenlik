<%@ Page Language="C#"%>
<script runat="server" type="text/C#">
protected void Page_Load(object sender, EventArgs e)
{

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["deger"]) != null)
{
Response.Write(Class.Fonksiyonlar.SiteFonksiyonlari.AdminKullaniciAdiKontrol(Class.Fonksiyonlar.SQLTemizle(Request.QueryString["deger"])));
}

}
</script>