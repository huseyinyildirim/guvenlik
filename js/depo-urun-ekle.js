$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/autocomplete.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$('#urun,#kod').bind('keyup', function() {BuyukHarf($(this))})
$('#adet').bind('keyup', function() { SayisalKarakter($(this)) })

$("#Anthem_urun_id__,#urun_id").css("display","none");
$("#Anthem_urun_id__,#urun_id").css("visibility","hidden");

$("#urun").autocomplete('ajax/urunler.aspx').result(function(event, item){$("#urun_getir").val(item.toString().split("|")[1])})

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

if($("#durum").val() == "9")
{
alert("Lütfen ürününün durumunu belirtiniz.");
YakSondur($("#durum"));
return(false)
}

return (confirm('İşlemi onaylıyor musunuz?'))

}
)

}
)