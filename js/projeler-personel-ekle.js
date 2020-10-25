$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$("#bolge_id_js,#firma_id_js").css("display","none");
$("#bolge_id_js,#firma_id_js").css("visibility","hidden");

$(".sil").click(function(){return(confirm('İlgili personeli projeden çıkarmak üzeresiniz.\nİşlemi onaylıyor musunuz?'))})

$("input[name=tablo1$ctl01$chck_sec]").click
(
function()
{
if (typeof $("input[name=tablo1$ctl01$chck_sec]:checked").val() != 'undefined') 
{
TumunuSecInput('#orta #orta_ic #sag_dis #sag #ic .beyaz tbody tr td .chck_span > input','0')
}
else
{
TumunuSecInput('#orta #orta_ic #sag_dis #sag #ic .beyaz tbody tr td .chck_span > input','1')
}
}
)

$("#gonder").click
(
function()
{
if (typeof $("#orta #orta_ic #sag_dis #sag #ic .beyaz tbody tr td .chck_span > input:checked").val() != 'undefined')
{
return (confirm('İşlemi onaylıyor musunuz?'))
}
}
)

}
)