using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;

public static class Class                                           //Class baþlýyor.
{

    public class Degiskenler                                            //Deðiþkenler
    {
        public static string SiteAdi = "karunsosyalhizmetler.com";                    //Session ve cookie de kullanýlýyor. Bir de MD5 'de secret key!
        public static string FirmaAdi = "Karun Sosyal Hizmetler";           //Title vs. gibi yerlerde kullanýlýyor.

        public static int SayfalamaSayisi = 100;                            //Grid sayfa sayýsý.
        public static int OturumVarmi = 0;

        public static string vbCrLf = "\r\n";                               //Satýr atlatmada kullanýlýyor.
        public static string TamAdres = String.Empty;                       //Tam adresi veriyor.

        public static string AdminKullaniciAdi = String.Empty;              //Kullanici adini veriyor.
        public static string AdminSifre = String.Empty;                     //Þifre veriyor.
        public static string AdminAdSoyad = String.Empty;                   //Admin adý ve soyadýný veriyor.
        public static string AdminID = String.Empty;                        //Admin ID sini veriyor.
        public static string AdminTip = String.Empty;                       //0 Genel Yönetici (Root), 1 Bölge Yöneticisi, 2 Firma Yöneticisi, 3 Genel Depo Yöneticisi, 4 Firma Depo Yöneticisi
        public static string AdminBolgeID = String.Empty;
        public static string AdminFirmaID = String.Empty;

        //MYSQL Bilgileri
        public static string MysqlSunucu = "localhost"; //89.19.19.139
        public static string MysqlPort = "3306";
        public static string MysqlVeritabani = "guvenlik";
        public static string MysqlKullanici = "root";
        public static string MysqlSifre = "50567232";
        //MYSQL Bilgileri

        //HazýrGridler Ýçin
        public static int SayfaSayisi = 10;
        public static int SayfadaGorunecekRakamSayisi = 10;
        public static string SonSayfaOku = "»";
        public static string IlkSayfaOku = "«";
        //HazýrGridler Ýçin

        /*
        public static string LogAdi = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "-").Replace(".", "-");
        public static string LogYolu = "logs\\";  
        public static string LogKayit = "OFF"; //OFF olursa kapalý ON olursa Açýk
        public static string KayitYok = "<img src=\"images/ikon/yok.png\" class=\"ikon\" alt=\"\"/> <span style=\"vertical-align:middle\">Kayýt bulunamadý.</span>";
        public static string ViewStateSikistir = "ON"; //OFF olursa kapalý ON olursa Açýk
        */
    }

    public class Fonksiyonlar                                           //Tüm fonksiyonlar
    {

        public static string TelFormatla(string x)                          //Telefon numarasý formatlýyor.
        {
            string telsonuc = String.Empty;

            if (x != null || x == "")
            {
                if (x.Length.ToString() == "13")
                {
                    string bir = x.Substring(0, 3);
                    string iki = x.Substring(3, 10);

                    telsonuc = "(" + bir + ") " + iki;
                }
                else
                {
                    telsonuc = "------";
                }
            }
            return telsonuc;
        }

        public static string SayiFormatla(string s)                         //Sayý formatlýyor.
        {
            string sayim = string.Empty;

            try
            {
                decimal ss = Convert.ToDecimal(s);
                sayim = String.Format("{0:#,###.####}", ss);
                string sm = sayim.Substring(0, 1);

                if (sm == ",")
                {
                    sayim = 0 + sayim;
                }
                else
                {
                    sayim = sayim.ToString();
                }

            }
            catch
            {
                sayim = "0";
            }

            return sayim;
        }

