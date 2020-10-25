using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ana_depo_urun_detay : System.Web.UI.Page
{
decimal toplamkayit, sayfaadet, limit = 0;

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "database.png", "Depo İşlemleri");

if (!IsCallback && !IsPostBack)
{
Sayfalama();

if (!Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))) || !Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["durum"]))))
{
Class.Fonksiyonlar.JavaScript.YonlendirNormal("/");
}
else if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) == "" || Class.Fonksiyonlar.SQLTemizle(Request.QueryString["durum"]) == "")
{
Class.Fonksiyonlar.JavaScript.YonlendirNormal("/");
}

VeriGetir();
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

public string SiralamaDegistir(string SortExpression, SortDirection currentSortDirection)
{
if (ViewState[SortExpression] == null)
ViewState[SortExpression] = currentSortDirection == SortDirection.Ascending ? "DESC" : "ASC";
else
ViewState[SortExpression] = ViewState[SortExpression].ToString() == "ASC" ? "DESC" : "ASC";

return ViewState[SortExpression].ToString();
}

protected void GridStilleri()
{
// KOLONLARI KENDİSİ OLUŞTURSUN MU //
tablo1.AutoGenerateColumns = false;

// SIRALAMA YAPACAK MI? //
tablo1.AllowSorting = true;

// GRİD TOOLTİP //
tablo1.ToolTip = "Depodaki İşlemler (Ürün Bazlı)";

// APTAL CSSLERİ TEMİZLE //
tablo1.GridLines = GridLines.None;
tablo1.CellSpacing = -1;

// VALIDLEME //
tablo1.Attributes.Add("summary", "");
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
if (Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["sayfa"]))))
{
if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["sayfa"]) != "")
{
limit = Convert.ToInt16(Class.Fonksiyonlar.SQLTemizle(Request.QueryString["sayfa"])) * Class.Degiskenler.SayfalamaSayisi;
}
if (limit < 0)
{
limit = 0;
}
}

string SQL = "SELECT IFNULL((SELECT adi FROM bolge USE INDEX (id) WHERE id=bolge_id),'MERKEZ DEPO') AS bolge,IFNULL((SELECT adi FROM firma USE INDEX (id) WHERE id=firma_id),'MERKEZ DEPO') AS firma,IFNULL((SELECT adi FROM proje USE INDEX (id) WHERE id=proje_id),'------') AS proje,IFNULL((SELECT tc_kimlik FROM personel USE INDEX (id) WHERE id=personel_id),'------') AS tc_kimlik,IFNULL((SELECT adi_soyadi FROM personel USE INDEX (id) WHERE id=personel_id),'------') AS adi_soyadi,adet,tip,admin_id,tarih FROM depo USE INDEX (bolge_id,firma_id,tip,urun_id,durum,tarih) WHERE (bolge_id!='0' or firma_id!='0' or tip!='2') and urun_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' and durum='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["durum"]) + "' " + Class.Fonksiyonlar.SiteFonksiyonlari.AdminSQL() + " ORDER BY tarih ASC LIMIT " + limit + "," + Class.Degiskenler.SayfalamaSayisi + "";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "depo");
Cache["depo"] = ds;

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

protected void Sayfalama()
{
string SQL = "SELECT COUNT(id) FROM depo USE INDEX (bolge_id,firma_id,tip,urun_id,durum) WHERE (bolge_id!='0' or firma_id!='0' or tip!='2') and urun_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' and durum='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["durum"]) + "' " + Class.Fonksiyonlar.SiteFonksiyonlari.AdminSQL() + "";
toplamkayit = Convert.ToDecimal(Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL));

if (toplamkayit > 0)
{
sayfaadet = toplamkayit / Class.Degiskenler.SayfalamaSayisi;
decimal f = sayfaadet;

string[] splt = f.ToString().Split(',');

try
{
f = f + 1;
}
catch
{
//
}

sayfalama_literal1.Text = string.Empty;
sayfalama_literal2.Text = string.Empty;

if (sayfaadet > 1)
{
for (int i = 0; i + 1 < f; i++)
{
sayfalama_literal1.Text = sayfalama_literal1.Text + "<a href=\"?id=" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "&amp;durum=" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["durum"]) + "&amp;sayfa=" + i + "\" id=\"sayfalama1_a_" + i + "\">" + (i + 1) + "</a> " + Class.Degiskenler.vbCrLf;
sayfalama_literal2.Text = sayfalama_literal2.Text + "<a href=\"?id=" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "&amp;durum=" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["durum"]) + "&amp;sayfa=" + i + "\" id=\"sayfalama2_a_" + i + "\">" + (i + 1) + "</a> " + Class.Degiskenler.vbCrLf;
}

