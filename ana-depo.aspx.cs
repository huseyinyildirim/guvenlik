using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ana_depo : System.Web.UI.Page
{
decimal toplamkayit, sayfaadet, limit = 0;

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "database.png", "Depo İşlemleri");

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
//tablo1.ToolTip = "Depodaki Tüm İşlemler";

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

string SQL = "SELECT id,bolge_id,firma_id,proje_id,urun_id,(SELECT urun FROM urun USE INDEX (id) WHERE id=urun_id) as urun_adi,(SELECT kod FROM urun USE INDEX (id) WHERE id=urun_id) as urun_kodu,durum,adet,tip,admin_id,tarih,onay FROM depo USE INDEX (bolge_id,firma_id,tip) WHERE bolge_id!='0' or firma_id!='0' or tip!='2' " + Class.Fonksiyonlar.SiteFonksiyonlari.AdminSQL() + " LIMIT " + limit + "," + Class.Degiskenler.SayfalamaSayisi + "";
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
string SQL = "SELECT COUNT(id) FROM depo USE INDEX (bolge_id,firma_id,tip) WHERE bolge_id!='0' or firma_id!='0' or tip!='2' " + Class.Fonksiyonlar.SiteFonksiyonlari.AdminSQL() + "";
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

nc1.Text = "<h3><img src=\"images/ikon/folder-open.png\" class=\"ikon\" alt=\"\"/> Depodaki Tüm İşlemler</h3>";
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
e.Row.Cells[12].Visible = false;
e.Row.Cells[13].Visible = false;
e.Row.Cells[14].Visible = false;
e.Row.Cells[15].Visible = false;
}

