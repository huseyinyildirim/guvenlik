$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/autocomplete.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/masked-input.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$("#bolge_id").attr("disabled", "disabled");

$('#adi,#sorumlu,#adres').bind('keyup', function() {BuyukHarf($(this))})
jQuery(function ($) { $('#tel').mask('999 999 99 99') });

$("#Anthem_urun_id__,#urun_id").css("display","none");
$("#Anthem_urun_id__,#urun_id").css("visibility","hidden");

$("#personel").autocomplete('ajax/personel.aspx').result(function(event, item){$("#personel_getir").val(item.toString().split("|")[1])})

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
$("#personel,#personel_getir").removeAttr("disabled")
}
else
{
$("#personel,#personel_getir").val("");
}
}
)

$("#checkbox3").click
(
function()
{
if (typeof $("input[name=checkbox3]:checked").val() != 'undefined') 
{
$("#gun1,#ay1,#yil1").removeAttr("disabled")
}
else
{
$("#gun1,#ay1,#yil1").attr("disabled", "disabled");
$("#gun1,#ay1,#yil1").attr("selectedIndex",0)
}
}
)
	
$("#checkbox4").click
(
function()
{
if (typeof $("input[name=checkbox4]:checked").val() != 'undefined') 
{
$("#gun2,#ay2,#yil2").removeAttr("disabled")
}
else
{
$("#gun2,#ay2,#yil2").attr("disabled", "disabled");
$("#gun2,#ay2,#yil2").attr("selectedIndex",0)
}
}
)

$("#sec").click
(
function()
{
if($("#personel").val() == "")
{
alert("Lütfen Proje Yöneticisini belirtiniz.");
YakSondur($("#personel"));
return(false)
}
else
{
$("#goster").css("visibility","visible");
$("#goster").css("display","block")
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

if (typeof $("input[name=checkbox2]:checked").val() != 'undefined')
{
if($("#personel").val() == "" && $("#personel_id").val() == "0")
{
alert("Lütfen Proje Yöneticisini belirtiniz.");
YakSondur($("#personel"));
return(false)
}
}

if (typeof $("input[name=checkbox2]:checked").val() != 'undefined')
{
if($("#personel_id").val() == "0")
{
alert("Lütfen Proje Yöneticisini seçiniz.");
YakSondur($("#sec"));
return(false)
}
}

if (typeof $("input[name=checkbox2]:checked").val() != 'undefined')
{
if($("#personel_id").val() == "0")
{
alert("Lütfen Proje Yöneticisini seçiniz.");
YakSondur($("#sec"));
return(false)
}
}

if($("#adi").val() == "")
{
alert("Lütfen proje adını belirtiniz.");
YakSondur($("#adi"));
return(false)
}

if($("#sorumlu").val() == "")
{
alert("Lütfen sorumlu kişiyi belirtiniz.");
YakSondur($("#sorumlu"));
return(false)
}

if($("#tel").val() == "")
{
alert("Lütfen telefonu belirtiniz.");
YakSondur($("#tel"));
return(false)
}

}
)

}
)