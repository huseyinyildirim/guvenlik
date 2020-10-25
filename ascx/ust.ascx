<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ust.ascx.cs" Inherits="ust" %>

<!--SCRIPTS-->
<script type="text/javascript">
<!--
$(document).ready
(
function()
{
$("#terket_a").click(function(){return(confirm('Sistemden çıkış yapmak üzeresiniz.\nİlgili işlemi onaylıyor musunuz?'))})
}
)
//-->
</script>
<!--SCRIPTS-->

<div id="en_ust">
<div id="en_ust_ust"></div>
<!--UST DIV-->
<div id="ust">
<!--LOGO-->
<div id="logo"><a href="default.aspx"><img src="images/blank.gif" alt=""/></a></div>
<!--LOGO-->
<!--PANEL LOGO-->
<div id="panel_logo"></div>
<!--PANEL LOGO-->
</div>
<!--UST DIV-->
<div id="en_ust_alt">
<div id="yukari"><a href="javascript:void(0)"><img src="images/yukari.png" id="yukari_ok" alt=""/></a></div>
<div id="terket" class="png"><a href="?terket=ok" id="terket_a"><img src="images/ikon/security-locked.png" class="ikon" alt=""/><span class="alignmid">Paneli Terket</span></a></div>
</div>
</div>

<!--PAGE LOADING-->
<div id="opaque" onclick="$('#opaque').css('display','none')"></div>
<!--PAGE LOADING-->