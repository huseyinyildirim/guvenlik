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

public static class Class                                           //Class ba�l�yor.
{

    public class Degiskenler                                            //De�i�kenler
    {
        public static string SiteAdi = "karunsosyalhizmetler.com";                    //Session ve cookie de kullan�l�yor. Bir de MD5 'de secret key!
        public static string FirmaAdi = "Karun Sosyal Hizmetler";           //Title vs. gibi yerlerde kullan�l�yor.

        public static int SayfalamaSayisi = 100;                            //Grid sayfa say�s�.
        public static int OturumVarmi = 0;

        public static string vbCrLf = "\r\n";                               //Sat�r atlatmada kullan�l�yor.
        public static string TamAdres = String.Empty;                       //Tam adresi veriyor.

        public static string AdminKullaniciAdi = String.Empty;              //Kullanici adini veriyor.
        public static string AdminSifre = String.Empty;                     //�ifre veriyor.
        public static string AdminAdSoyad = String.Empty;                   //Admin ad� ve soyad�n� veriyor.
        public static string AdminID = String.Empty;                        //Admin ID sini veriyor.
        public static string AdminTip = String.Empty;                       //0 Genel Y�netici (Root), 1 B�lge Y�neticisi, 2 Firma Y�neticisi, 3 Genel Depo Y�neticisi, 4 Firma Depo Y�neticisi
        public static string AdminBolgeID = String.Empty;
        public static string AdminFirmaID = String.Empty;

        //MYSQL Bilgileri
        public static string MysqlSunucu = "localhost"; //89.19.19.139
        public static string MysqlPort = "3306";
        public static string MysqlVeritabani = "guvenlik";
        public static string MysqlKullanici = "root";
        public static string MysqlSifre = "50567232";
        //MYSQL Bilgileri

        //Haz�rGridler ��in
        public static int SayfaSayisi = 10;
        public static int SayfadaGorunecekRakamSayisi = 10;
        public static string SonSayfaOku = "�";
        public static string IlkSayfaOku = "�";
        //Haz�rGridler ��in

        /*
        public static string LogAdi = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "-").Replace(".", "-");
        public static string LogYolu = "logs\\";  
        public static string LogKayit = "OFF"; //OFF olursa kapal� ON olursa A��k
        public static string KayitYok = "<img src=\"images/ikon/yok.png\" class=\"ikon\" alt=\"\"/> <span style=\"vertical-align:middle\">Kay�t bulunamad�.</span>";
        public static string ViewStateSikistir = "ON"; //OFF olursa kapal� ON olursa A��k
        */
    }

    public class Fonksiyonlar                                           //T�m fonksiyonlar
    {

        public static string TelFormatla(string x)                          //Telefon numaras� formatl�yor.
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

        public static string SayiFormatla(string s)                         //Say� formatl�yor.
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

        public static bool NumericKontrol(string word)                      //Gelen de�er numerik mi de�il mi boolean olarak kontrol ediyor.
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

        public static string MD5Sifrele(string str)                         //Kullan�c�n�n girdi�i �ifreyi secret key kullanarak MD5 yap�yor.
        {
            string password1 = FormsAuthentication.HashPasswordForStoringInConfigFile(str + "-" + Degiskenler.SiteAdi, "sha1");
            string password2 = FormsAuthentication.HashPasswordForStoringInConfigFile(password1, "md5");
            string password3 = FormsAuthentication.HashPasswordForStoringInConfigFile(password2, "md5");
            return password3;
        }


        public class CiktiVer                                             //Export fonksiyonlar� burada.
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

        public class JavaScript                                             //JavaScript fonksiyonlar� burada.
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
                public static string Yonlendir(string str1, string str2)            //Facebox t�klay�nca y�nlendiriyor.
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

                public static string GeriGonder(string str)                         //Facebox t�klay�nca geri g�nderiyor.
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

            public static string MesajKutusuVeYonlendir(string dgr1, string dgr2) //JS Mesaj kutusu vererek y�nlendiriyor
            {
                return ("alert(\"" + dgr1.Replace("[ln]", @"\n") + "\");\n" + "location.href=\"" + dgr2 + "\"\n");
            }