        public static bool NumericKontrol(string word)                      //Gelen deðer numerik mi deðil mi boolean olarak kontrol ediyor.
        {
            int numeric;

            if (int.TryParse(word, out numeric))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string MD5Sifrele(string str)                         //Kullanýcýnýn girdiði þifreyi secret key kullanarak MD5 yapýyor.
        {
            string password1 = FormsAuthentication.HashPasswordForStoringInConfigFile(str + "-" + Degiskenler.SiteAdi, "sha1");
            string password2 = FormsAuthentication.HashPasswordForStoringInConfigFile(password1, "md5");
            string password3 = FormsAuthentication.HashPasswordForStoringInConfigFile(password2, "md5");
            return password3;
        }


        public class CiktiVer                                             //Export fonksiyonlarý burada.
        {

            public static void Word(string SQL, string tablo, string adi)
            {
                DataSet ds = MySQL.DataSetGetir(SQL, tablo);
                DataTable dt = ds.Tables[0];

                //Create a dummy GridView
                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = dt;
                GridView1.DataBind();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + adi + ".doc");
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView1.RenderControl(hw);
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }

            public static void Excel(string SQL, string tablo, string adi)
            {
                DataSet ds = MySQL.DataSetGetir(SQL, tablo);
                DataTable dt = ds.Tables[0];

                //Create a dummy GridView
                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = dt;
                GridView1.DataBind();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + adi + ".xls");
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    //Apply text style to each Row
                    GridView1.Rows[i].Attributes.Add("class", "textmode");
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style>.textmode {mso-number-format:\@;}</style>";
                HttpContext.Current.Response.Write(style);
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }

            public static void Pdf(string SQL, string tablo, string adi)
            {
                DataSet ds = MySQL.DataSetGetir(SQL, tablo);
                DataTable dt = ds.Tables[0];

                //Create a dummy GridView
                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = dt;
                GridView1.DataBind();

                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + adi + ".pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView1.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.End();
            }

