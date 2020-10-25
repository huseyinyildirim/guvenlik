using System;
using System.Data;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

public partial class ana_depo_urun_ekle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "database.png", "Depo İşlemleri");

if (!IsCallback && !IsPostBack)
{
admin_id.Text = Class.Degiskenler.AdminAdSoyad;
}
}

protected void submit_Click(object sender, EventArgs e)
{
if (Class.Degiskenler.OturumVarmi == 1)
{
Ekle();
}
}

protected void Ekle()
{
string SQL = "INSERT INTO depo (urun_id,durum,adet,admin_id) values (@urun_id,@durum,@adet,@admin_id)";
MySqlCommand VeriGir = new MySqlCommand(SQL, Class.Fonksiyonlar.MySQL.Baglanti);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir.Parameters.AddWithValue("@urun_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["urun_id"]));
VeriGir.Parameters.AddWithValue("@durum", Class.Fonksiyonlar.SQLTemizle(Request.Form["durum"]));
VeriGir.Parameters.AddWithValue("@adet", Class.Fonksiyonlar.SQLTemizle(Request.Form["adet"]));
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

protected void sec_Click(object sender, EventArgs e)
{
string dgr = Class.Fonksiyonlar.SQLTemizle(Request.Form["urun"]);

if (dgr != string.Empty)
{
string[] lines = Regex.Split(dgr, " - ");

urun_id_lbl.Text = lines[0];

if (lines.Length > 1)
{
urun_kodu_lbl.Text = lines[1];
}
else
{
urun_kodu_lbl.Text = "------";
}

//string SQL = "SELECT id FROM urun USE INDEX (id) WHERE MATCH (urun) AGAINST ('" + lines[0] + "')";
string SQL = "SELECT id FROM urun USE INDEX (id) WHERE urun='" + lines[0] + "'";
urun_id.Text = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL);
goster.Visible = true;
}
else
{
urun_id.Text = "0";
goster.Visible = false;
}

}

}