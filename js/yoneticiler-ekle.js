$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

Sorgula('#kullanici_adi','#kullanici_adi_kontrol','0');

$('#adi_soyadi').bind('keyup',function(){$('#kullanici_adi').val($('#adi_soyadi').val().replace(" ", ".").toLowerCase());BuyukHarf($(this));Sorgula('#kullanici_adi','#kullanici_adi_kontrol','0')})
$('#kullanici_adi').bind('keyup', function() {KucukHarf($(this));Sorgula('#kullanici_adi','#kullanici_adi_kontrol','0')})

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


if($("#sifre").val() == "")
{
alert("Lütfen şifreyi belirtiniz.");
YakSondur($("#sifre"));
return(false)
}

if($("#tip").val() == "9")
{
alert("Lütfen yönetici tipini belirtiniz.");
YakSondur($("#tip"));
return(false)
}

}
)

}
)