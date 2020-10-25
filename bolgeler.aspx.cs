using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bolgeler : System.Web.UI.Page
{
decimal toplamkayit, sayfaadet, limit = 0;

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "web.png", "Bölge İşlemleri");

if (!IsCallback && !IsPostBack)
{
Sayfalama();

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["onay"]) == "ver" || Class.Fonksiyonlar.SQLTemizle(Request.QueryString["onay"]) == "kaldir")
{
if (Class.Fonksiyonlar.NumericKontrol((Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]))))
{
Onayİslemleri();
}
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
tablo1.ToolTip = "Bölgeler";

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

//0 Genel Yönetici (Root)
//1 Bölge Yöneticisi
//2 Firma Yöneticisi
//3 Genel Depo Yöneticisi
//4 Firma Depo Yöneticisi

string SQL;
switch (Class.Degiskenler.AdminTip)
{
case "1":
SQL = "SELECT * FROM bolge USE INDEX (id) WHERE id!='0' and id=" + Class.Degiskenler.AdminBolgeID + " LIMIT " + limit + "," + Class.Degiskenler.SayfalamaSayisi + "";
break;

case "2":
SQL = "SELECT * FROM bolge USE INDEX (id) WHERE id!='0' and id=" + Class.Degiskenler.AdminBolgeID + " LIMIT " + limit + "," + Class.Degiskenler.SayfalamaSayisi + "";
break;

default:
SQL = "SELECT * FROM bolge USE INDEX (id) WHERE id!='0' LIMIT " + limit + "," + Class.Degiskenler.SayfalamaSayisi + "";
break;
}

DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "bolge");
Cache["bolge"] = ds;

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
//0 Genel Yönetici (Root)
//1 Bölge Yöneticisi
//2 Firma Yöneticisi
//3 Genel Depo Yöneticisi
//4 Firma Depo Yöneticisi

string SQL;
switch (Class.Degiskenler.AdminTip)
{
    case "1":
        SQL = "SELECT COUNT(id) FROM bolge USE INDEX (id) WHERE id!='0' and id=" + Class.Degiskenler.AdminBolgeID + "";
        break;

    case "2":
        SQL = "SELECT COUNT(id) FROM bolge USE INDEX (id) WHERE id!='0' and id=" + Class.Degiskenler.AdminBolgeID + "";
        break;

    default:
        SQL="SELECT COUNT(id) FROM bolge USE INDEX (id) WHERE id!='0'";
        break;
}

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
sayfalama_literal1.Text = sayfalama_literal1.Text + "<a href=\"?sayfa=" + i + "\" id=\"sayfalama1_a_" + i + "\">" + (i + 1) + "</a> " + Class.Degiskenler.vbCrLf;
sayfalama_literal2.Text = sayfalama_literal2.Text + "<a href=\"?sayfa=" + i + "\" id=\"sayfalama2_a_" + i + "\">" + (i + 1) + "</a> " + Class.Degiskenler.vbCrLf;
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
GridViewRow rh1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
GridViewRow rh2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

Literal nc1 = new Literal();
Literal nc2 = new Literal();

TableHeaderCell hc1 = new TableHeaderCell();
TableHeaderCell hc2 = new TableHeaderCell();

nc1.Text = "<h3><img src=\"images/ikon/folder-open.png\" class=\"ikon\" alt=\"\"/> Bölgeler</h3>";
hc1.Controls.Add(nc1);
hc1.ColumnSpan = tablo1.Columns.Count;
hc1.CssClass = "tdb3";
rh1.Cells.Add(hc1);
rh1.Visible = true;
tablo1.Controls[0].Controls.AddAt(0, rh1);

