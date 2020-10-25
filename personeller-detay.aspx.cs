using System;
using System.Data;

public partial class personeller_detay : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "user.png", "Personel İşlemleri");

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
string SQL = "SELECT * FROM personel USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' " + Class.Fonksiyonlar.SiteFonksiyonlari.AdminSQL() + "";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "personel");

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
firma_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(ds.Tables[0].Rows[0]["firma_id"].ToString());

tc_kimlik.Text = ds.Tables[0].Rows[0]["tc_kimlik"].ToString();
adi_soyadi.Text = ds.Tables[0].Rows[0]["adi_soyadi"].ToString();

switch (ds.Tables[0].Rows[0]["cinsiyet"].ToString())
{
case "0":
cinsiyet.Text = "Erkek";
break;

case "1":
cinsiyet.Text = "Bayan";
break;
}

telefon.Text = Class.Fonksiyonlar.TelFormatla(ds.Tables[0].Rows[0]["tel"].ToString());
mail.Text = ds.Tables[0].Rows[0]["mail"].ToString();

if (ds.Tables[0].Rows[0]["dogum_tarihi"].ToString() != "")
{
dogum_tarihi.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dogum_tarihi"].ToString()).ToShortDateString();
}
else
{
dogum_tarihi.Text = "------";
}

dogum_yeri.Text = ds.Tables[0].Rows[0]["dogum_yeri"].ToString();
ana_adi.Text = ds.Tables[0].Rows[0]["ana_adi"].ToString();
baba_adi.Text = ds.Tables[0].Rows[0]["baba_adi"].ToString();
ssk_no.Text = ds.Tables[0].Rows[0]["ssk_no"].ToString();

switch (ds.Tables[0].Rows[0]["egitim_durumu"].ToString())
{
case "0":
egitim_durumu.Text = "Eğitimsiz";
break;

case "1":
egitim_durumu.Text = "Okul Öncesi";
break;

case "2":
egitim_durumu.Text = "İlköğretim";
break;

case "3":
egitim_durumu.Text = "Lise";
break;

case "4":
egitim_durumu.Text = "Yüksek Okul";
break;

case "5":
egitim_durumu.Text = "Üniversite";
break;

case "6":
egitim_durumu.Text = "Yüksek Lisans";
break;

case "7":
egitim_durumu.Text = "Doktora";
break;

case "9":
egitim_durumu.Text = "------";
break;
}

ehliyet.Text = ds.Tables[0].Rows[0]["ehliyet"].ToString();

switch (ds.Tables[0].Rows[0]["sertifika_durumu"].ToString())
{
case "0":
guvenlik_egitimi.Text = "Silahsız";
break;

case "1":
guvenlik_egitimi.Text = "Silahlı";
break;
}

il_kodu.Text = Class.Fonksiyonlar.SiteFonksiyonlari.ilAdiVer(ds.Tables[0].Rows[0]["il_kodu"].ToString());
ilce_kodu.Text = Class.Fonksiyonlar.SiteFonksiyonlari.ilceAdiVer(ds.Tables[0].Rows[0]["ilce_kodu"].ToString());

adres.Text = ds.Tables[0].Rows[0]["adres"].ToString();
notlar.Text = ds.Tables[0].Rows[0]["notlar"].ToString();

admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();
}

}

}