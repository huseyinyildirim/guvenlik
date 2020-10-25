$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/masked-input.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$('#adi_soyadi,#dogum_yeri,#ana_adi,#baba_adi,#ehliyet,#adres').bind('keyup', function() {BuyukHarf($(this))})
$('#mail').bind('keyup', function() {KucukHarf($(this))})
jQuery(function ($) { $('#tel').mask('999 999 99 99') });
$('#tc_kimlik,#ssk_no').bind('keyup', function() { SayisalKarakter($(this)) })

$("#gonder").click
(
function()
{

if($("#bolge_id").val() == "0")
{
alert("Lütfen bölge seçiniz.");
YakSondur($("#bolge_id"));
return(false)
}

if($("#firma_id").val() == "0")
{
alert("Lütfen firmayı seçiniz.");
YakSondur($("#firma_id"));
return(false)
}

if($("#tc_kimlik").val() == "")
{
alert("Lütfen T.C. Kimlik No belirtiniz.");
YakSondur($("#tc_kimlik"));
return(false)
}

if($("#tc_kimlik").val().length<11)
{
alert("T.C. Kimlik NO 11 karakterden küçük olamaz.");
YakSondur($("#tc_kimlik"));
return(false)
}

if($("#adi_soyadi").val() == "")
{
alert("Lütfen isim soy isim belirtiniz.");
YakSondur($("#adi_soyadi"));
return(false)
}

if($("#cinsiyet").val() == "9")
{
alert("Lütfen cinsiyeti belirtiniz");
YakSondur($("#cinsiyet"));
return(false)
}

if($("#tel").val() == "")
{
alert("Lütfen telefonu belirtiniz.");
YakSondur($("#tel"));
return(false)
}

if (!$("#mail").val() == "") 
{
if (!MailKontrol($("#mail").val())) 
{
alert("Lütfen geçerli bir mail adresi yazınız.");
YakSondur($("#mail"));
return(false)
}
}

if($("#sertifika_durumu").val() == "9")
{
alert("Lütfen sertifika durumunu belirtiniz");
YakSondur($("#sertifika_durumu"));
return(false)
}

if($("#il_kodu").val() != "0")
{
if($("#ilce_kodu").val() == "0")
{
alert("Lütfen ilçeyi belirtiniz.");
YakSondur($("#ilce_kodu"));
return(false)
}
}

}
)

}
)