try
{
nc2.Text = "<img src=\"images/ikon/info.png\" class=\"ikon\" alt=\"\"/> <span>Bu tablo " + Class.Fonksiyonlar.SayiFormatla(toplamkayit.ToString()) + " kayıttan ve " + Class.Fonksiyonlar.SayiFormatla(Convert.ToInt16(sayfaadet).ToString()) + " sayfadan oluşmaktadır. Şuan sayfa " + (Convert.ToInt16(Class.Fonksiyonlar.SQLTemizle(Request.QueryString["sayfa"])) + 1) + " 'i görüntülemektesiniz.</span>";
hc2.Controls.Add(nc2);
hc2.ColumnSpan = tablo1.Columns.Count;
hc2.CssClass = "tdb3";
rh2.Cells.Add(hc2);
rh2.Visible = true;
tablo1.Controls[0].Controls.AddAt(tablo1.Rows.Count+2, rh2);
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
e.Row.Cells[0].Visible = false;
}

if (e.Row.RowType == DataControlRowType.DataRow)
{
Label ID_Al = (Label)e.Row.FindControl("id") as Label;
string ID = ID_Al.Text;

e.Row.Cells[0].Visible = false;
e.Row.Cells[1].Style.Value = "background:none #fff;text-align:center";

e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;

e.Row.Cells[6].CssClass = "tdb2";
e.Row.Cells[7].CssClass = "tdb2";
e.Row.Cells[8].CssClass = "tdb2";

for (int i = 0; i < tablo1.Rows.Count + 1; i++)
{
e.Row.Cells[1].Text = Class.Fonksiyonlar.SayiFormatla((limit + i + 1).ToString());
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////
Label il_kodlari = (Label)e.Row.FindControl("il_kodlari") as Label;

string[] dizi = il_kodlari.Text.Split(',');

il_kodlari.Text = string.Empty;
for (int j = 0; j < dizi.Length; j++)
{
il_kodlari.Text = il_kodlari.Text + Class.Fonksiyonlar.SiteFonksiyonlari.ilAdiVer(dizi[j].ToString()) + " , ";
}

il_kodlari.Text = il_kodlari.Text.Substring(0, il_kodlari.Text.Length - 3);
////////////////////////////////////////////////////////////////////////////////////////////////////////////

e.Row.Cells[4].Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(e.Row.Cells[4].Text);
    
switch (e.Row.Cells[5].Text)
{
case "0":
e.Row.Cells[5].Text = "<b>Pasif</b>";
e.Row.Cells[8].Text = "<a href=\"?onay=ver&amp;id=" + ID + "\" id=\"bolgeler_onay_ver_" + ID + "\"><img src=\"images/ikon/onayla2.png\" class=\"ikon\" alt=\"\"/></a>";
break;

case "1":
e.Row.Cells[5].Text = "Aktif";
e.Row.Cells[8].Text = "<a href=\"?onay=kaldir&amp;id=" + ID + "\" id=\"bolgeler_onay_kaldir_" + ID + "\"><img src=\"images/ikon/onayla.png\" class=\"ikon\" alt=\"\"/></a>";
break;
}
 
e.Row.Cells[6].Text = "<a href=\"bolgeler-detay.aspx?id=" + ID + "\" id=\"bolgeler_detay_" + ID + "\"><img src=\"images/ikon/notes.png\" class=\"ikon\" alt=\"\"/></a>";
e.Row.Cells[7].Text = "<a href=\"bolgeler-guncelle.aspx?id=" + ID + "\" id=\"bolgeler_guncelle_" + ID + "\"><img src=\"images/ikon/guncelle.png\" class=\"ikon\" alt=\"\"/></a>";
}

}

protected void Onayİslemleri()
{
if (Class.Degiskenler.OturumVarmi == 1)
{
int deger = 0;

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["onay"]) == "ver")
{
deger = 1;
}

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["onay"]) == "kaldir")
{
deger = 0;
}

string SQL = "UPDATE bolge SET onay='" + deger + "' WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir, "İlgili kaydın onay durumu başarıyla değiştirilmiştir.", 1);
}
}

protected void GridSirala(object sender, GridViewSortEventArgs e)
{
DataTable dt = ((DataSet)Cache["bolge"]).Tables[0];

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

}