using System;

public partial class panel: System.Web.UI.Page
{

protected void Page_Load(object sender, EventArgs e)
{
Class.Fonksiyonlar.SiteFonksiyonlari.SessionKontrol();

Class.Fonksiyonlar.SiteFonksiyonlari.BaslikYaz(baslik_getir, "home.png", "Anasayfa");
}

// VIEWSTATE Sıkıştırmaca //
// Boş VIEWSTATE //
protected override void SavePageStateToPersistenceMedium(object viewState)
{
}

// Boş VIEWSTATE //
protected override object LoadPageStateFromPersistenceMedium()
{
return null;
}

}