$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/cookie.js\"><\/script\>").appendTo(document.body);
UstCookieKontrol2()
SolCookieKontrol2()

$("#orta #orta_ic #sol #sol_menu p.ana_menu").click
(
function()
{
$(this).next("div.ic_menu").slideToggle(500).siblings("div.ic_menu").slideUp("slow")
}
)

$("#orta #orta_ic #sol #sol_menu p.ana_menu").mouseover
(
function()
{
$(this).css("opacity","0");
$(this).stop().animate({opacity:1},'slow')
}
)

$("#orta #orta_ic #sol #sol_menu .ic_menu a").click
(
function()
{
$('#opaque').css('display','block')
}
)

$("#yukari").click
(
function()
{
if ($.cookie("ust") == "kapali")
{
$.cookie("ust", "acik", {expires:365});
UstMenuKapat()
}
else
{
$.cookie("ust", "kapali", {expires:365});
UstMenuAc()
}
UstCookieKontrol1()
}
)

function UstMenuKapat()
{
$("#en_ust_ust").css("background","#513e50");
$("#yukari_ok").attr("src", "images/asagi.png");
$("#ust").slideUp("slow")
}

function UstMenuAc()
{
$("#ust").show("slow");
$("#ust").css("visibility","visible");
$("#ust").css("display","block");
$("#en_ust_ust").css("background","#000");
$("#yukari_ok").attr("src", "images/yukari.png")
}

function UstCookieKontrol1()
{
if ($.cookie("ust") == "kapali")
{
UstMenuKapat()
}
else
{
UstMenuAc()
}
}

function UstCookieKontrol2()
{
if ($.cookie("ust") == "kapali")
{
$("#ust").css("visibility","hidden");
$("#ust").css("display","none");
UstMenuKapat()
}
else
{
UstMenuAc()
}
}

$("#bslk_sol").click
(
function ()
{
if ($.cookie("menu") == "kapali")
{
$.cookie("menu", "acik", {expires:365});
SolMenuKapat()
}
else
{
$.cookie("menu", "kapali", {expires:365});
SolMenuAc()
}
SolCookieKontrol1()
}
)

function SolMenuKapat()
{
$("#sag_dis").css("margin-left","0");
$("#bslk_sol img").attr("src","images/ikon/menu-ok2.png");
$("#bslk_sol span").text("Göster");
$("#sol").hide("slow")
}

function SolMenuAc()
{       
$("#sol").show("slow");
$("#sol").css("visibility","visible");
$("#sol").css("display","block");
$("#sag_dis").css("margin-left","208px");
$("#bslk_sol img").attr("src","images/ikon/menu-ok1.png");
$("#bslk_sol span").text("Gizle")
}

function SolCookieKontrol1()
{
if ($.cookie("menu") == "kapali")
{
SolMenuKapat()
}
else
{
SolMenuAc()
}
}

function SolCookieKontrol2()
{
if ($.cookie("menu") == "kapali")
{
$("#sol").css("visibility","hidden");
$("#sol").css("display","none");
SolMenuKapat()
}
else
{
SolMenuAc()
}
}

}
)