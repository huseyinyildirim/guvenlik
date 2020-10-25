$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/masked-input.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$('#adi,#vergi_dairesi,#adres').bind('keyup', function() {BuyukHarf($(this))});
$('#mail').bind('keyup', function() {KucukHarf($(this))});
jQuery(function ($) { $('#tel,#faks').mask('999 999 99 99') });
$('#vergi_no,#ticaret_sicil_no').bind('keyup', function() { SayisalKarakter($(this)) })

$("#gonder").click
(
function()
{

if($("#bolge_id").val() == "0")
{
alert("Lütfen bölgeyi belirtiniz");
YakSondur($("#bolge_id"));
return(false)
}

if($("#adi").val() == "")
{
alert("Lütfen firma adını belirtiniz.");
YakSondur($("#adi"));
return(false)
}

if($("#tel").val() == "")
{
alert("Lütfen bir telefon numarası belirtiniz.");
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