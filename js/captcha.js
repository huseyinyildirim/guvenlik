function CaptchaYukle()
{
trh=new Date();
$("#captcha_resim").attr("src", "inc/captcha.aspx?tarih=" + trh.getTime())
}