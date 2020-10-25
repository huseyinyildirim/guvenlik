using System;
using System.Data;

public partial class personeller_detay : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "asterisk.png", "Firma İşlemleri");

if (!IsCallback && !IsPostBack)
{
if (Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))))
{
VeriGetir();
}
}
}

// VIEWSTATE Sıkıştırmaca //
// Boş VIEWSTATE //
protected override void SavePageStateToPersistenceMedium(object viewState)
{
}

// Boş VIEWSTATE //
protected override object LoadPageStateFromPersistenceMedium()
{
return null;
}

protected void VeriGetir()
{
string SQL = "SELECT * FROM firma USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"])+ "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "firma");

if (ds.Tables[0].Rows.Count > 0)
{

switch (ds.Tables[0].Rows[0]["onay"].ToString())
{
case "0":
durum.Text = "<b>Pasif</b>";
break;

case "1":
durum.Text = "Aktif";
break;
}

bolge_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(ds.Tables[0].Rows[0]["bolge_id"].ToString());

adi.Text = ds.Tables[0].Rows[0]["adi"].ToString();
telefon.Text = Class.Fonksiyonlar.TelFormatla(ds.Tables[0].Rows[0]["tel"].ToString());
faks.Text = Class.Fonksiyonlar.TelFormatla(ds.Tables[0].Rows[0]["faks"].ToString());
mail.Text = ds.Tables[0].Rows[0]["mail"].ToString();
vergi_dairesi.Text = ds.Tables[0].Rows[0]["vergi_dairesi"].ToString();
vergi_no.Text = ds.Tables[0].Rows[0]["vergi_no"].ToString();
ticaret_sicil_no.Text = ds.Tables[0].Rows[0]["ticaret_sicil_no"].ToString();
il_kodu.Text = Class.Fonksiyonlar.SiteFonksiyonlari.ilAdiVer(ds.Tables[0].Rows[0]["il_kodu"].ToString());
ilce_kodu.Text = Class.Fonksiyonlar.SiteFonksiyonlari.ilceAdiVer(ds.Tables[0].Rows[0]["ilce_kodu"].ToString());
adres.Text = ds.Tables[0].Rows[0]["adres"].ToString();

admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();
}

}

}