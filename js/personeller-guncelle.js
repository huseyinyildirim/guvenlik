$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/masked-input.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$("#bolge_id").attr("disabled", "disabled");
$("#il_kodu,#ilce_kodu").attr("disabled", "disabled")

$('#adi_soyadi,#dogum_yeri,#ana_adi,#baba_adi,#ehliyet,#adres').bind('keyup', function() {BuyukHarf($(this))})
$('#mail').bind('keyup', function() {KucukHarf($(this))})
jQuery(function ($) { $('#tel').mask('999 999 99 99') });
$('#tc_kimlik,#ssk_no').bind('keyup', function() { SayisalKarakter($(this)) })

$("#checkbox1").click
(
function()
{
if (typeof $("input[name=checkbox1]:checked").val() != 'undefined') 
{
$("#bolge_id,#firma_id").removeAttr("disabled")
}
else
{
$("#bolge_id,#firma_id").attr("disabled", "disabled");
$("#bolge_id,#firma_id").attr("selectedIndex",0)
}
}
)

$("#checkbox2").click
(
function()
{
if (typeof $("input[name=checkbox2]:checked").val() != 'undefined') 
{
$("#cinsiyet").removeAttr("disabled")
}
else
{
$("#cinsiyet").attr("disabled", "disabled");
$("#cinsiyet").attr("selectedIndex",0)
}
}
)

$("#checkbox3").click
(
function()
{
if (typeof $("input[name=checkbox3]:checked").val() != 'undefined') 
{
$("#dogum_tarihi_gun,#dogum_tarihi_ay,#dogum_tarihi_yil").removeAttr("disabled")
}
else
{
$("#dogum_tarihi_gun,#dogum_tarihi_ay,#dogum_tarihi_yil").attr("disabled", "disabled");
$("#dogum_tarihi_gun,#dogum_tarihi_ay,#dogum_tarihi_yil").attr("selectedIndex",0)
}
}
)

$("#checkbox4").click
(
function()
{
if (typeof $("input[name=checkbox4]:checked").val() != 'undefined') 
{
$("#egitim_durumu").removeAttr("disabled")
}
else
{
$("#egitim_durumu").attr("disabled", "disabled");
$("#egitim_durumu").attr("selectedIndex",0)
}
}
)

$("#checkbox5").click
(
function()
{
if (typeof $("input[name=checkbox5]:checked").val() != 'undefined') 
{
$("#sertifika_durumu").removeAttr("disabled")
}
else
{
$("#sertifika_durumu").attr("disabled", "disabled");
$("#sertifika_durumu").attr("selectedIndex",0)
}
}
)

$("#checkbox6").click
(
function()
{
if (typeof $("input[name=checkbox6]:checked").val() != 'undefined') 
{
$("#il_kodu").removeAttr("disabled")
}
else
{
$("#il_kodu,#ilce_kodu").attr("disabled", "disabled");
$("#il_kodu,#ilce_kodu").attr("selectedIndex",0)
}
}
)

$("#gonder").click
(
function()
{

if (typeof $("input[name=checkbox1]:checked").val() != 'undefined')
{
if($("#bolge_id").val() == "0")
{
alert("Lütfen bölge seçiniz.");
YakSondur($("#bolge_id"));
return(false)
}
}

if (typeof $("input[name=checkbox1]:checked").val() != 'undefined')
{
if($("#firma_id").val() == "0")
{
alert("Lütfen firmayı seçiniz.");
YakSondur($("#firma_id"));
return(false)
}
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

if (typeof $("input[name=checkbox2]:checked").val() != 'undefined')
{
if($("#cinsiyet").val() == "9")
{
alert("Lütfen cinsiyeti belirtiniz");
YakSondur($("#cinsiyet"));
return(false)
}
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

if (typeof $("input[name=checkbox5]:checked").val() != 'undefined')
{
if($("#sertifika_durumu").val() == "9")
{
alert("Lütfen sertifika durumunu belirtiniz");
YakSondur($("#sertifika_durumu"));
return(false)
}
}

if (typeof $("input[name=checkbox6]:checked").val() != 'undefined') 
{
if($("#il_kodu").val() == "0")
{
alert("Lütfen ili belirtiniz");
YakSondur($("#il_kodu"));
return(false)
}
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