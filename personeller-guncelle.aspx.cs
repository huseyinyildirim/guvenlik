using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class personeller_guncelle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "user.png", "Personel İşlemleri");

if (!IsCallback && !IsPostBack)
{
if (Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))))
{
BolgeYukle();
ilYukle();
VeriGetir();
}
else
{
tbl_gonder.Visible = false;
}
}

}

protected void BolgeYukle()
{
//0 Genel Yönetici (Root)
//1 Bölge Yöneticisi
//2 Firma Yöneticisi
//3 Genel Depo Yöneticisi
//4 Firma Depo Yöneticisi

string SQL;
switch (Class.Degiskenler.AdminTip)
{
case "1":
SQL = "SELECT id,adi FROM bolge USE INDEX (onay,id) WHERE onay='1' and id!='0' and id=" + Class.Degiskenler.AdminBolgeID + " ORDER BY adi ASC";
break;

case "2":
SQL = "SELECT id,adi FROM bolge USE INDEX (onay,id) WHERE onay='1' and id!='0' and id=" + Class.Degiskenler.AdminBolgeID + " ORDER BY adi ASC";
break;

default:
SQL = "SELECT id,adi FROM bolge USE INDEX (onay,id) WHERE onay='1' and id!='0' ORDER BY adi ASC";
break;
}

DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "bolge");

if (ds.Tables[0].Rows.Count > 0)
{
for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
{
bolge_id.Items.Add(new ListItem(ds.Tables[0].Rows[i]["adi"].ToString(), ds.Tables[0].Rows[i]["id"].ToString()));
}
}

}

protected void bolge_SelectedIndexChanged(object sender, EventArgs e)
{
if (bolge_id.SelectedItem.Value.ToString() != "0")
{
firma_id.Enabled = true;

string SQL = "SELECT adi,id FROM firma USE INDEX (bolge_id) WHERE bolge_id='" + @bolge_id.SelectedItem.Value + "' ORDER BY adi ASC";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "ilce");

if (ds.Tables[0].Rows.Count > 0)
{
firma_id.Items.Clear();
firma_id.Items.Add(new ListItem("------ SEÇİNİZ ------", "0"));
for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
{
firma_id.Items.Add(new ListItem(ds.Tables[0].Rows[i]["adi"].ToString(), ds.Tables[0].Rows[i]["id"].ToString()));
}
}
}
else
{
firma_id.Enabled = false;
}
}

protected void ilYukle()
{
string SQL = "SELECT il_kodu,il FROM il USE INDEX (il_goster) WHERE il_goster='0' ORDER BY il ASC";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "il");

if (ds.Tables[0].Rows.Count > 0)
{
for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
{
il_kodu.Items.Add(new ListItem(ds.Tables[0].Rows[i]["il"].ToString(), ds.Tables[0].Rows[i]["il_kodu"].ToString()));
}
}
}

protected void il_kodu_SelectedIndexChanged(object sender, EventArgs e)
{

if (il_kodu.SelectedItem.Value.ToString() != "0")
{
ilce_kodu.Enabled = true;

string SQL = "SELECT ilce_kodu,ilce FROM ilce USE INDEX (il_kodu,merkez) WHERE il_kodu='" + @il_kodu.SelectedItem.Value + "' ORDER BY merkez DESC,ilce ASC";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "ilce");

if (ds.Tables[0].Rows.Count > 0)
{
ilce_kodu.Items.Clear();
ilce_kodu.Items.Add(new ListItem("------ SEÇİNİZ ------", "0"));
for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
{
ilce_kodu.Items.Add(new ListItem(ds.Tables[0].Rows[i]["ilce"].ToString(), ds.Tables[0].Rows[i]["ilce_kodu"].ToString()));
}
}
}
else
{
ilce_kodu.Enabled = false;
}
}

protected void VeriGetir()
{
string SQL = "SELECT * FROM personel USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' " + Class.Fonksiyonlar.SiteFonksiyonlari.AdminSQL() + "";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "personel");

