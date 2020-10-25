using System;
using System.Data;

public partial class ana_depo_urun_guncelle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "database.png", "Depo İşlemleri");

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
string SQL1 = "SELECT * FROM depo USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"])+ "'";
DataSet ds1 = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL1, "depo");

if (ds1.Tables[0].Rows.Count > 0)
{

switch (ds1.Tables[0].Rows[0]["onay"].ToString())
{
case "0":
onay.Text = "<b>Pasif</b>";
break;

case "1":
onay.Text = "Aktif";
break;
}

admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds1.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds1.Tables[0].Rows[0]["tarih"].ToString();

string SQL2 = "SELECT * FROM urun USE INDEX (id) WHERE id='" + ds1.Tables[0].Rows[0]["urun_id"].ToString() + "'";
DataSet ds2 = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL2, "urun");

urun.Text = ds2.Tables[0].Rows[0]["urun"].ToString();
kodu.Text = ds2.Tables[0].Rows[0]["kod"].ToString();

switch (ds1.Tables[0].Rows[0]["durum"].ToString())
{
case "0":
durum_lbl.Text = "Eski";
break;

case "1":
durum_lbl.Text = "Yeni";
break;
}

adet.Attributes.Add("value", ds1.Tables[0].Rows[0]["adet"].ToString());
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
string durum_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox1"]) == "on")
{
durum_guncelle = ",durum ='" + Class.Fonksiyonlar.SQLTemizle(durum.SelectedValue.ToString()) + "'";
}

string SQL = "UPDATE depo SET adet = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adet"]) + "' " + durum_guncelle + " WHERE id = '" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir,"Bilgileriniz başarıyla güncellenmiştir.",1);
}

}