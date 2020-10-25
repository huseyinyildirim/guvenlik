$('#tel').bind('keyup',function(){SifirSil($(this))})

function SifirSil(str)
{
if(str.val().substring(0,1)=="0")
{
str.val(str.val().substring(0,1).replace("0",""));
}
}

MailKontrol=function()
{
var string=arguments[0]
var allowedChars="1234567890abcdefghijklmnoprstuvyzqwx[].+@-_ABCDEFGHIJKLMNOPRSTUVYZQWX"
var cf=/(@.*@)|(\.\.)|(^\.)|(^@)|(@$)|(\.$)|(@\.)/
var cn=/^.+\@(\[?)[a-zA-Z0-9\-\.]+\.([a-zA-Z]{2,8}|[0-9]{1,3})(\]?)$/
for (var i = 0; i < string.length; i++)
{
if(allowedChars.indexOf(string.charAt(i))<0)
return false
}
if(!string.match(cf)&&string.match(cn))return-1
}

function KucukHarf(str)
{
if(!str.val()=="")
{
str.val(str.replace(/[^\-0\s]*/g,''));
}
}

function BuyukHarf(str)
{
if(!str.val()=="")
{
str.val(str.val().toUpperCase())
}
}

function SayisalKarakter(str)
{
if(!str.val()=="")
{
if(!str.val().match(/^[\-0-9\s]+$/g))
{
str.val(str.val().replace(/[^\-0-9\s]*/g,''));
alert("Bu alan yalnızca sayısal karakterlerden oluşmalıdır.")
}
}
}

function YakSondur(str)
{
str.seekAttention({ paddingTop: 5, paddingBottom: 5, paddingLeft: 5, paddingRight: 5 });
str.focus()
}

function Sorgula(str1,str2,dgr)
{
$.ajax
(
{
type: "GET",
url: "ajax/kullanici-adi-kontrol.aspx",
data: "deger=" + $(str1).val(),
success: function(msg)
{
if (msg>parseInt(dgr))
{
$(str2).css("visibility","visible");
$(str2).css("display","block");
$(str2).effect("bounce",{times:3},300)
}
else
{
$(str2).hide("slow");
$(str2).css("visibility","hidden");
$(str2).css("display","none")
}
}
}
)
}

function TumunuSecName(str1,str2)
{
if (str2=="0")
{
$("input[name="+str1+"]").attr('checked',true)
}
if (str2=="1")
{
$("input[name="+str1+"]").attr('checked',false)
}
}

function TumunuSecInput(str1,str2)
{
if (str2=="0")
{
$(str1).attr('checked',true)
}
if (str2=="1")
{
$(str1).attr('checked',false)
}
}