using System;
using System.Data;
using MySql.Data.MySqlClient;

public partial class bolgeler_ekle : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

        Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "web.png", "Bölge İşlemleri");

        if (!IsCallback && !IsPostBack)
        {
            admin_id.Text = Class.Degiskenler.AdminAdSoyad;
            IlGetir();
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
        string SQL = "INSERT INTO bolge (adi,il_kodlari,admin_id) values (@adi,@il_kodlari,@admin_id)";
        MySqlCommand VeriGir = new MySqlCommand(SQL, Class.Fonksiyonlar.MySQL.Baglanti);

        if (Class.Fonksiyonlar.MySQL.Baglanti.State == ConnectionState.Closed)
        {
            Class.Fonksiyonlar.MySQL.Baglanti.Open();
        }
        try
        {
            VeriGir.Parameters.AddWithValue("@adi", Class.Fonksiyonlar.SQLTemizle(Request.Form["adi"]));
            VeriGir.Parameters.AddWithValue("@il_kodlari", Class.Fonksiyonlar.SQLTemizle(Request.Form["il_kodlari"]));
            VeriGir.Parameters.AddWithValue("@admin_id", Class.Degiskenler.AdminID);
            VeriGir.ExecuteNonQuery();
        }
        finally
        {
            Class.Fonksiyonlar.MySQL.Baglanti.Close();
            Class.Fonksiyonlar.MySQL.Baglanti.Dispose();
        }

        Class.Fonksiyonlar.SiteFonksiyonlari.MesajYaz(mesaj_getir, "Bilgileriniz başarıyla eklenmiştir.", 0);
    }

    protected void IlGetir()
    {
        string SQL = "SELECT il_kodu,il FROM il USE INDEX (il_goster) WHERE il_goster='0' ORDER BY il ASC";
        DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "il");

        if (ds.Tables[0].Rows.Count > 0)
        {
            il_listele.DataSource = ds;
            il_listele.DataBind();
        }
    }

}