if (e.Row.RowType == DataControlRowType.DataRow)
{
Label ID_Al = (Label)e.Row.FindControl("id") as Label;
string ID = ID_Al.Text;

Label UrunID = (Label)e.Row.FindControl("urun_id") as Label;
Label BolgeID = (Label)e.Row.FindControl("bolge_id") as Label;
Label FirmaID = (Label)e.Row.FindControl("firma_id") as Label;

Label ProjeID_Al = (Label)e.Row.FindControl("proje_id") as Label;
string ProjeID = ProjeID_Al.Text;

string SQL = "SELECT COUNT(adet) FROM depo_urunler_say WHERE urun_id='" + UrunID.Text + "'";
string UrunSayisi = Class.Fonksiyonlar.MySQL.ExecuteScalar(SQL);

e.Row.Cells[0].Visible = false;
e.Row.Cells[12].Visible = false;
e.Row.Cells[13].Visible = false;
e.Row.Cells[14].Visible = false;
e.Row.Cells[15].Visible = false;

e.Row.Cells[1].Style.Value = "background:none #fff;text-align:center";

e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;

e.Row.Cells[10].CssClass = "tdb2";
e.Row.Cells[11].CssClass = "tdb2";


for (int i = 0; i < tablo1.Rows.Count + 1; i++)
{
e.Row.Cells[1].Text = Class.Fonksiyonlar.SayiFormatla((limit + i + 1).ToString());
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

e.Row.Cells[5].Text = Class.Fonksiyonlar.SayiFormatla(e.Row.Cells[5].Text.Replace("-", ""));

e.Row.Cells[7].Text = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVeTipVerID(e.Row.Cells[7].Text);

string sonuc, sonuc2 = string.Empty;
if (e.Row.Cells[3].Text != "&nbsp;")
{
sonuc = e.Row.Cells[2].Text + " adındaki " + e.Row.Cells[3].Text + " kod numarasına sahip ürün, " + e.Row.Cells[8].Text + " tarihinde, " + e.Row.Cells[7].Text.Replace("<i>", "").Replace("</i>", "") + " isimli yönetici tarafından " + Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(BolgeID.Text) + " bölgesinde bulunan, " + Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(FirmaID.Text) + " isimli firmaya, " + e.Row.Cells[5].Text + " adet olarak sevk edilmiştir.";
}
else
{
sonuc = e.Row.Cells[2].Text + " adındaki ürün, " + e.Row.Cells[8].Text + " tarihinde, " + e.Row.Cells[7].Text.Replace("<i>", "").Replace("</i>", "") + " isimli yönetici tarafından " + Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(BolgeID.Text) + " bölgesinde bulunan, " + Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(FirmaID.Text) + " isimli firmaya, " + e.Row.Cells[5].Text + " adet olarak sevk edilmiştir.";
}

if (ProjeID != "0")
{
sonuc2 = e.Row.Cells[8].Text + " tarihinde, " + e.Row.Cells[7].Text.Replace("<i>", "").Replace("</i>", "") + " isimli yönetici tarafından " + Class.Fonksiyonlar.SiteFonksiyonlari.BolgeAdiVer(BolgeID.Text) + " bölgesinde bulunan, " + Class.Fonksiyonlar.SiteFonksiyonlari.FirmaAdiVer(FirmaID.Text) + " isimli firmanın deposundan, " + Class.Fonksiyonlar.SiteFonksiyonlari.ProjeAdiVer(ProjeID) + " isimli projeye, " + e.Row.Cells[5].Text + " adet zimmetlenmiştir.";
}

switch (e.Row.Cells[6].Text)
{
case "0":
e.Row.Cells[6].Text = "Giriş";
break;

case "1":
if (ProjeID == "0")
{
e.Row.Cells[6].Text = "<img src=\"images/ikon/info.png\" class=\"ikon\" alt=\"\"/><span class=\"alignmid\"><a href=\"javascript:void(0)\" title=\"" + sonuc + "\" class=\"urunler_cikis\" id=\"urunler_cikis_" + ID + "\">Çıkış</a></span>";
}
else
{
e.Row.Cells[6].Text = "<img src=\"images/ikon/info_p.png\" class=\"ikon\" alt=\"\"/><span class=\"alignmid\"><a href=\"javascript:void(0)\" title=\"" + sonuc2 + "\" class=\"urunler_cikis\" id=\"urunler_cikis_" + ID + "\">Çıkış</a></span>";
}
e.Row.Cells[9].Text = "------";
break;

case "2":
e.Row.Cells[6].Text = "<img src=\"images/ikon/info_k.png\" class=\"ikon\" alt=\"\"/><span class=\"alignmid\"><a href=\"javascript:void(0)\" title=\"" + sonuc.Replace("firmaya","firma tarafından").Replace("sevk","imha") + "\" class=\"urunler_cikis\" id=\"urunler_cikis_" + ID + "\">İmha</a></span>";
e.Row.Cells[9].Text = "------";
break;
}

switch (e.Row.Cells[9].Text)
{
case "0":
e.Row.Cells[9].Text = "<b>Pasif</b>";
if (UrunSayisi=="1")
{
e.Row.Cells[11].Text = "<a href=\"?onay=ver&amp;id=" + ID + "\" id=\"urunler_onay_ver_" + ID + "\"><img src=\"images/ikon/onayla2.png\" class=\"ikon\" alt=\"\"/></a>";
}
else
{
e.Row.Cells[11].Text = "<img src=\"images/ikon/yok.png\" class=\"ikon\" alt=\"\"/>";
}
break;

case "1":
e.Row.Cells[9].Text = "Aktif";
if (UrunSayisi == "1")
{
e.Row.Cells[11].Text = "<a href=\"?onay=kaldir&amp;id=" + ID + "\" id=\"urunler_onay_kaldir_" + ID + "\"><img src=\"images/ikon/onayla.png\" class=\"ikon\" alt=\"\"/></a>";
}
else
{
e.Row.Cells[11].Text = "<img src=\"images/ikon/yok.png\" class=\"ikon\" alt=\"\"/>";
}
break;

case "------":
e.Row.Cells[9].Text = "------";
e.Row.Cells[11].Text = "<img src=\"images/ikon/yok.png\" class=\"ikon\" alt=\"\"/>";
break;
}
 
if (e.Row.Cells[9].Text=="------")
{
e.Row.Cells[10].Text = "<img src=\"images/ikon/yok.png\" class=\"ikon\" alt=\"\"/>";
}
else
{
if (UrunSayisi == "1")
{
e.Row.Cells[10].Text = "<a href=\"ana-depo-urun-guncelle.aspx?id=" + ID + "\" id=\"urunler_guncelle_" + ID + "\"><img src=\"images/ikon/guncelle.png\" class=\"ikon\" alt=\"\"/></a>";
}
else
{
e.Row.Cells[10].Text = "<img src=\"images/ikon/yok.png\" class=\"ikon\" alt=\"\"/>";
}
}
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

string SQL = "UPDATE depo SET onay='" + deger + "' WHERE id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL);
Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir,"İlgili kaydın onay durumu başarıyla değiştirilmiştir.",1);
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

}