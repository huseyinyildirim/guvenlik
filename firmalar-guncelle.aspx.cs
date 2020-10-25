using System;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class firmalar_guncelle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "asterisk.png", "Firma İşlemleri");

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
string SQL = "SELECT * FROM firma USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "firma");

if (ds.Tables[0].Rows.Count > 0)
{
bolge_id_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(ds.Tables[0].Rows[0]["bolge_id"].ToString());

adi.Attributes.Add("value", ds.Tables[0].Rows[0]["adi"].ToString());
tel.Attributes.Add("value", ds.Tables[0].Rows[0]["tel"].ToString());
faks.Attributes.Add("value", ds.Tables[0].Rows[0]["faks"].ToString());
mail.Attributes.Add("value", ds.Tables[0].Rows[0]["mail"].ToString());
vergi_dairesi.Attributes.Add("value", ds.Tables[0].Rows[0]["vergi_dairesi"].ToString());
vergi_no.Attributes.Add("value", ds.Tables[0].Rows[0]["vergi_no"].ToString());
ticaret_sicil_no.Attributes.Add("value", ds.Tables[0].Rows[0]["ticaret_sicil_no"].ToString());
il_kodu_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.ilAdiVer(ds.Tables[0].Rows[0]["il_kodu"].ToString());
ilce_kodu_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.ilceAdiVer(ds.Tables[0].Rows[0]["ilce_kodu"].ToString());
adres.Text = ds.Tables[0].Rows[0]["adres"].ToString();

admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();
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
bolge_guncelle = ",bolge_id ='" + Class.Fonksiyonlar.SQLTemizle(bolge_id.SelectedValue.ToString()) + "'";
}

string il_kodu_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox2"]) == "on")
{
il_kodu_guncelle = ",il_kodu ='" + Class.Fonksiyonlar.SQLTemizle(il_kodu.SelectedValue.ToString()) + "',ilce_kodu ='" + Class.Fonksiyonlar.SQLTemizle(ilce_kodu.SelectedValue.ToString()) + "'";
}

string SQL = "UPDATE firma SET adi = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adi"]) + "', tel = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["tel"]) + "', faks = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["faks"]) + "', mail = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["mail"]) + "', vergi_dairesi = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["vergi_dairesi"]) + "', vergi_no = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["vergi_no"]) + "', ticaret_sicil_no = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["ticaret_sicil_no"]) + "', adres = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adres"]) + "' " + bolge_guncelle + il_kodu_guncelle + " WHERE id = '" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir,"Bilgileriniz başarıyla güncellenmiştir.",1);
}

}