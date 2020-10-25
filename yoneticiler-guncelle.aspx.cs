using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class yoneticiler_guncelle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "group.png", "Yönetici İşlemleri");

if (!IsCallback && !IsPostBack)
{
if (Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))))
{
BolgeYukle();
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

protected void VeriGetir()
{
string SQL = "SELECT * FROM admin USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' " + Class.Fonksiyonlar.SiteFonksiyonlari.AdminSQL() + "";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "admin");

if (ds.Tables[0].Rows.Count > 0)
{
admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();

bolge_id_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(ds.Tables[0].Rows[0]["bolge_id"].ToString());
firma_id_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(ds.Tables[0].Rows[0]["firma_id"].ToString());

adi_soyadi.Attributes.Add("value", ds.Tables[0].Rows[0]["adi_soyadi"].ToString());
kullanici_adi.Attributes.Add("value", ds.Tables[0].Rows[0]["kullanici_adi"].ToString());

switch (ds.Tables[0].Rows[0]["tip"].ToString())
{
case "0":
mevcut_tip.Text = "Genel Yönetici (Root)";
break;

case "1":
mevcut_tip.Text = "Bölge Yöneticisi";
break;

case "2":
mevcut_tip.Text = "Firma Yöneticisi";
break;

case "3":
mevcut_tip.Text = "Genel Depo Yöneticisi";
break;

case "4":
mevcut_tip.Text = "Firma Depo Yöneticisi";
break;
}

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
KullaniciAdiKontrol();
}
}

protected void KullaniciAdiKontrol()
{
if (Convert.ToDecimal(Class.Fonksiyonlar.SiteFonksiyonlari.AdminKullaniciAdiKontrol(Class.Fonksiyonlar.SQLTemizle(Request.Form["kullanici_adi"]))) < 2)
{
Guncelle();
}
else
{
ClientScript.RegisterClientScriptBlock(this.GetType(), Class.Degiskenler.SiteAdi, Class.Fonksiyonlar.JavaScript.FaceBox.GeriGonder("Seçtiğiniz kullanici adı kullanımda. Lütfen başka bir kullanıcı adı belirtiniz."), true);
}
}

protected void Guncelle()
{
string bolge_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox1"]) == "on")
{
bolge_guncelle = ",bolge_id ='" + Class.Fonksiyonlar.SQLTemizle(bolge_id.SelectedValue.ToString()) + "',firma_id ='" + Class.Fonksiyonlar.SQLTemizle(firma_id.SelectedValue.ToString()) + "'";
}

string sifre_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox2"]) == "on")
{
sifre_guncelle = ",sifre='" + Class.Fonksiyonlar.MD5Sifrele(Class.Fonksiyonlar.SQLTemizle(Request.Form["sifre"])) + "'";
}

string tip_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox3"]) == "on")
{
tip_guncelle = ",tip ='" + Class.Fonksiyonlar.SQLTemizle(tip.SelectedValue.ToString()) + "'";
}

string SQL = "UPDATE admin SET adi_soyadi ='" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adi_soyadi"]) + "',kullanici_adi ='" + Class.Fonksiyonlar.SQLTemizle(Request.Form["kullanici_adi"]) + "' " + bolge_guncelle + sifre_guncelle + tip_guncelle + " WHERE id = '" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir,"Bilgileriniz başarıyla güncellenmiştir.",1);
}

}