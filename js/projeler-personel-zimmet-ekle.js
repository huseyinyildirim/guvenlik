$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/autocomplete.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$('#adet').bind('keyup',function(){SayisalKarakter($(this))})

$("#bolge_id_js,#firma_id_js").css("display","none");
$("#bolge_id_js,#firma_id_js").css("visibility","hidden");
$("#tum_personel").css("float","right");
$("#tum_personel").css("margin-right","6px");

$("#proje_personelleri").attr("disabled", "disabled");

$("#tum_personel").click
(
function()
{
if (typeof $("input[name=tum_personel]:checked").val() != 'undefined') 
{
$("#proje_personelleri").attr("disabled", "disabled");
$("#proje_personelleri").attr("selectedIndex",0)
}
else
{
$("#proje_personelleri").removeAttr("disabled")
}
}
)


$("#urun").autocomplete('ajax/projeler-depo-urunler.aspx?bolge_id='+$("#bolge_id_js").text()+'&firma_id='+$("#firma_id_js").text()).result(function(event, item){$("#urun_getir").val(item.toString().split("|")[1])})

$(".sil").click(function(){return(confirm('İlgili kaydı silmek üzeresiniz.\nİşlemi onaylıyor musunuz?'))})

$("#sec").click
(
function()
{

if($("#urun").val() == "")
{
alert("Lütfen bir ürün seçiniz.");
YakSondur($("#urun"));
return(false)
}

if($("#adet").val() == "")
{
alert("Lütfen ürün adetini belirtiniz.");
YakSondur($("#adet"));
return(false)
}

if (typeof $("input[name=tum_personel]:checked").val() == 'undefined')
{
if($("#proje_personelleri").val() == "0")
{
alert("Lütfen personel belirtiniz.");
YakSondur($("#proje_personelleri"));
return(false)
}
}

}
)

$("#gonder").click
(
function()
{

if($("#tr_count").val() != "0")
{
return (confirm('İşlemi onaylıyor musunuz?'))
}

}
)

}
)