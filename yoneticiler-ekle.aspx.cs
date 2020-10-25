using System;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI;

public partial class yoneticiler_ekle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "group.png", "Yönetici İşlemleri");

if (!IsCallback && !IsPostBack)
{
BolgeYukle();
admin_id.Text = Class.Degiskenler.AdminAdSoyad;
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

protected void submit_Click(object sender, EventArgs e)
{
if (Class.Degiskenler.OturumVarmi == 1)
{
KullaniciAdiKontrol();
}
}

protected void KullaniciAdiKontrol()
{
if (Convert.ToDecimal(Class.Fonksiyonlar.SiteFonksiyonlari.AdminKullaniciAdiKontrol(Class.Fonksiyonlar.SQLTemizle(Request.Form["kullanici_adi"]))) < 1)
{
Ekle();
}
else
{
ClientScript.RegisterClientScriptBlock(this.GetType(), Class.Degiskenler.SiteAdi, Class.Fonksiyonlar.JavaScript.FaceBox.GeriGonder("Seçtiğiniz kullanici adı kullanımda. Lütfen başka bir kullanıcı adı belirtiniz."), true);
}
}

protected void Ekle()
{
string SQL = "INSERT INTO admin (bolge_id,firma_id,adi_soyadi,kullanici_adi,sifre,tip,admin_id) values (@bolge_id,@firma_id,@adi_soyadi,@kullanici_adi,@sifre,@tip,@admin_id)";
MySqlCommand VeriGir = new MySqlCommand(SQL, Class.Fonksiyonlar.MySQL.Baglanti);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir.Parameters.AddWithValue("@bolge_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["bolge_id"]));
VeriGir.Parameters.AddWithValue("@firma_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["firma_id"]));
VeriGir.Parameters.AddWithValue("@adi_soyadi", Class.Fonksiyonlar.SQLTemizle(Request.Form["adi_soyadi"]));
VeriGir.Parameters.AddWithValue("@kullanici_adi", Class.Fonksiyonlar.SQLTemizle(Request.Form["kullanici_adi"]));
VeriGir.Parameters.AddWithValue("@sifre", Class.Fonksiyonlar.MD5Sifrele(Class.Fonksiyonlar.SQLTemizle(Request.Form["sifre"])));
VeriGir.Parameters.AddWithValue("@tip", Class.Fonksiyonlar.SQLTemizle(Request.Form["tip"]));
VeriGir.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
VeriGir.ExecuteNonQuery();
}
finally
{
Class.Fonksiyonlar.MySQL.Baglanti.Close();
Class.Fonksiyonlar.MySQL.Baglanti.Dispose();
}

Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir,"Bilgileriniz başarıyla eklenmiştir.",0);
}

}