$(document).ready
(
function()
{
$("<script type=\"text/javascript\" src=\"js/facebox.js\"><\/script\>").appendTo(document.body);
$("<script type=\"text/javascript\" src=\"js/captcha.js\"><\/script\>").appendTo(document.body);

if (!$.browser.msie)
{
$("#orta #orta_ic #giris,#orta #orta_ic #giris table td .input,#orta #orta_ic #giris table td .input2").corner("keep")
}
else
{
DD_roundies.addRule('#orta #orta_ic #giris,#orta #orta_ic #giris table td .input2', '10px')
}
}
)