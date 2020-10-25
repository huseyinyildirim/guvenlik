using System;
using System.Data;

public partial class urunler_guncelle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "shopping-cart.png", "Ürün İşlemleri");

if (!IsCallback && !IsPostBack)
{
if (Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))))
{
VeriGetir();
}
else
{
tbl_gonder.Visible = false;
}
}

}

protected void VeriGetir()
{
string SQL = "SELECT * FROM urun USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"])+ "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "urun");

if (ds.Tables[0].Rows.Count > 0)
{
admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();

urun.Attributes.Add("value", ds.Tables[0].Rows[0]["urun"].ToString());
kod.Attributes.Add("value", ds.Tables[0].Rows[0]["kod"].ToString());
}
else
{
tbl_gonder.Visible = false;
}

}

protected void submit_Click(object sender, EventArgs e)
{
if (Class.Degiskenler.OturumVarmi == 1)
{
Guncelle();
}
}

protected void Guncelle()
{
string SQL = "UPDATE urun SET urun = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["urun"]) + "',kod = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["kod"]) + "' WHERE id = '" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir,"Bilgileriniz başarıyla güncellenmiştir.",1);
}

}