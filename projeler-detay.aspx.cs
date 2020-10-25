using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class projeler_detay : System.Web.UI.Page
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
}
}
}

protected void GridStilleri()
{
// KOLONLARI KENDİSİ OLUŞTURSUN MU //
tablo1.AutoGenerateColumns = false;
tablo2.AutoGenerateColumns = false;
tablo3.AutoGenerateColumns = false;

// SIRALAMA YAPACAK MI? //
tablo1.AllowSorting = true;
tablo2.AllowSorting = true;
tablo3.AllowSorting = true;

// SAYFALAMA YAPACAK MI? //
tablo1.AllowPaging = true;
tablo2.AllowPaging = true;
tablo3.AllowSorting = true;

// SAYFA SAYISI //
tablo1.PageSize = Class.Degiskenler.SayfaSayisi;
tablo2.PageSize = Class.Degiskenler.SayfaSayisi;
tablo3.PageSize = Class.Degiskenler.SayfaSayisi;

// SAYFALAMA NEREDE GÖRÜNECEK //
tablo1.PagerSettings.Position = PagerPosition.TopAndBottom;
tablo2.PagerSettings.Position = PagerPosition.TopAndBottom;
tablo3.PagerSettings.Position = PagerPosition.TopAndBottom;

// SAYFALAMA FORMATI //
tablo1.PagerSettings.Mode = PagerButtons.NumericFirstLast;
tablo2.PagerSettings.Mode = PagerButtons.NumericFirstLast;
tablo3.PagerSettings.Mode = PagerButtons.NumericFirstLast;

// SAYFALAMA KAÇAR KAÇAR GİDECEK //
tablo1.PagerSettings.PageButtonCount = Class.Degiskenler.SayfadaGorunecekRakamSayisi;
tablo2.PagerSettings.PageButtonCount = Class.Degiskenler.SayfadaGorunecekRakamSayisi;
tablo3.PagerSettings.PageButtonCount = Class.Degiskenler.SayfadaGorunecekRakamSayisi;

// SAYFALAMA FORMATI GERİ TUŞU //
tablo1.PagerSettings.FirstPageText = Class.Degiskenler.IlkSayfaOku;
tablo2.PagerSettings.FirstPageText = Class.Degiskenler.IlkSayfaOku;
tablo3.PagerSettings.FirstPageText = Class.Degiskenler.IlkSayfaOku;

// SAYFALAMA FORMATI İLERİ TUŞU //
tablo1.PagerSettings.LastPageText = Class.Degiskenler.SonSayfaOku;
tablo2.PagerSettings.LastPageText = Class.Degiskenler.SonSayfaOku;
tablo3.PagerSettings.LastPageText = Class.Degiskenler.SonSayfaOku;

// SAYFALAMA STİLİ //
tablo1.PagerStyle.CssClass = "gridsayfalama";
tablo2.PagerStyle.CssClass = "gridsayfalama";
tablo3.PagerStyle.CssClass = "gridsayfalama";

// GRİD TOOLTİP //
tablo1.ToolTip = "Projeye Eklenen Ürünler";
tablo2.ToolTip = "Projeye Eklenen Personeller";
tablo3.ToolTip = "Projedeki Personellere Eklenen Ürünler";

// APTAL CSSLERİ TEMİZLE //
tablo1.GridLines = GridLines.None;
tablo1.CellSpacing = -1;
tablo2.GridLines = GridLines.None;
tablo2.CellSpacing = -1;
tablo3.GridLines = GridLines.None;
tablo3.CellSpacing = -1;

// VALIDLEME //
tablo1.Attributes.Add("summary", "");
tablo2.Attributes.Add("summary", "");
tablo3.Attributes.Add("summary", "");
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


protected void GridYukle1()
{
string SQL = "SELECT id,(SELECT urun FROM urun USE INDEX (id) WHERE id=urun_id) as urun,(SELECT kod FROM urun USE INDEX (id) WHERE id=urun_id) as kod,durum,adet FROM proje_urun USE INDEX (proje_id) WHERE proje_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "proje_urun");
Cache["proje_urun"] = ds;

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

protected void GridYuklenince1(object sender, EventArgs e)
{
GridStilleri();
GridYukle1();
//GridYukle2();
}

protected void GridSayfala1(object sender, GridViewPageEventArgs e)
{
tablo1.PageIndex = e.NewPageIndex;
tablo1.DataBind();
}

protected void OnRowDataBound1(object sender, GridViewRowEventArgs e)
{

if (e.Row.RowType == DataControlRowType.Header)
{
e.Row.Cells[0].Visible = false;
}

if (e.Row.RowType == DataControlRowType.DataRow)
{
Label ID_Al = (Label)e.Row.FindControl("id") as Label;
string ID = ID_Al.Text;

e.Row.Cells[0].Visible = false;

e.Row.Cells[1].Style.Value = "background:none #fff;text-align:center";

e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;

for (int i = 0; i < tablo1.Rows.Count + 1; i++)
{
e.Row.Cells[1].Text = (tablo1.PageSize * (tablo1.PageIndex) + (i + 1)).ToString();
}

switch (e.Row.Cells[4].Text)
{
case "0":
e.Row.Cells[4].Text = "Eski";
break;

case "1":
e.Row.Cells[4].Text = "Yeni";
break;
}

e.Row.Cells[5].Text = Class.Fonksiyonlar.SayiFormatla(e.Row.Cells[5].Text);
}

}

protected void GridSirala1(object sender, GridViewSortEventArgs e)
{
DataTable dt = ((DataSet)Cache["proje_urun"]).Tables[0];

if (dt != null)
{
string sira = SiralamaDegistir(e.SortExpression, e.SortDirection);

DataView dv = new DataView(dt);
dv.Sort = e.SortExpression + " " + sira;

tablo1.DataSource = dv;
tablo1.DataBind();
}
}

protected void GridYukle2()
{
string SQL = "SELECT id,(SELECT tc_kimlik FROM personel USE INDEX (id) WHERE id=personel_id) as tc_kimlik,(SELECT adi_soyadi FROM personel USE INDEX (id) WHERE id=personel_id) as adi_soyadi,(SELECT cinsiyet FROM personel USE INDEX (id) WHERE id=personel_id) as cinsiyet,(SELECT sertifika_durumu FROM personel USE INDEX (id) WHERE id=personel_id) as sertifika_durumu FROM proje_personel USE INDEX (proje_id) WHERE proje_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "proje_personel");
Cache["proje_personel"] = ds;

/*
DataView dv = new DataView(dt);
dv.Sort = "tarih DESC";

if (dv.Count > 0)
{
tablo2.DataSource = dv;
tablo2.DataBind();
}
*/

tablo2.DataSource = ds;
tablo2.DataBind();
}

protected void GridYuklenince2(object sender, EventArgs e)
{
GridStilleri();
GridYukle2();
}

protected void GridSayfala2(object sender, GridViewPageEventArgs e)
{
tablo2.PageIndex = e.NewPageIndex;
tablo2.DataBind();
}

protected void OnRowDataBound2(object sender, GridViewRowEventArgs e)
{

if (e.Row.RowType == DataControlRowType.Header)
{
e.Row.Cells[0].Visible = false;
}

if (e.Row.RowType == DataControlRowType.DataRow)
{
Label ID_Al = (Label)e.Row.FindControl("id") as Label;
string ID = ID_Al.Text;

e.Row.Cells[0].Visible = false;

e.Row.Cells[1].Style.Value = "background:none #fff;text-align:center";

e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

for (int i = 0; i < tablo2.Rows.Count + 1; i++)
{
e.Row.Cells[1].Text = (tablo2.PageSize * (tablo2.PageIndex) + (i + 1)).ToString();
}

switch (e.Row.Cells[4].Text)
{
case "0":
e.Row.Cells[4].Text = "Erkek";
break;

case "1":
e.Row.Cells[4].Text = "Bayan";
break;
}

switch (e.Row.Cells[5].Text)
{
case "0":
e.Row.Cells[5].Text = "Silahsız";
break;

case "1":
e.Row.Cells[5].Text = "Silahlı";
break;
}


}

}

protected void GridSirala2(object sender, GridViewSortEventArgs e)
{
DataTable dt = ((DataSet)Cache["proje_personel"]).Tables[0];

if (dt != null)
{
string sira = SiralamaDegistir(e.SortExpression, e.SortDirection);

DataView dv = new DataView(dt);
dv.Sort = e.SortExpression + " " + sira;

tablo2.DataSource = dv;
tablo2.DataBind();
}
}

protected void GridYukle3()
{
string SQL = "SELECT id,(SELECT tc_kimlik FROM personel USE INDEX (id) WHERE id=personel_id) as tc_kimlik,(SELECT adi_soyadi FROM personel USE INDEX (id) WHERE id=personel_id) as adi_soyadi,(SELECT urun FROM urun USE INDEX (id) WHERE id=urun_id) as urun,(SELECT kod FROM urun USE INDEX (id) WHERE id=urun_id) as kod,durum,adet,tamam FROM proje_personel_urun USE INDEX (proje_id) WHERE proje_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "proje_personel_urun");
Cache["proje_personel_urun"] = ds;

/*
DataView dv = new DataView(dt);
dv.Sort = "tarih DESC";

if (dv.Count > 0)
{
tablo3.DataSource = dv;
tablo3.DataBind();
}
*/

tablo3.DataSource = ds;
tablo3.DataBind();
}

protected void GridYuklenince3(object sender, EventArgs e)
{
GridStilleri();
GridYukle3();
}

protected void GridSayfala3(object sender, GridViewPageEventArgs e)
{
tablo3.PageIndex = e.NewPageIndex;
tablo3.DataBind();
}

protected void OnRowDataBound3(object sender, GridViewRowEventArgs e)
{

if (e.Row.RowType == DataControlRowType.Header)
{
e.Row.Cells[0].Visible = false;
e.Row.Cells[8].Visible = false;
}

if (e.Row.RowType == DataControlRowType.DataRow)
{
Label ID_Al = (Label)e.Row.FindControl("id") as Label;
string ID = ID_Al.Text;

Label tamam_lbl = (Label)e.Row.FindControl("tamam") as Label;
string tamam = tamam_lbl.Text;

e.Row.Cells[0].Visible = false;
e.Row.Cells[8].Visible = false;

e.Row.Cells[1].Style.Value = "background:none #fff;text-align:center";

e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

//e.Row.Cells[6].CssClass = "tdb2";

for (int i = 0; i < tablo3.Rows.Count + 1; i++)
{
e.Row.Cells[1].Text = (tablo3.PageSize * (tablo3.PageIndex) + (i + 1)).ToString();
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

}

}

protected void GridSirala3(object sender, GridViewSortEventArgs e)
{
DataTable dt = ((DataSet)Cache["proje_personel_urun"]).Tables[0];

if (dt != null)
{
string sira = SiralamaDegistir(e.SortExpression, e.SortDirection);

DataView dv = new DataView(dt);
dv.Sort = e.SortExpression + " " + sira;

tablo3.DataSource = dv;
tablo3.DataBind();
}
}

protected void VeriGetir()
{
string SQL = "SELECT * FROM proje USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"])+ "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "proje");

if (ds.Tables[0].Rows.Count > 0)
{

switch (ds.Tables[0].Rows[0]["onay"].ToString())
{
case "0":
durum.Text = "<b>Pasif</b>";
break;

case "1":
durum.Text = "Aktif";
break;
}

bolge_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(ds.Tables[0].Rows[0]["bolge_id"].ToString());
firma_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(ds.Tables[0].Rows[0]["firma_id"].ToString());
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

telefon.Text = Class.Fonksiyonlar.TelFormatla(ds.Tables[0].Rows[0]["tel"].ToString());

adres.Text = ds.Tables[0].Rows[0]["adres"].ToString();
notlar.Text = ds.Tables[0].Rows[0]["notlar"].ToString();

admin_id.Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(ds.Tables[0].Rows[0]["admin_id"].ToString()) + " - " + ds.Tables[0].Rows[0]["tarih"].ToString();
}

}

}