if (ds.Tables[0].Rows.Count > 0)
{
bolge_id_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(ds.Tables[0].Rows[0]["bolge_id"].ToString());
firma_id_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(ds.Tables[0].Rows[0]["firma_id"].ToString());
//il_kodu_label.Text = Class.Fonksiyonlar.SiteFonksiyonlari.ilAdiVer(ds.Tables[0].Rows[0]["il_kodu"].ToString());
admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();

tc_kimlik.Attributes.Add("value", ds.Tables[0].Rows[0]["tc_kimlik"].ToString());
adi_soyadi.Attributes.Add("value", ds.Tables[0].Rows[0]["adi_soyadi"].ToString());

switch (ds.Tables[0].Rows[0]["cinsiyet"].ToString())
{
case "0":
cinsiyet_label.Text = "Erkek";
break;

case "1":
cinsiyet_label.Text = "Bayan";
break;
}

tel.Attributes.Add("value", ds.Tables[0].Rows[0]["tel"].ToString());
mail.Attributes.Add("value", ds.Tables[0].Rows[0]["mail"].ToString());

if (ds.Tables[0].Rows[0]["dogum_tarihi"].ToString() != "")
{
dogum_tarihi_label.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dogum_tarihi"].ToString()).ToShortDateString();
}
else
{
dogum_tarihi_label.Text = "------";
}

dogum_yeri.Attributes.Add("value", ds.Tables[0].Rows[0]["dogum_yeri"].ToString());
ana_adi.Attributes.Add("value", ds.Tables[0].Rows[0]["ana_adi"].ToString());
baba_adi.Attributes.Add("value", ds.Tables[0].Rows[0]["baba_adi"].ToString());
ssk_no.Attributes.Add("value", ds.Tables[0].Rows[0]["ssk_no"].ToString());

switch (ds.Tables[0].Rows[0]["egitim_durumu"].ToString())
{
case "0":
egitim_durumu_label.Text = "Eğitimsiz";
break;

case "1":
egitim_durumu_label.Text = "Okul Öncesi";
break;

case "2":
egitim_durumu_label.Text = "İlköğretim";
break;

case "3":
egitim_durumu_label.Text = "Lise";
break;

case "4":
egitim_durumu_label.Text = "Yüksek Okul";
break;

case "5":
egitim_durumu_label.Text = "Üniversite";
break;

case "6":
egitim_durumu_label.Text = "Yüksek Lisans";
break;

case "7":
egitim_durumu_label.Text = "Doktora";
break;

case "9":
egitim_durumu_label.Text = "------";
break;
}

ehliyet.Attributes.Add("value", ds.Tables[0].Rows[0]["ehliyet"].ToString());

switch (ds.Tables[0].Rows[0]["sertifika_durumu"].ToString())
{
case "0":
guvenlik_egitimi.Text = "Silahsız";
break;

case "1":
guvenlik_egitimi.Text = "Silahlı";
break;
}

il_kodu_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.ilAdiVer(ds.Tables[0].Rows[0]["il_kodu"].ToString());
ilce_kodu_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.ilceAdiVer(ds.Tables[0].Rows[0]["ilce_kodu"].ToString());

adres.Text = ds.Tables[0].Rows[0]["adres"].ToString();
notlar.Text = ds.Tables[0].Rows[0]["notlar"].ToString();
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
string bolge_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox1"]) == "on")
{
bolge_guncelle = ",bolge_id ='" + Class.Fonksiyonlar.SQLTemizle(bolge_id.SelectedValue.ToString()) + "',firma_id ='" + Class.Fonksiyonlar.SQLTemizle(firma_id.SelectedValue.ToString()) + "'";
}

string cinsiyet_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox2"]) == "on")
{
cinsiyet_guncelle = ",cinsiyet ='" + Class.Fonksiyonlar.SQLTemizle(cinsiyet.Value) + "'";
}

string dogum_tarihi_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox3"]) == "on")
{
dogum_tarihi_guncelle = ",dogum_tarihi ='" + Class.Fonksiyonlar.SQLTemizle(dogum_tarihi_yil.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(dogum_tarihi_ay.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(dogum_tarihi_gun.Value) + "'";
}

string egitim_durumu_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox4"]) == "on")
{
egitim_durumu_guncelle = ",egitim_durumu ='" + Class.Fonksiyonlar.SQLTemizle(egitim_durumu.Value) + "'";
}

string guvenlik_egitimi_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox5"]) == "on")
{
guvenlik_egitimi_guncelle = ",sertifika_durumu ='" + Class.Fonksiyonlar.SQLTemizle(sertifika_durumu.Value) + "'";
}

string il_kodu_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox6"]) == "on")
{
il_kodu_guncelle = ",il_kodu ='" + Class.Fonksiyonlar.SQLTemizle(il_kodu.SelectedValue.ToString()) + "',ilce_kodu ='" + Class.Fonksiyonlar.SQLTemizle(ilce_kodu.SelectedValue.ToString()) + "'";
}

string SQL = "UPDATE personel SET tc_kimlik = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["tc_kimlik"]) + "',adi_soyadi = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adi_soyadi"]) + "',tel = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["tel"]) + "',mail = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["mail"]) + "',dogum_yeri = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["dogum_yeri"]) + "',ana_adi = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["ana_adi"]) + "',baba_adi = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["baba_adi"]) + "',ssk_no = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["ssk_no"]) + "',ehliyet = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["ehliyet"]) + "',adres = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adres"]) + "',notlar = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["notlar"]) + "' " + bolge_guncelle + cinsiyet_guncelle + dogum_tarihi_guncelle + egitim_durumu_guncelle + guvenlik_egitimi_guncelle + il_kodu_guncelle + " WHERE id = '" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir,"Bilgileriniz başarıyla güncellenmiştir.",1);
}

}