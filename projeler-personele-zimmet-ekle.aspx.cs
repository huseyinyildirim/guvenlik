using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI;

public partial class projeler_personele_zimmet_ekle : System.Web.UI.Page
{
protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "finance.png", "Proje İşlemleri");

if (!IsCallback && !IsPostBack)
{

if (Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))))
{
VeriGetir();
PersonelGetir();
}

if (Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))) && Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["sil"]))))
{
Sil();
}

}

}

private ViewStateSerializer.ViewStateSerializer _persister = null;
protected override PageStatePersister PageStatePersister
{
get
{
if (_persister == null)
{
_persister = new ViewStateSerializer.ViewStateSerializer(Page, ViewStateSerializer.ViewStateSerializer.SecurityLevel.Low, false);
//_persister.CompressPage(); // optional (compress all output HTML page) if have problems, comment it
}
return _persister;
}
}

protected void GridStilleri()
{
// KOLONLARI KENDİSİ OLUŞTURSUN MU //
tablo1.AutoGenerateColumns = false;

// SIRALAMA YAPACAK MI? //
tablo1.AllowSorting = true;

// SAYFALAMA YAPACAK MI? //
tablo1.AllowPaging = true;

// SAYFA SAYISI //
tablo1.PageSize = Class.Degiskenler.SayfaSayisi;

// SAYFALAMA NEREDE GÖRÜNECEK //
tablo1.PagerSettings.Position = PagerPosition.TopAndBottom;

// SAYFALAMA FORMATI //
tablo1.PagerSettings.Mode = PagerButtons.NumericFirstLast;

// SAYFALAMA KAÇAR KAÇAR GİDECEK //
tablo1.PagerSettings.PageButtonCount = Class.Degiskenler.SayfadaGorunecekRakamSayisi;

// SAYFALAMA FORMATI GERİ TUŞU //
tablo1.PagerSettings.FirstPageText = Class.Degiskenler.IlkSayfaOku;

// SAYFALAMA FORMATI İLERİ TUŞU //
tablo1.PagerSettings.LastPageText = Class.Degiskenler.SonSayfaOku;

// SAYFALAMA STİLİ //
tablo1.PagerStyle.CssClass = "gridsayfalama";

// GRİD TOOLTİP //
tablo1.ToolTip = "Projeye Eklenen Ürünler";

// APTAL CSSLERİ TEMİZLE //
tablo1.GridLines = GridLines.None;
tablo1.CellSpacing = -1;

// VALIDLEME //
tablo1.Attributes.Add("summary", "");
}

protected void VeriGetir()
{
string SQL = "SELECT * FROM proje USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "proje");

bolge_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(ds.Tables[0].Rows[0]["bolge_id"].ToString());
bolge_id_js.Text = ds.Tables[0].Rows[0]["bolge_id"].ToString();
firma_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(ds.Tables[0].Rows[0]["firma_id"].ToString());
firma_id_js.Text = ds.Tables[0].Rows[0]["firma_id"].ToString();
personel_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.PersonelAdSoyadVer(ds.Tables[0].Rows[0]["yonetici_id"].ToString());

baslangic.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["baslangic"]).ToShortDateString();

if (ds.Tables[0].Rows[0]["bitis"].ToString() != "")
{
bitis.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["bitis"]).ToShortDateString();
}
else
{
bitis.Text = "------";
}


adi.Text = ds.Tables[0].Rows[0]["adi"].ToString();
sorumlu.Text = ds.Tables[0].Rows[0]["sorumlu"].ToString();

admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();
}

protected void PersonelGetir()
{
string SQL = "SELECT personel_id,(SELECT adi_soyadi FROM personel USE INDEX (id) WHERE id=personel_id) AS adi_soyadi FROM proje_personel USE INDEX (proje_id,tamam) WHERE proje_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' and tamam='1' ORDER BY adi_soyadi ASC";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "proje_personel");

if (ds.Tables[0].Rows.Count > 0)
{
for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
{
proje_personelleri.Items.Add(new ListItem(ds.Tables[0].Rows[i]["adi_soyadi"].ToString(), ds.Tables[0].Rows[i]["personel_id"].ToString()));
}
}

}


