$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$("#bolge_id").attr("disabled", "disabled")

Sorgula('#kullanici_adi','#kullanici_adi_kontrol','1');

$('#adi_soyadi').bind('keyup', function() {BuyukHarf($(this))})
$('#kullanici_adi').bind('keyup', function() {KucukHarf($(this));Sorgula('#kullanici_adi','#kullanici_adi_kontrol','1')})

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
$("#sifre").removeAttr("disabled")
}
else
{
$("#sifre").attr("disabled", "disabled");
$("#sifre").val("")
}
}
)

$("#checkbox3").click
(
function()
{
if (typeof $("input[name=checkbox3]:checked").val() != 'undefined') 
{
$("#tip").removeAttr("disabled")
}
else
{
$("#tip").attr("disabled", "disabled");
$("#tip").attr("selectedIndex",0)
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
alert("Lütfen bölgeyi belirtiniz.");
YakSondur($("#bolge_id"));
return(false)
}
}

if (typeof $("input[name=checkbox1]:checked").val() != 'undefined') 
{
if($("#firma_id").val() == "0")
{
alert("Lütfen firmayı belirtiniz.");
YakSondur($("#firma_id"));
return(false)
}
}

if($("#adi_soyadi").val() == "")
{
alert("Lütfen isim soy isim belirtiniz.");
YakSondur($("#adi_soyadi"));
return(false)
}

if($("#kullanici_adi").val() == "")
{
alert("Lütfen kullanıcı adını belirtiniz.");
YakSondur($("#kullanici_adi"));
return(false)
}

if (typeof $("input[name=checkbox2]:checked").val() != 'undefined') 
{
if($("#sifre").val() == "")
{
alert("Lütfen şifreyi belirtiniz.");
YakSondur($("#sifre"));
return(false)
}
}

if (typeof $("input[name=checkbox3]:checked").val() != 'undefined') 
{
if($("#tip").val() == "9")
{
alert("Lütfen yönetici tipini belirtiniz.");
YakSondur($("#tip"));
return(false)
}
}

}
)

}
)