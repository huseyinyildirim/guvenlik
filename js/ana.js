$(document).ready
(
function()
{
if ($("#orta #orta_ic").height() < 510)
{
$("<script type=\"text/javascript\" src=\"js/footer.js\"><\/script\>").appendTo(document.body);
$("#alt_ic").positionFooter(true)
//alert($('#alt_ic').offset().top)
//alert($("#orta #orta_ic").height())
}
else
{
$("<script type=\"text/javascript\" src=\"js/scrollto.js\"><\/script\>").appendTo(document.body);
$("#orta #orta_ic #sag_dis").scrollto(1000)
}
if (!$.browser.msie)
{
$("<script type=\"text/javascript\" src=\"js/corner.js\"><\/script\>").appendTo(document.body);
$("#orta #orta_ic #sag_dis #baslik_dis #baslik").corner("top keep");
$("#orta #orta_ic #sag_dis #sag").corner("bottom right keep");
$("#orta #orta_ic #sag_dis #sag #ic").corner("5px keep");
$("#orta #orta_ic #sag_dis #sag #ic .beyaz").corner("4px keep");
$("#orta #orta_ic #sag_dis #sag #ic #sari").corner("4px keep");
$("#orta #orta_ic #sag_dis #sag #ic #syh").corner("4px keep");
$("#orta #orta_ic #sol #sol_menu .ana_menu").corner("4px keep");
$("#orta #orta_ic #sol #sol_menu .ic_menu a").corner("4px keep");
$("#orta #orta_ic #sag_dis #sag #ic .input").corner("4px keep");
$("#orta #orta_ic #sag_dis #sag #ic .input2").corner("4px keep");
$("#orta #orta_ic #sag_dis #sag #ic .input4").corner("4px keep")
}
else
{
$("<script type=\"text/javascript\" src=\"js/corner-fix.js\"><\/script\>").appendTo(document.body);
DD_roundies.addRule('#orta #orta_ic #sag_dis #baslik_dis #baslik', '8px 8px 0 0');
DD_roundies.addRule('#orta #orta_ic #sag_dis #sag', '0 8px 8px 8px');
DD_roundies.addRule('#orta #orta_ic #sag_dis #sag #ic', '4px');
DD_roundies.addRule('#orta #orta_ic #sag_dis #sag #ic .beyaz', '4px');
DD_roundies.addRule('#orta #orta_ic #sag_dis #sag #ic #sari', '4px');
DD_roundies.addRule('#orta #orta_ic #sag_dis #sag #ic #syh', '4px');
DD_roundies.addRule('#orta #orta_ic #sag_dis #sag #ic .input2', '4px');
DD_roundies.addRule('#orta #orta_ic #sag_dis #sag #ic .input4', '4px')
}
Cufon.replace('#en_ust #en_ust_alt #terket,#orta #orta_ic #sol #sol_menu .ana_menu,#orta #orta_ic #sag_dis #baslik_dis #baslik h1,#orta #orta_ic #sag_dis #sag #ic .beyaz h1,#orta #orta_ic #sag_dis #sag #ic .beyaz table#tbl1 tbody tr td span#sp,#orta #orta_ic #sag_dis #sag #ic .beyaz h3,#orta #orta_ic #sag_dis #sag #ic .beyaz2 h1,#orta #orta_ic #sag_dis #sag #ic #syh')
}
)