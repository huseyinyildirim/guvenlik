using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class ana_depo_urun_imha : System.Web.UI.Page
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
string SQL1 = "INSERT INTO depo (bolge_id,firma_id,urun_id,durum,adet,tip,admin_id) values (@bolge_id,@firma_id,@urun_id,@durum,@adet,@tip,@admin_id)";
MySqlCommand VeriGir1 = new MySqlCommand(SQL1, Class.Fonksiyonlar.MySQL.Baglanti);
MySqlCommand VeriGir2 = new MySqlCommand(SQL1, Class.Fonksiyonlar.MySQL.Baglanti);

string SQL2 = "SELECT bolge_id FROM firma USE INDEX (id) WHERE id='" + firma_id.SelectedItem.Value.ToString() + "'";
string blg_id = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL2);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir1.Parameters.AddWithValue("@bolge_id", blg_id);
VeriGir1.Parameters.AddWithValue("@firma_id", firma_id.SelectedItem.Value.ToString());
VeriGir1.Parameters.AddWithValue("@urun_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["urun_id"]));
VeriGir1.Parameters.AddWithValue("@durum", Class.Fonksiyonlar.SQLTemizle(Request.Form["durum"]));
VeriGir1.Parameters.AddWithValue("@adet", Class.Fonksiyonlar.SQLTemizle(Request.Form["adet"]));
VeriGir1.Parameters.AddWithValue("@tip", "2");
VeriGir1.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
VeriGir1.ExecuteNonQuery();

VeriGir2.Parameters.AddWithValue("@bolge_id", "0");
VeriGir2.Parameters.AddWithValue("@firma_id", "0");
VeriGir2.Parameters.AddWithValue("@urun_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["urun_id"]));
VeriGir2.Parameters.AddWithValue("@durum", Class.Fonksiyonlar.SQLTemizle(Request.Form["durum"]));
VeriGir2.Parameters.AddWithValue("@adet", "-" + Class.Fonksiyonlar.SQLTemizle(Request.Form["adet"]));
VeriGir2.Parameters.AddWithValue("@tip", "2");
VeriGir2.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
VeriGir2.ExecuteNonQuery();
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
string kod, drm = string.Empty;
string[] lines = Regex.Split(dgr, " - ");

urun_id_lbl.Text = lines[0];

if (lines.Length > 2)
{
kod = lines[2];

switch (kod)
{
case "ESKİ":
durum.Attributes.Add("value", "0");
drm = "0";
break;

case "YENİ":
durum.Attributes.Add("value", "1");
drm = "1";
break;
}

urun_kodu_lbl.Text = lines[1];
urun_durumu_lbl.Text = kod;
}
else
{
kod = lines[1];

switch (kod)
{
case "ESKİ":
durum.Attributes.Add("value", "0");
drm = "0";
break;

case "YENİ":
durum.Attributes.Add("value", "1");
drm = "1";
break;
}

urun_kodu_lbl.Text = "------";
urun_durumu_lbl.Text = kod;
}


//string SQL = "SELECT id FROM urun USE INDEX (id) WHERE MATCH (urun) AGAINST ('" + lines[0] + "')";
string SQL = "SELECT id FROM urun USE INDEX (id) WHERE urun='" + lines[0] + "'";
string snc = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL);
urun_id.Text = snc;
goster.Visible = true;
firma_id.Enabled = true;
kullanilabilir_adet_lbl.Text = "0";
kullanilabilir_adet.Text = "0";

string SQL2 = "SELECT firma_id,(SELECT adi FROM firma USE INDEX (id) WHERE id=firma_id) adi FROM depo USE INDEX (urun_id,durum,firma_id) WHERE urun_id='" + snc + "'  and durum = '" + drm + "' GROUP BY firma_id ORDER BY adi ASC";
DataSet ds2 = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL2, "depo");

if (ds2.Tables[0].Rows.Count > 0)
{
firma_id.Items.Clear();
firma_id.Items.Add(new ListItem("------ SEÇİNİZ ------", "-1"));

for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
{
firma_id.Items.Add(new ListItem(ds2.Tables[0].Rows[i]["adi"].ToString(), ds2.Tables[0].Rows[i]["firma_id"].ToString()));
}
}
else
{
goster.Visible = false;
firma_id.Items.Clear();
firma_id.Items.Add(new ListItem("------ SEÇİNİZ ------", "-1"));
firma_id.Enabled = false;
}

}
else
{
urun_id.Text = "0";
goster.Visible = false;
firma_id.Enabled = false;
}

}

protected void firma_id_SelectedIndexChanged(object sender, EventArgs e)
{
if (firma_id.SelectedItem.Value.ToString() != "-1")
{
string SQL = "SELECT CAST(IFNULL((SELECT adet FROM depo_urunler_toplamlar WHERE firma_id='" + firma_id.SelectedItem.Value.ToString() + "' and urun_id='" + urun_id.Text + "' and durum = '" + durum.Value + "'),'0')AS CHAR)";
kullanilabilir_adet_lbl.Text = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL).Replace("-", "");
kullanilabilir_adet.Text = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL).Replace("-", "");
}
else
{
kullanilabilir_adet_lbl.Text = "0";
kullanilabilir_adet.Text = "0";
}
}

}