            public static void MesajKutusuVeYonlendirNormal(string dgr1, string dgr2) //JS Mesaj kutusu vererek y�nlendiriyor
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

            public static string GeriGonder()                                   //JS geri g�nder.
            {
            return ("window.history.back()\n");
            }

            public static void GeriGonderNormal()                               //JS geri g�nder.
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

            public static string Yonlendir(string str)                          //JS y�nlendir.
            {
                return ("location.href=\"" + str + "\"\n");
            }

            public static void YonlendirNormal(string str)                      //JS y�nlendir.
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

        public static void TestMesajKutusu(string d)                        //WinForm tabanl� mesaj kutusu.
        {
            System.Windows.Forms.MessageBox.Show(d, "Test Mesaj�", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            return;
        }

        public static string SQLTemizle(string str)                         //SQL Injection i�in temizle fonksiyonu.
        {
            try
            {
                if (str != string.Empty)
                {
                    str = str.Replace("'", "");
                    str = str.Replace("`", "");
                    str = str.Replace("�", "");

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

        public static string UrlvePathYaz()                                 //Tam URL yi yaz�yor.
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

        public static string MevcutSayfa()                                  //Bulunulan sayfan�n tam yolunu yaz�yor.
        {
            return (HttpContext.Current.Request.Url.AbsoluteUri);
        }

        public class SiteFonksiyonlari                                      //Site fonksiyonlar�.
        {

            public static string AdminSQL()
            {
                string SQLSonuc = string.Empty;
                //0 Genel Y�netici (Root), 1 B�lge Y�neticisi, 2 Firma Y�neticisi, 3 Genel Depo Y�neticisi, 4 Firma Depo Y�neticisi

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

            public static void SessionKontrol()                                 //Session varm� ona bak�yor.
            {
                if (HttpContext.Current.Session["kullanici_adi"] == null || HttpContext.Current.Session["sifre"] == null)
                {
                    JavaScript.MesajKutusuVeYonlendirNormal("Oturumunuz do�rulanamad� veya oturum zaman a��m�na u�rad�.[ln]L�tfen yeniden giri� yap�n�z.", "default.aspx");
                    Degiskenler.OturumVarmi = 0;
                }
                else
                {
                    Degiskenler.OturumVarmi = 1;
                }
            }

            public static void SessionVarmi()                                   //Session varm� ona bak�yor. Yoksa anasayfaya y�nlendiriyor.
            {
                if (HttpContext.Current.Session["kullanici_adi"] != null || HttpContext.Current.Session["sifre"] != null)
                {
                    JavaScript.YonlendirNormal("panel.aspx");
                }
            }

            public static string PersonelAdSoyadVer(string str)                     //ID si g�nderilen personelin ad�n� soyad�n� veriyor.
            {
                string SQL = "SELECT adi_soyadi FROM personel USE INDEX (id) WHERE id ='" + str + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminIDVer(string str1, string str2)           //Adminin ID sini veriyor.
            {
                string SQL = "SELECT id FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminAdSoyadVer(string str1, string str2)         //Kullan�c� ad� g�nderilen adminin ad�n� soyad�n� veriyor.
            {
                string SQL = "SELECT adi_soyadi FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminTipVer(string str1, string str2)              //ID si g�nderilen adminin tipini veriyor.
            {
                string SQL = "SELECT tip FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminBolgeIDVer(string str1, string str2)              //ID si g�nderilen adminin b�lge ID sini veriyor.
            {
                string SQL = "SELECT bolge_id FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminFirmaIDVer(string str1, string str2)              //ID si g�nderilen adminin firma ID sini veriyor.
            {
                string SQL = "SELECT firma_id FROM admin WHERE kullanici_adi ='" + str1 + "' and sifre ='" + str2 + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminAdSoyadVerID(string str)                     //ID si g�nderilen adminin ad�n� soyad�n� veriyor.
            {
                string SQL = "SELECT adi_soyadi FROM admin USE INDEX (id) WHERE id ='" + str + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string AdminAdSoyadVeTipVerID(string str)                //ID si g�nderilen adminin ad�n� soyad�n� ve tipini veriyor.
            {
                string SQL = "SELECT adi_soyadi,tip FROM admin USE INDEX (id) WHERE id ='" + str + "'";
                DataSet ds = MySQL.DataSetGetir(SQL, "admin");

                string tip = string.Empty;

                switch (ds.Tables[0].Rows[0]["tip"].ToString())
                {
                    case "0":
                        tip = "Genel Y�netici (Root)";
                        break;

                    case "1":
                        tip = "B�lge Y�neticisi";
                        break;

                    case "2":
                        tip = "Firma Y�neticisi";
                        break;

                    case "3":
                        tip = "Genel Depo Y�neticisi";
                        break;

                    case "4":
                        tip = "Firma Depo Y�neticisi";
                        break;
                }

                return (ds.Tables[0].Rows[0]["adi_soyadi"].ToString() + " <i>(" + tip + ")</i>");
            }

            public static string AdminKullaniciAdiKontrol(string str)           //Yeni giri�lerde veya g�ncellemelerde kullan�c� ad�n� kontrol ediyor.
            {
                string SQL = "SELECT COUNT(id) FROM admin WHERE kullanici_adi ='" + str + "'";
                return (MySQL.ExecuteScalar(SQL));
            }

            public static string ilAdiVer(string str)                            //ID si g�nderilen ilin ad�n� getiriyor.
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

            public static string ilceAdiVer(string str)                          //ID si g�nderilen il�enin ad�n� getiriyor.
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

            public static string BolgeAdiVer(string str)                            //ID si g�nderilen bolgenin ad�n� getiriyor.
            {
                string SQL = "SELECT adi FROM bolge USE INDEX (id) WHERE id='" + str + "'";
                return (MySQL.ExecuteScalar_BosDegerDondur(SQL));
            }

            public static string FirmaAdiVer(string str)                            //ID si g�nderilen firman�n ad�n� getiriyor.
            {
                string SQL = "SELECT adi FROM firma USE INDEX (id) WHERE id='" + str + "'";
                return (MySQL.ExecuteScalar_BosDegerDondur(SQL));
            }

            public static string UrunAdiVer(string str)                            //ID si g�nderilen �r�n�n ad�n� getiriyor.
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

            public static string UrunKoduVer(string str)                            //ID si g�nderilen �r�n�n kodunu getiriyor.
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

            public static string ProjeAdiVer(string str)                            //ID si g�nderilen projenin ad�n� getiriyor.
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

            public static void BaslikYaz(Panel pnl, string rsm, string bslk)          //Ba�l��� yaz�yor. Hem de User Control den �a��r�yor.
            {
                UserControl BaslikEkle = (UserControl)((Page)HttpContext.Current.Handler).LoadControl("~/ascx/baslik.ascx");
                pnl.Controls.Add(BaslikEkle);

                System.Web.UI.WebControls.Image ikon = (System.Web.UI.WebControls.Image)BaslikEkle.FindControl("ikon") as System.Web.UI.WebControls.Image;
                ikon.ImageUrl = "~/images/ikon/" + rsm;

                Label baslik = (Label)BaslikEkle.FindControl("baslik") as Label;
                baslik.Text = bslk;
            }

            public static void MesajYaz(Panel pnl, string bslk, int durum)          //Mesaj yaz�yor. Hem de User Control den �a��r�yor.
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

        public class MySQL                                                  //MySQL fonksiyonlar�.
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

            public static void ExecuteNonQuery(string sql)                      //ExecuteNonQuery (DELETE,UPDATE vb. i�lemler i�in)
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

            public static string ExecuteScalar(string sql)                      //D�nen ilk de�eri verir.
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

            public static string ExecuteScalar_BosDegerDondur(string sql)       //D�nen ilk de�er null gelirse bo� de�er d�nd�r�r.
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