public string SiralamaDegistir(string SortExpression, SortDirection currentSortDirection)
{
if (ViewState[SortExpression] == null)
ViewState[SortExpression] = currentSortDirection == SortDirection.Ascending ? "DESC" : "ASC";
else
ViewState[SortExpression] = ViewState[SortExpression].ToString() == "ASC" ? "DESC" : "ASC";

return ViewState[SortExpression].ToString();
}

protected void MouseOverStilleri(object sender, GridViewRowEventArgs e)
{
if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Normal)
e.Row.CssClass = "normal";

if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
e.Row.CssClass = "alternate";
}


protected void GridYukle()
{
string SQL = "SELECT id,(SELECT tc_kimlik FROM personel USE INDEX (id) WHERE id=personel_id) as tc_kimlik,(SELECT adi_soyadi FROM personel USE INDEX (id) WHERE id=personel_id) as adi_soyadi,(SELECT urun FROM urun USE INDEX (id) WHERE id=urun_id) as urun,(SELECT kod FROM urun USE INDEX (id) WHERE id=urun_id) as kod,durum,adet,tamam FROM proje_personel_urun USE INDEX (proje_id) WHERE proje_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "proje_personel_urun");
Cache["proje_personel_urun"] = ds;

/*
DataView dv = new DataView(dt);
dv.Sort = "tarih DESC";

if (dv.Count > 0)
{
tablo1.DataSource = dv;
tablo1.DataBind();
}
*/

tablo1.DataSource = ds;
tablo1.DataBind();
}

protected void GridYuklenince(object sender, EventArgs e)
{
GridStilleri();
GridYukle();
tr_count.Text = tablo1.Rows.Count.ToString();
}

protected void GridSayfala(object sender, GridViewPageEventArgs e)
{
tablo1.PageIndex = e.NewPageIndex;
tablo1.DataBind();
}

protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
{

if (e.Row.RowType == DataControlRowType.Header)
{
e.Row.Cells[0].Visible = false;
e.Row.Cells[9].Visible = false;
}

if (e.Row.RowType == DataControlRowType.DataRow)
{
Label ID_Al = (Label)e.Row.FindControl("id") as Label;
string ID = ID_Al.Text;

Label tamam_lbl = (Label)e.Row.FindControl("tamam") as Label;
string tamam = tamam_lbl.Text;

e.Row.Cells[0].Visible = false;
e.Row.Cells[9].Visible = false;

e.Row.Cells[1].Style.Value = "background:none #fff;text-align:center";

e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

//e.Row.Cells[6].CssClass = "tdb2";

for (int i = 0; i < tablo1.Rows.Count + 1; i++)
{
e.Row.Cells[1].Text = (tablo1.PageSize * (tablo1.PageIndex) + (i + 1)).ToString();
}

switch (e.Row.Cells[6].Text)
{
case "0":
e.Row.Cells[6].Text = "Eski";
break;

case "1":
e.Row.Cells[6].Text = "Yeni";
break;
}

e.Row.Cells[7].Text = Class.Fonksiyonlar.SayiFormatla(e.Row.Cells[7].Text);

switch (tamam_lbl.Text)
{
case "0":
e.Row.Cells[8].Text = "<a href=\"?id=" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "&amp;sil=" + ID + "\" class=\"sil\" id=\"proje_urun_sil_" + ID + "\"><img src=\"images/ikon/sil.png\" class=\"ikon\" alt=\"\"/></a>";
break;

case "1":
e.Row.Cells[8].Text = "<img src=\"images/ikon/yok.png\" class=\"ikon\" alt=\"\"/>";
break;
}

}

}

