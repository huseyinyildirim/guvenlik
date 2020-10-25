using System;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI;

public partial class projeler_personel_ekle : System.Web.UI.Page
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

GridYuklenince(null, null);
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
tablo1.AllowPaging = false;

// GRİD TOOLTİP //
tablo1.ToolTip = "Projeye Eklenen Personel";

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
string SQL = "SELECT CAST(IFNULL((SELECT tamam FROM proje_personel USE INDEX (personel_id,bolge_id,firma_id) WHERE personel_id=personel.id and bolge_id=personel.bolge_id and firma_id=personel.firma_id and proje_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "'),'0')AS CHAR(1)) AS tamam,id,tc_kimlik,adi_soyadi,cinsiyet,sertifika_durumu FROM personel USE INDEX (bolge_id,firma_id) WHERE bolge_id='" + bolge_id_js.Text + "' and firma_id='" + firma_id_js.Text + "'";
DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "personel");
Cache["personel"] = ds;

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
}

protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
{

if (e.Row.RowType == DataControlRowType.Header)
{
e.Row.Cells[0].Visible = false;
e.Row.Cells[7].Visible = false;

CheckBox cb = new CheckBox();
cb.ID = "chck_sec";
e.Row.Cells[6].Controls.Add(cb);
}

if (e.Row.RowType == DataControlRowType.DataRow)
{
Label ID_Al = (Label)e.Row.FindControl("id") as Label;
string ID = ID_Al.Text;

e.Row.Cells[0].Visible = false;
e.Row.Cells[7].Visible = false;

e.Row.Cells[1].Style.Value = "background:none #fff;text-align:center";

e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

e.Row.Cells[6].CssClass = "tdb2";

for (int i = 0; i < tablo1.Rows.Count + 1; i++)
{
e.Row.Cells[1].Text = (i + 1).ToString();
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

CheckBox cb = (CheckBox)e.Row.FindControl("chck") as CheckBox;
Label tamam_Al = (Label)e.Row.FindControl("tamam") as Label;
string tamam = tamam_Al.Text;

if (tamam == "1")
{
cb.Visible = false;
e.Row.Cells[6].Text = "<img src=\"images/ikon/yok.png\" class=\"ikon\" alt=\"\"/>";
}
else
{
cb.InputAttributes["value"] = ID;
}

}

}

protected void GridSirala(object sender, GridViewSortEventArgs e)
{
DataTable dt = ((DataSet)Cache["personel"]).Tables[0];

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
int dg = 0;

for (int i = 0; i < tablo1.Rows.Count; i++)
{
CheckBox cb = (CheckBox)tablo1.Rows[i].FindControl("chck") as CheckBox;

if (cb.Checked)
{
string SQL1 = "INSERT INTO proje_personel (bolge_id,firma_id,proje_id,personel_id,admin_id) values (@bolge_id,@firma_id,@proje_id,@personel_id,@admin_id)";
MySqlCommand VeriGir = new MySqlCommand(SQL1, Class.Fonksiyonlar.MySQL.Baglanti);

if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
{
Class.Fonksiyonlar.MySQL.Baglanti.Open();
}
try
{
VeriGir.Parameters.AddWithValue("@bolge_id", bolge_id_js.Text);
VeriGir.Parameters.AddWithValue("@firma_id", firma_id_js.Text);
VeriGir.Parameters.AddWithValue("@proje_id", Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]));
VeriGir.Parameters.AddWithValue("@personel_id", cb.InputAttributes["value"]);
VeriGir.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
VeriGir.ExecuteNonQuery();
}
finally
{
Class.Fonksiyonlar.MySQL.Baglanti.Close();
Class.Fonksiyonlar.MySQL.Baglanti.Dispose();
}

string SQL2 = "UPDATE proje_personel SET tamam='1' WHERE proje_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"]) + "' and tamam='0'";
Class.Fonksiyonlar.MySQL.ExecuteNonQuery(SQL2);

dg = 1;
}

}

if (dg == 1)
{
GridYuklenince(null, null);
}

ClientScript.RegisterClientScriptBlock(this.GetType(), Class.Degiskenler.SiteAdi, Class.Fonksiyonlar.JavaScript.Yonlendir("projeler-personele-zimmet-ekle.aspx?id=" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["id"])), true);
}

}