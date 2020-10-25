using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class projeler_guncelle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "finance.png", "Proje İşlemleri");

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
string SQL = "SELECT * FROM proje USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"])+ "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "personel");

if (ds.Tables[0].Rows.Count > 0)
{
bolge_id_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(ds.Tables[0].Rows[0]["bolge_id"].ToString());
firma_id_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(ds.Tables[0].Rows[0]["firma_id"].ToString());

personel_id_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.PersonelAdSoyadVer(ds.Tables[0].Rows[0]["yonetici_id"].ToString());

baslangic_lbl.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["baslangic"]).ToShortDateString();

if (ds.Tables[0].Rows[0]["bitis"].ToString() != "")
{
bitis_lbl.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["bitis"]).ToShortDateString();
}
else
{
bitis_lbl.Text = "------";
}

admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();

adi.Attributes.Add("value", ds.Tables[0].Rows[0]["adi"].ToString());
sorumlu.Attributes.Add("value", ds.Tables[0].Rows[0]["sorumlu"].ToString());
tel.Attributes.Add("value", ds.Tables[0].Rows[0]["tel"].ToString());

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

string yonetici_id_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox2"]) == "on")
{
yonetici_id_guncelle = ",yonetici_id ='" + Class.Fonksiyonlar.SQLTemizle(Request.Form["personel_id"]) + "'";
}

string baslangic_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox3"]) == "on")
{
baslangic_guncelle = ",baslangic ='" + Class.Fonksiyonlar.SQLTemizle(yil1.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(ay1.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(gun1.Value) + "'";
}

string bitis_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox4"]) == "on")
{
bitis_guncelle = ",bitis ='" + Class.Fonksiyonlar.SQLTemizle(yil2.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(ay2.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(gun2.Value) + "'";
}

string SQL = "UPDATE proje SET adi = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adi"]) + "', sorumlu = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["sorumlu"]) + "',tel = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["tel"]) + "',adres = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adres"]) + "',notlar = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["notlar"]) + "' " + bolge_guncelle + yonetici_id_guncelle + baslangic_guncelle + bitis_guncelle + " WHERE id = '" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir,"Bilgileriniz başarıyla güncellenmiştir.",1);
}

protected void sec_Click(object sender, EventArgs e)
{
string dgr = Class.Fonksiyonlar.SQLTemizle(Request.Form["personel"]);

if (dgr != string.Empty)
{
string[] lines = System.Text.RegularExpressions.Regex.Split(dgr, " - ");
tc_kimlik_lbl.Text = lines[0];
adi_soyadi_lbl.Text = lines[1];

//string SQL = "SELECT id FROM personel USE INDEX (tc_kimlik) WHERE MATCH (tc_kimlik) AGAINST ('" + lines[0] + "')";
string SQL = "SELECT id FROM personel USE INDEX (tc_kimlik) WHERE tc_kimlik='" + lines[0] + "'";
personel_id.Text = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL);
goster.Visible = true;
}
else
{
personel_id.Text = "0";
goster.Visible = false;
}
}

}