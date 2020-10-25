using System;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class firmalar_ekle : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "asterisk.png", "Firma İşlemleri");

if (!IsCallback && !IsPostBack)
{
BolgeYukle();
ilYukle();

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

protected void submit_Click(object sender, EventArgs e)
{
if (Class.Degiskenler.OturumVarmi == 1)
{
Ekle();
}
}

protected void Ekle()
{
string ilce_kodu = string.Empty;

if (Class.Fonksiyonlar.SQLTemizle(Request.Form["ilce_kodu"]) == "")
{
ilce_kodu = "0";
}


string SQL = "INSERT INTO firma (bolge_id,adi,tel,faks,mail,vergi_dairesi,vergi_no,ticaret_sicil_no,il_kodu,ilce_kodu,adres,admin_id) values (@bolge_id,@adi,@tel,@faks,@mail,@vergi_dairesi,@vergi_no,@ticaret_sicil_no,@il_kodu,@ilce_kodu,@adres,@admin_id)";
MySqlCommand VeriGir = new MySqlCommand(SQL, Class.Fonksiyonlar.MySQL.Baglanti);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir.Parameters.AddWithValue("@bolge_id", Class.Fonksiyonlar.SQLTemizle(Request.Form["bolge_id"]));
VeriGir.Parameters.AddWithValue("@adi", Class.Fonksiyonlar.SQLTemizle(Request.Form["adi"]));
VeriGir.Parameters.AddWithValue("@tel", Class.Fonksiyonlar.SQLTemizle(Request.Form["tel"]));
VeriGir.Parameters.AddWithValue("@faks", Class.Fonksiyonlar.SQLTemizle(Request.Form["faks"]));
VeriGir.Parameters.AddWithValue("@mail", Class.Fonksiyonlar.SQLTemizle(Request.Form["mail"]));
VeriGir.Parameters.AddWithValue("@vergi_dairesi", Class.Fonksiyonlar.SQLTemizle(Request.Form["vergi_dairesi"]));
VeriGir.Parameters.AddWithValue("@vergi_no", Class.Fonksiyonlar.SQLTemizle(Request.Form["vergi_no"]));
VeriGir.Parameters.AddWithValue("@ticaret_sicil_no", Class.Fonksiyonlar.SQLTemizle(Request.Form["ticaret_sicil_no"]));
VeriGir.Parameters.AddWithValue("@il_kodu", Class.Fonksiyonlar.SQLTemizle(Request.Form["il_kodu"]));
VeriGir.Parameters.AddWithValue("@ilce_kodu",ilce_kodu);
VeriGir.Parameters.AddWithValue("@adres", Class.Fonksiyonlar.SQLTemizle(Request.Form["adres"]));
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

protected void bolge_id_SelectedIndexChanged(object sender, EventArgs e)
{

}
}