using System;

public partial class _default : System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionVarmi();
}

protected void submit_Click(object sender, EventArgs e)
{
if (Session["captcha"] != null)
{
if (Request.Form["captcha"] == Session["captcha"].ToString())
{
KullaniciAdiKontrol();
}
else
{
ClientScript.RegisterClientScriptBlock(this.GetType(), Class.Degiskenler.SiteAdi, Class.Fonksiyonlar.JavaScript.FaceBox.GeriGonder("Güvenlik kodu doğrulanamadı.<br/>Lütfen güvenlik kodunu kontrol ederek yeniden yazınız."), true);
return;
}
}
else
{
ClientScript.RegisterClientScriptBlock(this.GetType(), Class.Degiskenler.SiteAdi, Class.Fonksiyonlar.JavaScript.FaceBox.GeriGonder("Güvenlik kodu doğrulanamadı.<br/>Lütfen güvenlik kodunu kontrol ederek yeniden yazınız."), true);
return;
}
}

protected void KullaniciAdiKontrol()
{
string SQL = "SELECT kullanici_adi FROM admin USE INDEX (onay) WHERE onay='1' and kullanici_adi='" + Class.Fonksiyonlar.SQLTemizle(Request.Form["kullanici_adi"]) + "'";

if (Class.Fonksiyonlar.MySQL.ExecuteScalar_BosDegerDondur(SQL) == "")
{
ClientScript.RegisterClientScriptBlock(this.GetType(), Class.Degiskenler.SiteAdi, Class.Fonksiyonlar.JavaScript.FaceBox.GeriGonder("Kullanıcı adı bulunamadı.<br/>Lütfen kullanıcı adınızı kontrol ederek yeniden yazınız."), true);
}
else
{
SifreKontrol();
}

}

protected void SifreKontrol()
{
string SQL = "SELECT sifre FROM admin USE INDEX (onay) WHERE onay='1' and kullanici_adi='" + Class.Fonksiyonlar.SQLTemizle(Request.Form["kullanici_adi"]) + "' and sifre ='" + Class.Fonksiyonlar.MD5Sifrele(Class.Fonksiyonlar.SQLTemizle(Request.Form["sifre"])) + "'";

if (Class.Fonksiyonlar.MySQL.ExecuteScalar_BosDegerDondur(SQL) == "")
{
ClientScript.RegisterClientScriptBlock(this.GetType(), Class.Degiskenler.SiteAdi, Class.Fonksiyonlar.JavaScript.FaceBox.GeriGonder("Şifreniz yanlıştır.<br/>Lütfen şifrenizi kontrol ederek yeniden yazınız."), true);
}
else
{
Session.Add("kullanici_adi", Class.Fonksiyonlar.SQLTemizle(Request.Form["kullanici_adi"]));
Session.Add("sifre", Class.Fonksiyonlar.SQLTemizle(Request.Form["sifre"]));

Class.Degiskenler.AdminKullaniciAdi = Session["kullanici_adi"].ToString();
Class.Degiskenler.AdminSifre = Class.Fonksiyonlar.MD5Sifrele(Class.Fonksiyonlar.SQLTemizle(Request.Form["sifre"]));
Class.Degiskenler.AdminAdSoyad = Class.Fonksiyonlar.SiteFonksiyonlari.AdminAdSoyadVer(Class.Degiskenler.AdminKullaniciAdi,Class.Degiskenler.AdminSifre);
Class.Degiskenler.AdminID = Class.Fonksiyonlar.SiteFonksiyonlari.AdminIDVer(Class.Degiskenler.AdminKullaniciAdi, Class.Degiskenler.AdminSifre);
Class.Degiskenler.AdminTip = Class.Fonksiyonlar.SiteFonksiyonlari.AdminTipVer(Class.Degiskenler.AdminKullaniciAdi, Class.Degiskenler.AdminSifre);
Class.Degiskenler.AdminBolgeID = Class.Fonksiyonlar.SiteFonksiyonlari.AdminBolgeIDVer(Class.Degiskenler.AdminKullaniciAdi, Class.Degiskenler.AdminSifre);
Class.Degiskenler.AdminFirmaID = Class.Fonksiyonlar.SiteFonksiyonlari.AdminFirmaIDVer(Class.Degiskenler.AdminKullaniciAdi, Class.Degiskenler.AdminSifre);

Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Class.Degiskenler.SiteAdi, Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla giriş yapılmıştır.[ln][ln]Hoşgeldin Sn. " + Class.Degiskenler.AdminAdSoyad + "[ln]Şimdi yönetici sayfasına yönlendiriliyorsunuz.", "panel.aspx"), true);
}

}

}