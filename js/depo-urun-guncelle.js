$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$('#adet').bind('keyup', function() {SayisalKarakter($(this)) })

$("#checkbox1").click
(
function()
{
if (typeof $("input[name=checkbox1]:checked").val() != 'undefined') 
{
$("#durum").removeAttr("disabled")
}
else
{
$("#durum").attr("disabled", "disabled");
$("#durum").attr("selectedIndex",0)
}
}
)

$("#gonder").click
(
function()
{

if (typeof $("input[name=checkbox1]:checked").val() != 'undefined')
{
if($("#durum").val() == "9")
{
alert("Lütfen ürün durumunu belirtiniz.");
YakSondur($("#durum"));
return(false)
}
}

if($("#adet").val() == "9")
{
alert("Lütfen ürününün adetini belirtiniz.");
YakSondur($("#adet"));
return(false)
}

}
)

}
)