            public static void Csv(string SQL, string tablo, string adi)
            {
                DataSet ds = MySQL.DataSetGetir(SQL, tablo);
                DataTable dt = ds.Tables[0];

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + adi + ".csv");
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/text";

                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Columns[k].ColumnName + ',');
                }
                //append new line
                sb.Append("\r\n");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                    }
                    //append new line
                    sb.Append("\r\n");
                }
                HttpContext.Current.Response.Output.Write(sb.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }

        }

        public class JavaScript                                             //JavaScript fonksiyonlarý burada.
        {

            public class FaceBox                                                //Facebox burada.
            {

                /*
                public static string Normal(string str)                             //Facebox normal pencere.
                {
                StringBuilder sb = new StringBuilder();
                sb.Append("$(document).ready\n");
                sb.Append("(\n");
                sb.Append("function()\n");
                sb.Append("{\n");
                sb.Append("$.facebox.settings.opacity = 0.5;\n");
                sb.Append("jQuery.facebox('" + str + "')\n");
                sb.Append("}\n");
                sb.Append(")\n");
                return sb.ToString();
                }
                */

                /*
                public static string Yonlendir(string str1, string str2)            //Facebox týklayýnca yönlendiriyor.
                {
                StringBuilder sb = new StringBuilder();
                sb.Append("$(document).ready\n");
                sb.Append("(\n");
                sb.Append("function()\n");
                sb.Append("{\n");
                sb.Append("$.facebox.settings.opacity = 0.5;\n");
                sb.Append("jQuery.facebox('" + str1 + "')\n\n");

                sb.Append("$(\"#facebox_kapat\").click\n");
                sb.Append("(\n");
                sb.Append("function()\n");
                sb.Append("{\n");
                sb.Append("location.href=\"" + str2 + "\"\n");
                sb.Append("}\n");
                sb.Append(")\n\n");

                sb.Append("var KEYCODE_ESC = 27;\n");
                sb.Append("$(document).keyup(\n");
                sb.Append("function (e)\n");
                sb.Append("{\n");
                sb.Append("if (e.keyCode == KEYCODE_ESC)\n");
                sb.Append("{\n");
                sb.Append("location.href=\"" + str2 + "\"\n");
                sb.Append("}\n");
                sb.Append("}\n");
                sb.Append(")\n\n");

                sb.Append("}\n");
                sb.Append(")\n");
                return sb.ToString();
                }
                */

                public static string GeriGonder(string str)                         //Facebox týklayýnca geri gönderiyor.
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("$(document).ready\n");
                    sb.Append("(\n");
                    sb.Append("function()\n");
                    sb.Append("{\n");
                    sb.Append("$.facebox.settings.opacity = 0.5;\n");
                    sb.Append("jQuery.facebox('" + str + "')\n\n");

                    sb.Append("$(\"#facebox_kapat\").click\n");
                    sb.Append("(\n");
                    sb.Append("function()\n");
                    sb.Append("{\n");
                    sb.Append("window.history.back()\n");
                    sb.Append("}\n");
                    sb.Append(")\n\n");

                    sb.Append("var KEYCODE_ESC = 27;\n");
                    sb.Append("$(document).keyup(\n");
                    sb.Append("function (e)\n");
                    sb.Append("{\n");
                    sb.Append("if (e.keyCode == KEYCODE_ESC)\n");
                    sb.Append("{\n");
                    sb.Append("window.history.back()\n");
                    sb.Append("}\n");
                    sb.Append("}\n");
                    sb.Append(")\n\n");

                    sb.Append("}\n");
                    sb.Append(")\n");
                    return sb.ToString();
                }

            }

            public static string MesajKutusuVeYonlendir(string dgr1, string dgr2) //JS Mesaj kutusu vererek yönlendiriyor
            {
                return ("alert(\"" + dgr1.Replace("[ln]", @"\n") + "\");\n" + "location.href=\"" + dgr2 + "\"\n");
            }

            public static void MesajKutusuVeYonlendirNormal(string dgr1, string dgr2) //JS Mesaj kutusu vererek yönlendiriyor
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type=\"text/javascript\">\n");
                sb.Append("//<![CDATA[\n");
                sb.Append("alert(\"" + dgr1.Replace("[ln]", @"\n") + "\");\n");
                sb.Append("location.href=\"" + dgr2 + "\"\n");
                sb.Append("//]]>\n");
                sb.Append("</script>\n");
                HttpContext.Current.Response.Write(sb.ToString());
            }

            public static void MesajKutusuNormal(string dgr)                    //JS Mesaj kutusu.
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type=\"text/javascript\">\n");
                sb.Append("//<![CDATA[\n");
                sb.Append("alert(\"" + dgr.Replace("[ln]", @"\n") + "\")\n");
                sb.Append("//]]>\n");
                sb.Append("</script>\n");
                HttpContext.Current.Response.Write(sb.ToString());
            }

            /*
            public static string MesajKutusu(string dgr)                        //JS Mesaj kutusu.
            {
            return ("alert(\"" + dgr.Replace("[ln]", @"\n")  + "\")\n");
            }

            public static string GeriGonder()                                   //JS geri gönder.
            {
            return ("window.history.back()\n");
            }

            public static void GeriGonderNormal()                               //JS geri gönder.
            {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">\n");
            sb.Append("//<![CDATA[\n");
            sb.Append("window.history.back()\n");
            sb.Append("//]]>\n");
            sb.Append("</script>\n");
            HttpContext.Current.Response.Write(sb.ToString());
            }
            */

            public static string Yonlendir(string str)                          //JS yönlendir.
            {
                return ("location.href=\"" + str + "\"\n");
            }

            public static void YonlendirNormal(string str)                      //JS yönlendir.
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type=\"text/javascript\">\n");
                sb.Append("//<![CDATA[\n");
                sb.Append("location.href=\"" + str + "\"\n");
                sb.Append("//]]>\n");
                sb.Append("</script>\n");
                HttpContext.Current.Response.Write(sb.ToString());
            }

        }

        public static void TestMesajKutusu(string d)                        //WinForm tabanlý mesaj kutusu.
        {
            System.Windows.Forms.MessageBox.Show(d, "Test Mesajý", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            return;
        }

        public static string SQLTemizle(string str)                         //SQL Injection için temizle fonksiyonu.
        {
            try
            {
                if (str != string.Empty)
                {
                    str = str.Replace("'", "");
                    str = str.Replace("`", "");
                    str = str.Replace("’", "");

                    str = str.Replace("--", "");
                    str = str.Replace(";", "");
                    str = str.Replace("(", "");
                    str = str.Replace(")", "");
                    str = str.Replace("=", "");
                    str = str.Replace("<", "");
                    str = str.Replace(">", "");
                    str = str.Replace("%", "");
                    str = str.Replace("&", "");
                    str = str.Replace("$", "");
                    str = str.Replace("*", "");
                    str = str.Replace("@@", "");

                    str = str.Replace("!", "");
                    str = str.Replace("#", "");
                    str = str.Replace("?", "");
                    str = str.Replace("[", "");
                    str = str.Replace("\"", "");
                    str = str.Replace("]", "");
                    str = str.Replace("^", "");
                    str = str.Replace("{", "");
                    str = str.Replace("|", "");
                    str = str.Replace("}", "");
                    str = str.Replace("~", "");
                    str = str.Replace("+", "");
                    str = str.Replace("/", "");
                }
            }
            catch
            {
                //
            }
            return str;
        }

        public static string UrlvePathYaz()                                 //Tam URL yi yazýyor.
        {
            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (String.IsNullOrEmpty(protocol) | String.Equals(protocol, "0"))
                protocol = "http://";
            else
                protocol = "https://";

            string currentAddress = HttpContext.Current.Request.Url.ToString();
            Regex rx = new Regex(@"([^/]*)(localhost|\blocalhost:\d+\b)([^/]*)", RegexOptions.IgnoreCase);
            Match MatchObj = rx.Match(currentAddress);
            if (!(string.IsNullOrEmpty(MatchObj.Groups[0].Value)))
            {
                Degiskenler.TamAdres = String.Concat(protocol,
                MatchObj.Groups[0].Value,
                HttpContext.Current.Request.ApplicationPath);
            }
            else
            {
                Degiskenler.TamAdres = String.Concat(protocol,
                HttpContext.Current.Request.ServerVariables["SERVER_NAME"],
                HttpContext.Current.Request.ApplicationPath);
            }

            if (!Degiskenler.TamAdres.EndsWith("/"))
                Degiskenler.TamAdres += "/";

            return Degiskenler.TamAdres;
        }

        public static string MevcutSayfa()                                  //Bulunulan sayfanýn tam yolunu yazýyor.
        {
            return (HttpContext.Current.Request.Url.AbsoluteUri);
        }

        public class SiteFonksiyonlari                                      //Site fonksiyonlarý.
        {

            public static string AdminSQL()
            {
                string SQLSonuc = string.Empty;
                //0 Genel Yönetici (Root), 1 Bölge Yöneticisi, 2 Firma Yöneticisi, 3 Genel Depo Yöneticisi, 4 Firma Depo Yöneticisi

                string SQL = "SELECT bolge_id,firma_id FROM admin USE INDEX (id) WHERE id ='" + Degiskenler.AdminID + "'";
                DataSet ds = MySQL.DataSetGetir(SQL, "admin");

                switch (Degiskenler.AdminTip)
                {
                    case "0":
                        SQLSonuc = string.Empty;
                        break;

                    case "1":
                        SQLSonuc = "and bolge_id='" + ds.Tables[0].Rows[0]["bolge_id"].ToString() + "'";
                        break;

                    case "2":
                        SQLSonuc = "and firma_id='" + ds.Tables[0].Rows[0]["firma_id"].ToString() + "'";
                        break;

                    case "3":
                        SQLSonuc = string.Empty;
                        break;

                    case "4":
                        SQLSonuc = string.Empty;
                        break;
                }

                return (SQLSonuc);
            }

            public static void SessionKontrol()                                 //Session varmý ona bakýyor.
            {
                if (HttpContext.Current.Session["kullanici_adi"] == null || HttpContext.Current.Session["sifre"] == null)
                {
                    JavaScript.MesajKutusuVeYonlendirNormal("Oturumunuz doðrulanamadý veya oturum zaman aþýmýna uðradý.[ln]Lütfen yeniden giriþ yapýnýz.", "default.aspx");
                    Degiskenler.OturumVarmi = 0;
                }
                else
                {
                    Degiskenler.OturumVarmi = 1;
                }
            }

            public static void SessionVarmi()                                   //Session varmý ona bakýyor. Yoksa anasayfaya yönlendiriyor.
            {
                if (HttpContext.Current.Session["kullanici_adi"] != null || HttpContext.Current.Session["sifre"] != null)
                {
                    JavaScript.YonlendirNormal("panel.aspx");
                }
            }

            public static string PersonelAdSoyadVer(string str)                     //ID si gönderilen personelin adýný soyadýný veriyor.
            {
                string SQL = "SELECT adi_soyadi FROM personel USE INDEX (id) WHERE id ='" + str + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminIDVer(string str1, string str2)           //Adminin ID sini veriyor.
            {
                string SQL = "SELECT id FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminAdSoyadVer(string str1, string str2)         //Kullanýcý adý gönderilen adminin adýný soyadýný veriyor.
            {
                string SQL = "SELECT adi_soyadi FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminTipVer(string str1, string str2)              //ID si gönderilen adminin tipini veriyor.
            {
                string SQL = "SELECT tip FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminBolgeIDVer(string str1, string str2)              //ID si gönderilen adminin bölge ID sini veriyor.
            {
                string SQL = "SELECT bolge_id FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminFirmaIDVer(string str1, string str2)              //ID si gönderilen adminin firma ID sini veriyor.
            {
                string SQL = "SELECT firma_id FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminAdSoyadVerID(string str)                     //ID si gönderilen adminin adýný soyadýný veriyor.
            {
                string SQL = "SELECT adi_soyadi FROM admin USE INDEX (id) WHERE id ='" + str + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminAdSoyadVeTipVerID(string str)                //ID si gönderilen adminin adýný soyadýný ve tipini veriyor.
            {
                string SQL = "SELECT adi_soyadi,tip FROM admin USE INDEX (id) WHERE id ='" + str + "'";
                DataSet ds = MySQL.DataSetGetir(SQL, "admin");

                string tip = string.Empty;

                switch (ds.Tables[0].Rows[0]["tip"].ToString())
                {
                    case "0":
                        tip = "Genel Yönetici (Root)";
                        break;

                    case "1":
                        tip = "Bölge Yöneticisi";
                        break;

                    case "2":
                        tip = "Firma Yöneticisi";
                        break;

                    case "3":
                        tip = "Genel Depo Yöneticisi";
                        break;

                    case "4":
                        tip = "Firma Depo Yöneticisi";
                        break;
                }

                return (ds.Tables[0].Rows[0]["adi_soyadi"].ToString() + " <i>(" + tip + ")</i>");
            }

            public static string AdminKullaniciAdiKontrol(string str)           //Yeni giriþlerde veya güncellemelerde kullanýcý adýný kontrol ediyor.
            {
                string SQL = "SELECT COUNT(id) FROM admin WHERE kullanici_adi ='" + str + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string ilAdiVer(string str)                            //ID si gönderilen ilin adýný getiriyor.
            {
                if (str.Length < 2)
                {
                    str = 0 + str;
                }

                if (str != "00")
                {
                    string SQL = "SELECT il FROM il USE INDEX (il_kodu) WHERE il_kodu='" + str + "'";
                    string sonuc = MySQL.ExecuteScalar_BosDegerDondur(SQL);
                    if (sonuc == "")
                    {
                        return ("HATA");
                    }
                    else
                    {
                        return (sonuc);
                    }
                }
                else
                {
                    return ("------");
                }
            }

            public static string ilceAdiVer(string str)                          //ID si gönderilen ilçenin adýný getiriyor.
            {
                if (str != "0" || str != "")
                {
                    string SQL = "SELECT ilce FROM ilce USE INDEX (ilce_kodu) WHERE ilce_kodu='" + str + "'";
                    string sonuc = MySQL.ExecuteScalar_BosDegerDondur(SQL);
                    if (sonuc == "")
                    {
                        return ("HATA");
                    }
                    else
                    {
                        return (sonuc);
                    }
                }
                else
                {
                    return ("------");
                }
            }

            public static string BolgeAdiVer(string str)                            //ID si gönderilen bolgenin adýný getiriyor.
            {
                string SQL = "SELECT adi FROM bolge USE INDEX (id) WHERE id='" + str + "'";
                return (MySQL.ExecuteScalar_BosDegerDondur(SQL));
            }

            public static string FirmaAdiVer(string str)                            //ID si gönderilen firmanýn adýný getiriyor.
            {
                string SQL = "SELECT adi FROM firma USE INDEX (id) WHERE id='" + str + "'";
                return (MySQL.ExecuteScalar_BosDegerDondur(SQL));
            }

            public static string UrunAdiVer(string str)                            //ID si gönderilen ürünün adýný getiriyor.
            {
                if (str != "0" || str != "")
                {
                    string SQL = "SELECT urun FROM urun USE INDEX (id) WHERE id='" + str + "'";
                    string sonuc = MySQL.ExecuteScalar_BosDegerDondur(SQL);
                    if (sonuc == "")
                    {
                        return ("HATA");
                    }
                    else
                    {
                        return (sonuc);
                    }
                }
                else
                {
                    return ("------");
                }
            }

            public static string UrunKoduVer(string str)                            //ID si gönderilen ürünün kodunu getiriyor.
            {
                if (str != "0" || str != "")
                {
                    string SQL = "SELECT kod FROM urun USE INDEX (id) WHERE id='" + str + "'";
                    string sonuc = MySQL.ExecuteScalar_BosDegerDondur(SQL);
                    if (sonuc == "")
                    {
                        return ("HATA");
                    }
                    else
                    {
                        return (sonuc);
                    }
                }
                else
                {
                    return ("------");
                }
            }

            public static string ProjeAdiVer(string str)                            //ID si gönderilen projenin adýný getiriyor.
            {
                if (str != "0" || str != "")
                {
                    string SQL = "SELECT adi FROM proje USE INDEX (id) WHERE id='" + str + "'";
                    string sonuc = MySQL.ExecuteScalar_BosDegerDondur(SQL);
                    if (sonuc == "")
                    {
                        return ("HATA");
                    }
                    else
                    {
                        return (sonuc);
                    }
                }
                else
                {
                    return ("------");
                }
            }

            public static void BaslikYaz(Panel pnl, string rsm, string bslk)          //Baþlýðý yazýyor. Hem de User Control den çaðýrýyor.
            {
                UserControl BaslikEkle = (UserControl)((Page)HttpContext.Current.Handler).LoadControl("~/ascx/baslik.ascx");
                pnl.Controls.Add(BaslikEkle);

                System.Web.UI.WebControls.Image ikon = (System.Web.UI.WebControls.Image)BaslikEkle.FindControl("ikon") as System.Web.UI.WebControls.Image;
                ikon.ImageUrl = "~/images/ikon/" + rsm;

                Label baslik = (Label)BaslikEkle.FindControl("baslik") as Label;
                baslik.Text = bslk;
            }

            public static void MesajYaz(Panel pnl, string bslk, int durum)          //Mesaj yazýyor. Hem de User Control den çaðýrýyor.
            {
                UserControl MesajEkle = (UserControl)((Page)HttpContext.Current.Handler).LoadControl("~/ascx/mesaj.ascx");
                pnl.Controls.Add(MesajEkle);

                Label baslik = (Label)MesajEkle.FindControl("msg") as Label;
                baslik.Text = bslk;

                if (durum == 1)
                {
                    Literal yonlendir = (Literal)MesajEkle.FindControl("yonlendir") as Literal;
                    yonlendir.Visible = true;
                }

            }

        }

        public class MySQL                                                  //MySQL fonksiyonlarý.
        {

            public static string ConnectionString = "SERVER=" + Degiskenler.MysqlSunucu + ";" + "PORT=" + Degiskenler.MysqlPort + ";" + "DATABASE=" + Degiskenler.MysqlVeritabani + ";" + "USER ID=" + Degiskenler.MysqlKullanici + ";" + "PASSWORD=" + Degiskenler.MysqlSifre + ";" + "CHARSET=latin5;CONNECTION TIMEOUT=60;DEFAULT COMMAND TIMEOUT=60;POOLING=TRUE;MAX POOL SIZE=15;MIN POOL SIZE=5";
            public static MySqlConnection Baglanti = new MySqlConnection(ConnectionString);

            public static DataSet DataSetGetir(string sqlstr, string tabloadi)
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlstr, Baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds, tabloadi);
                return ds;
            }

            public static void ExecuteNonQuery(string sql)                      //ExecuteNonQuery (DELETE,UPDATE vb. iþlemler için)
            {
                MySqlCommand Komut = new MySqlCommand(sql, Baglanti);

                if (Baglanti.State == ConnectionState.Closed)
                {
                    Baglanti.Open();
                }

                try
                {
                    Komut.ExecuteNonQuery();
                }
                finally
                {
                    Baglanti.Close();
                    Baglanti.Dispose();
                }
            }

            public static string ExecuteScalar(string sql)                      //Dönen ilk deðeri verir.
            {
                string sonuc = string.Empty;

                MySqlCommand Komut = new MySqlCommand(sql, Baglanti);

                if (Baglanti.State == ConnectionState.Closed)
                {
                    Baglanti.Open();
                }

                try
                {
                    sonuc = Komut.ExecuteScalar().ToString();
                }
                finally
                {
                    Baglanti.Close();
                    Baglanti.Dispose();
                }
                return sonuc;
            }

            public static string ExecuteScalar_BosDegerDondur(string sql)       //Dönen ilk deðer null gelirse boþ deðer döndürür.
            {
                string sonuc = string.Empty;

                MySqlCommand Komut = new MySqlCommand(sql, Baglanti);

                if (Baglanti.State == ConnectionState.Closed)
                {
                    Baglanti.Open();
                }

                try
                {
                    sonuc = Komut.ExecuteScalar().ToString();
                }
                catch
                {
                    sonuc = string.Empty;
                }
                finally
                {
                    Baglanti.Close();
                    Baglanti.Dispose();
                }
                return sonuc;
            }

        }

    }
}