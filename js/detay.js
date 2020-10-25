$(document).ready
(
function()
{
$("<script type=\"text/javascript\" src=\"js/print-element.js\"><\/script\>").appendTo(document.body)

$("#yazdir").click
(
function()
{
printElem({leaveOpen:true,printMode:'popup',pageTitle:'Detaylar'})
}
)

function printElem(options)
{
$('#basilacak').printElement(options)
}

}
)