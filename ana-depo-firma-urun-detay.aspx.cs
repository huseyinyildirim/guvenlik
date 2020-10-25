using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ana_depo_firma_urun_detay : System.Web.UI.Page
{
decimal toplamkayit, sayfaadet, limit = 0;
string imha = string.Empty;

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "database.png", "Depo İşlemleri");

if (!IsCallback && !IsPostBack)
{
Sayfalama();

if (!Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))))
{
Class.Fonksiyonlar.JavaScript.YonlendirNormal("/");
}
else if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) == "")
{
Class.Fonksiyonlar.JavaScript.YonlendirNormal("/");
}

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) == "0")
{
imha = "and tip!='2'";
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
tablo1.ToolTip = "Depodaki İşlemler (Firma Bazlı)";

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

string SQL = "SELECT (SELECT urun FROM urun USE INDEX (id) WHERE id=urun_id) as urun,(SELECT kod FROM urun USE INDEX (id) WHERE id=urun_id) as kod,proje_id,durum,adet,admin_id,tip,tarih FROM depo USE INDEX (firma_id,tarih) WHERE firma_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' " + imha + " ORDER BY tarih ASC LIMIT " + limit + "," + Class.Degiskenler.SayfalamaSayisi + "";
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
string SQL = "SELECT COUNT(id) FROM depo USE INDEX (firma_id) WHERE firma_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' " + imha + "";
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
sayfalama_literal1.Text = sayfalama_literal1.Text + "<a href=\"?id=" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "&amp;sayfa=" + i + "\" id=\"sayfalama1_a_" + i + "\">" + (i + 1) + "</a> " + Class.Degiskenler.vbCrLf;
sayfalama_literal2.Text = sayfalama_literal2.Text + "<a href=\"?id=" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "&amp;sayfa=" + i + "\" id=\"sayfalama2_a_" + i + "\">" + (i + 1) + "</a> " + Class.Degiskenler.vbCrLf;
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

if (e.Row.RowType == DataControlRowType.Header)
{
e.Row.Cells[8].Visible = false;
}

if (e.Row.RowType == DataControlRowType.DataRow)
{
Label ProjeID_Al = (Label)e.Row.FindControl("proje_id") as Label;
string ProjeID = ProjeID_Al.Text;

e.Row.Cells[8].Visible = false;

e.Row.Cells[0].Style.Value = "background:none #fff;text-align:center";

e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;

for (int i = 0; i < tablo1.Rows.Count + 1; i++)
{
e.Row.Cells[0].Text = Class.Fonksiyonlar.SayiFormatla((limit + i + 1).ToString());
}

switch (e.Row.Cells[3].Text)
{
case "0":
e.Row.Cells[3].Text = "Eski";
break;

case "1":
e.Row.Cells[3].Text = "Yeni";
break;
}

switch (e.Row.Cells[5].Text)
{
case "0":
e.Row.Cells[5].Text = "Depo Giriş";
break;

case "1":
if (ProjeID == "0")
{
e.Row.Cells[5].Text = "Giriş";
}
else
{
e.Row.Cells[5].Text = "Çıkış";
}
break;

case "2":
e.Row.Cells[5].Text = "İmha";
break;
}

e.Row.Cells[4].Text = Class.Fonksiyonlar.SayiFormatla(e.Row.Cells[4].Text.Replace("-", ""));

e.Row.Cells[6].Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(e.Row.Cells[6].Text);
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
string SQL1 = "SELECT bolge_id FROM firma USE INDEX (id) WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
bolge_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL1));
firma_lbl.Text = Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]));
string SQL2 = "SELECT adet FROM depo_urunler_toplamlar WHERE firma_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
urun_adet_lbl.Text = Class.Fonksiyonlar.SayiFormatla(Class.Fonksiyonlar.MySQL.ExecuteScalar_BosDegerDondur(SQL2)).Replace("-", "");
string SQL3 = "SELECT SUM(adet) FROM depo_urunler_say WHERE firma_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' " + imha + "";
islem_adet_lbl.Text = Class.Fonksiyonlar.SayiFormatla(Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL3));
}

}