using System;
using System.Data;
using MySql.Data.MySqlClient;

public partial class bolgeler_guncelle: System.Web.UI.Page
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
IlGetir();
}
else
{
tbl_gonder.Visible = false;
}
}
}

protected void VeriGetir()
{
string SQL = "SELECT * FROM bolge USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' " + Class.Fonksiyonlar.SiteFonksiyonlari.AdminSQL().Replace("bolge_id", "id") + "";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "bolge");

if (ds.Tables[0].Rows.Count > 0)
{
admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();

adi.Attributes.Add("value", ds.Tables[0].Rows[0]["adi"].ToString());

////////////////////////////////////////////////////////////////////////////////////////////////////////////
string[] dizi = ds.Tables[0].Rows[0]["il_kodlari"].ToString().Split(',');

il_kodlari_lbl.Text = string.Empty;
for (int j = 0; j < dizi.Length; j++)
{
il_kodlari_lbl.Text = il_kodlari_lbl.Text + Class.Fonksiyonlar.SiteFonksiyonlari.ilAdiVer(dizi[j].ToString()) + " , ";
}

il_kodlari_lbl.Text = il_kodlari_lbl.Text.Substring(0, il_kodlari_lbl.Text.Length - 3);
////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

protected void IlGetir()
{
string SQL = "SELECT il_kodu,il FROM il USE INDEX (il_goster) WHERE il_goster='0' ORDER BY il ASC";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "il");

if (ds.Tables[0].Rows.Count > 0)
{
il_listele.DataSource = ds;
il_listele.DataBind();
}
}

protected void Guncelle()
{
string il_kodlari_guncelle = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["checkbox"]) == "on")
{
il_kodlari_guncelle = ",il_kodlari ='" + Class.Fonksiyonlar.SQLTemizle(Class.Fonksiyonlar.SQLTemizle(Request.Form["il_kodlari"])) + "'";
}

string SQL = "UPDATE bolge SET adi = '" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adi"]) + "' " + il_kodlari_guncelle + " WHERE id = '" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir,"Bilgileriniz başarıyla güncellenmiştir.",1);
}

}