using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class ana_depo_urun_sevki : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "database.png", "Depo İşlemleri");

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
Ekle();
}
}

protected void Ekle()
{
string SQL = "INSERT INTO depo (bolge_id,firma_id,urun_id,durum,adet,tip,admin_id) values (@bolge_id,@firma_id,@urun_id,@durum,@adet,@tip,@admin_id)";
MySqlCommand VeriGir = new MySqlCommand(SQL, Class.Fonksiyonlar.MySQL.Baglanti);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir.Parameters.AddWithValue("@bolge_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["bolge_id"]));
VeriGir.Parameters.AddWithValue("@firma_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["firma_id"]));
VeriGir.Parameters.AddWithValue("@urun_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["urun_id"]));
VeriGir.Parameters.AddWithValue("@durum", Class.Fonksiyonlar.SQLTemizle(Request.Form["durum"]));
VeriGir.Parameters.AddWithValue("@adet", "-"+Class.Fonksiyonlar.SQLTemizle(Request.Form["adet"]));
VeriGir.Parameters.AddWithValue("@tip", "1");
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
string kod;

if (dgr != string.Empty)
{
string[] lines = Regex.Split(dgr, " - ");

urun_id_lbl.Text = lines[0];

if (lines.Length > 3)
{
kod = lines[2];

switch (kod)
{
case "ESKİ":
durum.Attributes.Add("value", "0");
break;

case "YENİ":
durum.Attributes.Add("value", "1");
break;
}

urun_kodu_lbl.Text = lines[1];
urun_durumu_lbl.Text = lines[2];
urun_adet_lbl.Text = Class.Fonksiyonlar.SayiFormatla(lines[3]);
kullanilabilir_adet.Text = lines[3];
}
else
{
kod = lines[1];

switch (kod)
{
case "ESKİ":
durum.Attributes.Add("value", "0");
break;

case "YENİ":
durum.Attributes.Add("value", "1");
break;
}

urun_kodu_lbl.Text = "------";
urun_durumu_lbl.Text = lines[1];
urun_adet_lbl.Text = Class.Fonksiyonlar.SayiFormatla(lines[2]);
kullanilabilir_adet.Text = lines[2];
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