protected void GridSirala(object sender, GridViewSortEventArgs e)
{
DataTable dt = ((DataSet)Cache["proje_personel_urun"]).Tables[0];

if (dt != null)
{
string sira = SiralamaDegistir(e.SortExpression, e.SortDirection);

DataView dv = new DataView(dt);
dv.Sort = e.SortExpression + " " + sira;

tablo1.DataSource = dv;
tablo1.DataBind();
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
if (Class.Fonksiyonlar.SQLTemizle(Request.Form["tr_count"]) != "0")
{
string SQL1 = "SELECT personel_id,urun_id,durum,adet FROM proje_personel_urun USE INDEX (proje_id,tamam) WHERE proje_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' and tamam='0'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL1,"proje_urun");

if (ds.Tables[0].Rows.Count > 0)
{
for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
{
string urun_id = ds.Tables[0].Rows[i]["urun_id"].ToString();
string durum = ds.Tables[0].Rows[i]["durum"].ToString();
string adet = ds.Tables[0].Rows[i]["adet"].ToString();
string personel_id = ds.Tables[0].Rows[i]["personel_id"].ToString();

string SQL2 = "INSERT INTO depo (bolge_id,firma_id,proje_id,personel_id,urun_id,durum,adet,tip,admin_id) values (@bolge_id,@firma_id,@proje_id,@personel_id,@urun_id,@durum,@adet,@tip,@admin_id)";
MySqlCommand VeriGir1 = new MySqlCommand(SQL2, Class.Fonksiyonlar.MySQL.Baglanti);
MySqlCommand VeriGir2 = new MySqlCommand(SQL2, Class.Fonksiyonlar.MySQL.Baglanti);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir1.Parameters.AddWithValue("@bolge_id", bolge_id_js.Text);
VeriGir1.Parameters.AddWithValue("@firma_id", firma_id_js.Text);
VeriGir1.Parameters.AddWithValue("@proje_id", Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]));
VeriGir1.Parameters.AddWithValue("@personel_id", personel_id);
VeriGir1.Parameters.AddWithValue("@urun_id", urun_id);
VeriGir1.Parameters.AddWithValue("@durum", durum);
VeriGir1.Parameters.AddWithValue("@adet",adet);
VeriGir1.Parameters.AddWithValue("@tip", "1");
VeriGir1.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
VeriGir1.ExecuteNonQuery();

VeriGir2.Parameters.AddWithValue("@bolge_id","0");
VeriGir2.Parameters.AddWithValue("@firma_id","0");
VeriGir2.Parameters.AddWithValue("@proje_id", Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]));
VeriGir2.Parameters.AddWithValue("@personel_id", personel_id);
VeriGir2.Parameters.AddWithValue("@urun_id", urun_id);
VeriGir2.Parameters.AddWithValue("@durum", durum);
VeriGir2.Parameters.AddWithValue("@adet", "-" + adet);
VeriGir2.Parameters.AddWithValue("@tip", "2");
VeriGir2.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
VeriGir2.ExecuteNonQuery();
}
finally
{
Class.Fonksiyonlar.MySQL.Baglanti.Close();
Class.Fonksiyonlar.MySQL.Baglanti.Dispose();
}

}

string SQL3 = "UPDATE proje_personel_urun SET tamam='1' WHERE proje_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' and tamam='0'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL3);
}

}

Response.Redirect("projeler.aspx");
}

protected void sec_Click(object sender, EventArgs e)
{
if (Class.Fonksiyonlar.SQLTemizle(Request.Form["urun"]) != "" && Class.Fonksiyonlar.SQLTemizle(Request.Form["adet"]) != "" && Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))))
{
string[] lines = Regex.Split(Class.Fonksiyonlar.SQLTemizle(Request.Form["urun"]), " - ");
string urun_durumu, urun_durumu_son = string.Empty;

//string SQL1 = "SELECT id FROM urun USE INDEX (id) WHERE MATCH (urun) AGAINST ('" + lines[0] + "')";
string SQL1 = "SELECT id FROM urun USE INDEX (id) WHERE urun='" + lines[0] + "'";
string urun_id = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL1);

if (lines.Length > 3)
{
urun_durumu = lines[2];

switch (urun_durumu)
{
case "ESKİ":
urun_durumu_son = "0";
break;

case "YENİ":
urun_durumu_son = "1";
break;
}

}
else
{
urun_durumu = lines[1];

switch (urun_durumu)
{
case "ESKİ":
urun_durumu_son = "0";
break;

case "YENİ":
urun_durumu_son = "1";
break;
}

}