sayfalama_literal1.Text = "<!--SAYFALAMA-->" + Class.Degiskenler.vbCrLf + "<div id=\"sayfalama1\">" + Class.Degiskenler.vbCrLf + "<p>" + sayfalama_literal1.Text + "</p>" + Class.Degiskenler.vbCrLf + "</div>" + Class.Degiskenler.vbCrLf + "<!--SAYFALAMA-->";
sayfalama_literal2.Text = "<!--SAYFALAMA-->" + Class.Degiskenler.vbCrLf + "<div id=\"sayfalama2\">" + Class.Degiskenler.vbCrLf + "<p>" + sayfalama_literal2.Text + "</p>" + Class.Degiskenler.vbCrLf + "</div>" + Class.Degiskenler.vbCrLf + "<!--SAYFALAMA-->";
}
}

}

protected void SayfaOlaylariYaz()
{
if (tablo1.Rows.Count > 0)
{
GridViewRow rh2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
Literal nc2 = new Literal();
TableHeaderCell hc2 = new TableHeaderCell();

try
{
nc2.Text = "<img src=\"images/ikon/info.png\" class=\"ikon\" alt=\"\"/> <span>Bu tablo " + Class.Fonksiyonlar.SayiFormatla(toplamkayit.ToString()) + " kayıttan ve " + Class.Fonksiyonlar.SayiFormatla(Convert.ToInt16(sayfaadet).ToString()) + " sayfadan oluşmaktadır. Şuan sayfa " + (Convert.ToInt16(Class.Fonksiyonlar.SQLTemizle(Request.QueryString["sayfa"])) + 1) + " 'i görüntülemektesiniz.</span>";
hc2.Controls.Add(nc2);
hc2.ColumnSpan = tablo1.Columns.Count;
hc2.CssClass = "tdb3";
rh2.Cells.Add(hc2);
rh2.Visible = true;
tablo1.Controls[0].Controls.AddAt(1, rh2);
}
catch
{
//
}
}
}

protected void GridYuklenince(object sender, EventArgs e)
{
GridStilleri();
GridYukle();
SayfaOlaylariYaz();
Sayfalama();
}

protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
{
if (e.Row.RowType == DataControlRowType.DataRow)
{
e.Row.Cells[0].Style.Value = "background:none #fff;text-align:center";

e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;

for (int i = 0; i < tablo1.Rows.Count + 1; i++)
{
e.Row.Cells[0].Text = Class.Fonksiyonlar.SayiFormatla((limit + i + 1).ToString());
}

e.Row.Cells[6].Text = Class.Fonksiyonlar.SayiFormatla(e.Row.Cells[6].Text.Replace("-",""));

switch (e.Row.Cells[7].Text)
{
case "0":
e.Row.Cells[7].Text = "Giriş";
break;

case "1":
e.Row.Cells[7].Text = "Çıkış";
break;

case "2":
e.Row.Cells[7].Text = "İmha";
break;
}

e.Row.Cells[8].Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(e.Row.Cells[8].Text);
}

}

protected void GridSirala(object sender, GridViewSortEventArgs e)
{
DataTable dt = ((DataSet)Cache["depo"]).Tables[0];

if (dt != null)
{
string sira = SiralamaDegistir(e.SortExpression, e.SortDirection);

DataView dv = new DataView(dt);
dv.Sort = e.SortExpression + " " + sira;

tablo1.DataSource = dv;
tablo1.DataBind();
}

SayfaOlaylariYaz();
}


protected void VeriGetir()
{
switch (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["durum"]))
{
case "0":
durumu_lbl.Text = "ESKİ";
break;

case "1":
durumu_lbl.Text = "YENİ";
break;
}

string SQL1 = "SELECT adet FROM depo_urunler_toplamlar WHERE urun_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' and durum='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["durum"]) + "' " + Class.Fonksiyonlar.SiteFonksiyonlari.AdminSQL() + "";
kalan_lbl.Text = Class.Fonksiyonlar.SayiFormatla(Class.Fonksiyonlar.MySQL.ExecuteScalar_BosDegerDondur(SQL1));

string SQL2 = "SELECT urun,kod FROM urun USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL2, "urun");

adi_lbl.Text = ds.Tables[0].Rows[0]["urun"].ToString();
kodu_lbl.Text = ds.Tables[0].Rows[0]["kod"].ToString();

}

}