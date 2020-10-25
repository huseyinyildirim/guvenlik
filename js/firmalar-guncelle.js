$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/masked-input.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$("#bolge_id").attr("disabled", "disabled")
$("#il_kodu,#ilce_kodu").attr("disabled", "disabled")

$('#adi,#vergi_dairesi,#adres').bind('keyup', function() {BuyukHarf($(this))});
$('#mail').bind('keyup', function() {KucukHarf($(this))});
jQuery(function ($) { $('#tel,#faks').mask('999 999 99 99') });
$('#vergi_no,#ticaret_sicil_no').bind('keyup', function() { SayisalKarakter($(this)) })

$("#checkbox1").click
(
function()
{
if (typeof $("input[name=checkbox1]:checked").val() != 'undefined') 
{
$("#bolge_id").removeAttr("disabled")
}
else
{
$("#bolge_id").attr("disabled", "disabled");
$("#bolge_id").attr("selectedIndex",0)
}
}
)

$("#checkbox2").click
(
function()
{
if (typeof $("input[name=checkbox2]:checked").val() != 'undefined') 
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
alert("Lütfen bölgeyi belirtiniz");
YakSondur($("#bolge_id"));
return(false)
}
if($("#bolge_il_kodu").val() == "0")
{
alert("Lütfen bölge şehrini belirtiniz.");
YakSondur($("#bolge_il_kodu"));
return(false)
}
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

if (typeof $("input[name=checkbox2]:checked").val() != 'undefined') 
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