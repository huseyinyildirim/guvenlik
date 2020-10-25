$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/autocomplete.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$('#urun').bind('keyup', function() {BuyukHarf($(this))})

$('#adet').bind('keyup',function()
{
SayisalKarakter($(this));
	 
if($("#urun_id").val() != "0")
{
if(parseInt($('#adet').val()) > parseInt($('#kullanilabilir_adet').val()))
{
alert("Sevk edilecek adet kullanılabilir adetten büyük olamaz.\nLütfen sevk edeceğiniz adeti kontrol ediniz.");
return(false)
}

if(parseInt($('#adet').val()) < 1)
{
alert("Sevk edilecek adet 0 dan küçük olamaz.\nLütfen sevk edeceğiniz adeti kontrol ediniz.");
return(false)
}
}

}
)

$("#Anthem_urun_id__,#urun_id").css("display","none");
$("#Anthem_urun_id__,#urun_id").css("visibility","hidden");

$("#urun").autocomplete('ajax/depo-urunler.aspx').result(function(event, item){$("#urun_getir").val(item.toString().split("|")[1])})

$("#sec").click
(
function()
{
if($("#urun").val() == "")
{
alert("Lütfen ürün adını belirtiniz.");
YakSondur($("#urun"));
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

if($("#urun").val() == "" && $("#urun_id").val() == "0")
{
alert("Lütfen ürün adını belirtiniz.");
YakSondur($("#urun"));
return(false)
}

if($("#urun_id").val() == "0")
{
alert("Lütfen ürün seçimini yapınız.");
YakSondur($("#sec"));
return(false)
}

if(parseInt($('#adet').val()) > parseInt($('#kullanilabilir_adet').val()))
{
alert("Sevk edilecek adet kullanılabilir adetten büyük olamaz.\nLütfen sevk edeceğiniz adeti kontrol ediniz.");
YakSondur($("#adet"));
return(false)
}

if(parseInt($('#adet').val()) < 1)
{
alert("Sevk edilecek adet 0 dan küçük olamaz.\nLütfen sevk edeceğiniz adeti kontrol ediniz.");
YakSondur($("#adet"));
return(false)
}

return (confirm('İşlemi onaylıyor musunuz?'))

}
)

}
)