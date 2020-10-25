using System;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class projeler_ekle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "finance.png", "Proje İşlemleri");

if (!IsCallback && !IsPostBack)
{
BolgeYukle();
admin_id.Text = Class.Degiskenler.AdminAdSoyad;
}

gun1.SelectedIndex = DateTime.Now.Day-1;
ay1.SelectedIndex = DateTime.Now.Month-1;

switch (DateTime.Now.Year)
{
case 2010:
yil1.SelectedIndex = 80;
break;

case 2011:
yil1.SelectedIndex = 81;
break;

case 2012:
yil1.SelectedIndex = 82;
break;

case 2013:
yil1.SelectedIndex = 83;
break;
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
Ekle();
}
}

protected void Ekle()
{
string baslangic = Class.Fonksiyonlar.SQLTemizle(yil1.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(ay1.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(gun1.Value);
string bitis = Class.Fonksiyonlar.SQLTemizle(yil2.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(ay2.Value) + "-" + Class.Fonksiyonlar.SQLTemizle(gun2.Value);

string SQL1 = "INSERT INTO proje (bolge_id,firma_id,yonetici_id,baslangic,bitis,adi,sorumlu,tel,adres,notlar,admin_id) values (@bolge_id,@firma_id,@yonetici_id,@baslangic,@bitis,@adi,@sorumlu,@tel,@adres,@notlar,@admin_id)";
MySqlCommand VeriGir = new MySqlCommand(SQL1, Class.Fonksiyonlar.MySQL.Baglanti);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir.Parameters.AddWithValue("@bolge_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["bolge_id"]));
VeriGir.Parameters.AddWithValue("@firma_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["firma_id"]));
VeriGir.Parameters.AddWithValue("@yonetici_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["personel_id"]));
VeriGir.Parameters.AddWithValue("@baslangic", baslangic);
VeriGir.Parameters.AddWithValue("@bitis", bitis);
VeriGir.Parameters.AddWithValue("@adi", Class.Fonksiyonlar.SQLTemizle(Request.Form["adi"]));
VeriGir.Parameters.AddWithValue("@sorumlu", Class.Fonksiyonlar.SQLTemizle(Request.Form["sorumlu"]));
VeriGir.Parameters.AddWithValue("@tel", Class.Fonksiyonlar.SQLTemizle(Request.Form["tel"]));
VeriGir.Parameters.AddWithValue("@adres", Class.Fonksiyonlar.SQLTemizle(Request.Form["adres"]));
VeriGir.Parameters.AddWithValue("@notlar", Class.Fonksiyonlar.SQLTemizle(Request.Form["notlar"]));
VeriGir.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
VeriGir.ExecuteNonQuery();
}
finally
{
Class.Fonksiyonlar.MySQL.Baglanti.Close();
Class.Fonksiyonlar.MySQL.Baglanti.Dispose();
}

string BirDakikaOnce = DateTime.Today.ToString("yyyy-MM-dd") + " " + DateTime.Now.AddMinutes(-1).ToShortTimeString() + ":59";
string BirDakikaSonra = DateTime.Today.ToString("yyyy-MM-dd") + " " + DateTime.Now.AddMinutes(1).ToShortTimeString() + ":59";

string SQL2 = "SELECT MAX(id) FROM proje USE INDEX (tarih,admin_id,onay) WHERE tarih BETWEEN '" + BirDakikaOnce + "' and '" + BirDakikaSonra + "' and admin_id='" + Class.Degiskenler.AdminID + "' and onay='1' LIMIT 1";
string proje_id = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL2);

ClientScript.RegisterClientScriptBlock(this.GetType(), Class.Degiskenler.SiteAdi, Class.Fonksiyonlar.JavaScript.Yonlendir("projeler-zimmet-ekle.aspx?id=" + proje_id), true);
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