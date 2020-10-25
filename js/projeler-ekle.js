$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/autocomplete.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/masked-input.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$('#adi,#sorumlu,#adres').bind('keyup', function() {BuyukHarf($(this))})
jQuery(function ($) {$('#tel').mask('999 999 99 99')});

$("#Anthem_urun_id__,#urun_id").css("display","none");
$("#Anthem_urun_id__,#urun_id").css("visibility","hidden");

$("#personel").autocomplete('ajax/personel.aspx').result(function(event, item){$("#personel_getir").val(item.toString().split("|")[1])})

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

if($("#personel").val() == "" && $("#personel_id").val() == "0")
{
alert("Lütfen Proje Yöneticisini belirtiniz.");
YakSondur($("#personel"));
return(false)
}

if($("#personel_id").val() == "0")
{
alert("Lütfen Proje Yöneticisini seçiniz.");
YakSondur($("#sec"));
return(false)
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