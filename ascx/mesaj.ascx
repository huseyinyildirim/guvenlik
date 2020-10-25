<%@ Control Language="C#" AutoEventWireup="true"%>

<!--SCRIPTS-->
<script type="text/javascript">
<!--
$(document).ready
(
function()
{
$("#orta #orta_ic #sag_dis #sag #ic #syh").focus();
$("#orta #orta_ic #sag_dis #sag #ic #syh").effect("highlight",{},4000).hide("drop",{direction:"down"},1000)
}
)
//-->
</script>
<asp:literal runat="server" id="yonlendir" Visible="false">
<script type="text/javascript" src="js/timer.js"></script>
<script type="text/javascript">
<!--
$(document).ready
(
function()
{
$.timer(5100,function(timer)
{
var ref = document.referrer;
location.href = ref;
timer.stop();
}
)
}
)
//-->
</script>
</asp:literal>
<!--SCRIPTS-->

<div id="syh">
<asp:Label id="msg" runat="server" />
</div>