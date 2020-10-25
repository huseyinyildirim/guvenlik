$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$('#urun,#kod').bind('keyup', function() {BuyukHarf($(this))})

$("#gonder").click
(
function()
{

if($("#urun").val() == "")
{
alert("Lütfen ürün adını belirtiniz.");
YakSondur($("#urun"));
return(false)
}

if($("#urun").val().length<4)
{
alert("Ürün adı 4 karakterden küçük olamaz!");
YakSondur($("#urun"));
return(false)
}

}
)

}
)