using System;
using System.Data;

public partial class bolgeler_detay : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "web.png", "Bölge İşlemleri");

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
string SQL1 = "SELECT * FROM bolge USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
DataSet ds1 = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL1, "bolge");

if (ds1.Tables[0].Rows.Count > 0)
{

switch (ds1.Tables[0].Rows[0]["onay"].ToString())
{
case "0":
durum.Text = "<b>Pasif</b>";
break;

case "1":
durum.Text = "Aktif";
break;
}

adi.Text = ds1.Tables[0].Rows[0]["adi"].ToString();

string[] dizi = ds1.Tables[0].Rows[0]["il_kodlari"].ToString().Split(',');

for (int j = 0; j < dizi.Length; j++)
{
il_kodlari.Text = il_kodlari.Text + Class.Fonksiyonlar.SiteFonksiyonlari.ilAdiVer(dizi[j].ToString()) + " , ";
}

il_kodlari.Text = il_kodlari.Text.Substring(0, il_kodlari.Text.Length - 3);

string SQL2 = "SELECT adi FROM firma USE INDEX (bolge_id) WHERE bolge_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
DataSet ds2 = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL2, "bolge");

if (ds2.Tables[0].Rows.Count > 0)
{
for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
{
bagli_firmalar.Text = bagli_firmalar.Text + ds2.Tables[0].Rows[i]["adi"].ToString() + " , ";
}
bagli_firmalar.Text = bagli_firmalar.Text.Substring(0, bagli_firmalar.Text.Length - 3);
}

admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds1.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds1.Tables[0].Rows[0]["tarih"].ToString();
}

}

}