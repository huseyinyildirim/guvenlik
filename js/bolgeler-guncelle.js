$(document).ready
(
function()
{

$("<script type=\"text/javascript\" src=\"js/seekattention.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/js.js\"><\/script\>").appendTo(document.body);

$('#adi').bind('keyup', function() {BuyukHarf($(this))})

$("#checkbox").click
(
function()
{
if (typeof $("input[name=checkbox]:checked").val() != 'undefined') 
{
$("input[name=il_kodlari]").removeAttr("disabled")
}
else
{
$("input[name=il_kodlari]").attr("disabled", "disabled");
$("input[name=il_kodlari]").attr('checked', false)
}
}
)

$("#gonder").click
(
function()
{

if($("#adi").val() == "")
{
alert("Lütfen bölge adını belirtiniz.");
YakSondur($("#adi"));
return(false)
}

if (typeof $("input[name=checkbox]:checked").val() != 'undefined') 
{
if (typeof $("input[name=il_kodlari]:checked").val() == 'undefined') 
{
alert("Lütfen bölgede faaliyet gösterilen şehri veya şehirleri belirtiniz.");
YakSondur($("#sehirler"));
return(false)
}
}

}
)

}
)