if (tum_personel.Checked)
{
for (int i = 1; i < proje_personelleri.Items.Count; i++)
{
string SQL2 = "SELECT COUNT(id) FROM proje_personel_urun USE INDEX (proje_id,urun_id,personel_id,durum) WHERE proje_id ='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' and urun_id ='" + urun_id + "' and personel_id ='" + proje_personelleri.Items[i].Value + "' and durum ='" + urun_durumu_son + "'";
string sonuc = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL2);

if (sonuc == "0")
{
string SQL3 = "INSERT INTO proje_personel_urun (bolge_id,firma_id,proje_id,personel_id,urun_id,durum,adet,admin_id) values (@bolge_id,@firma_id,@proje_id,@personel_id,@urun_id,@durum,@adet,@admin_id)";
MySqlCommand VeriGir = new MySqlCommand(SQL3, Class.Fonksiyonlar.MySQL.Baglanti);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir.Parameters.AddWithValue("@bolge_id", bolge_id_js.Text);
VeriGir.Parameters.AddWithValue("@firma_id", firma_id_js.Text);
VeriGir.Parameters.AddWithValue("@proje_id", Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]));
VeriGir.Parameters.AddWithValue("@personel_id", proje_personelleri.Items[i].Value);
VeriGir.Parameters.AddWithValue("@urun_id", urun_id);
VeriGir.Parameters.AddWithValue("@durum", urun_durumu_son);
VeriGir.Parameters.AddWithValue("@adet", Class.Fonksiyonlar.SQLTemizle(Request.Form["adet"]));
VeriGir.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
VeriGir.ExecuteNonQuery();
}
finally
{
Class.Fonksiyonlar.MySQL.Baglanti.Close();
Class.Fonksiyonlar.MySQL.Baglanti.Dispose();
}

tr_count.Text = (Convert.ToDecimal(tr_count.Text) + 1).ToString();
}
}

}
else
{
if (proje_personelleri.SelectedItem.Value != "0")
{
string SQL2 = "SELECT COUNT(id) FROM proje_personel_urun USE INDEX (proje_id,urun_id,personel_id,durum) WHERE proje_id ='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' and urun_id ='" + urun_id + "' and personel_id ='" + proje_personelleri.SelectedItem.Value + "' and durum ='" + urun_durumu_son + "'";
string sonuc = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL2);

if (sonuc == "0")
{
string SQL3 = "INSERT INTO proje_personel_urun (bolge_id,firma_id,proje_id,personel_id,urun_id,durum,adet,admin_id) values (@bolge_id,@firma_id,@proje_id,@personel_id,@urun_id,@durum,@adet,@admin_id)";
MySqlCommand VeriGir = new MySqlCommand(SQL3, Class.Fonksiyonlar.MySQL.Baglanti);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir.Parameters.AddWithValue("@bolge_id", bolge_id_js.Text);
VeriGir.Parameters.AddWithValue("@firma_id", firma_id_js.Text);
VeriGir.Parameters.AddWithValue("@proje_id", Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]));
VeriGir.Parameters.AddWithValue("@personel_id", proje_personelleri.SelectedItem.Value);
VeriGir.Parameters.AddWithValue("@urun_id", urun_id);
VeriGir.Parameters.AddWithValue("@durum", urun_durumu_son);
VeriGir.Parameters.AddWithValue("@adet", Class.Fonksiyonlar.SQLTemizle(Request.Form["adet"]));
VeriGir.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
VeriGir.ExecuteNonQuery();
}
finally
{
Class.Fonksiyonlar.MySQL.Baglanti.Close();
Class.Fonksiyonlar.MySQL.Baglanti.Dispose();
}

tr_count.Text = (Convert.ToDecimal(tr_count.Text) + 1).ToString();
}
}

}

GridYukle();
}

}

protected void Sil()
{
if (Class.Degiskenler.OturumVarmi == 1)
{
string SQL = "DELETE FROM proje_personel_urun WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["sil"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir, "İlgili kayıt başarıyla silinmiştir.", 0);
tr_count.Text = (Convert.ToDecimal(tr_count.Text) - 1).ToString();
}
}

}