<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" EnableViewState="False" EnableEventValidation="False" %>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust2.ascx" TagPrefix="include" TagName="ust2"%>
<%@ Register src="~/ascx/alt2.ascx" TagPrefix="include" TagName="alt2"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust2 ID="ust2" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/giris.js"></script>
<!--SCRIPTS-->

<!--ANA DIV-->
<div id="orta">

<!--ORTA DIV-->
<div id="orta_ic">

<!--DEFAULT-->
<div id="giris">

<!--FORM-->
<form id="form1" runat="server">
<fieldset>
<table summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td colspan="2" align="center"><img src="images/users.png" alt="" id="rsm"/></td>
</tr>
<tr>
<td><b>Kullanıcı Adı</b></td>
<td><input type="text" id="kullanici_adi" name="kullanici_adi" maxlength="150" class="input"/></td>
</tr>
<tr>
<td><b>Şifre</b></td>
<td><input type="password" id="sifre" name="sifre" maxlength="60" class="input"/></td>
</tr>
<tr>
<td>&nbsp;</td>
<td align="center"><a onclick="CaptchaYukle()" class="pointer" title="Güvenlik kodunu yeniden yüklemek için tıklayınız"><img src="inc/captcha.aspx" class="alignmid" alt="" id="captcha_resim"/></a>
<p class="span">Güvenlik kodu büyük-küçük harf duyarlıdır.</p>
</td>
</tr>
<tr>
<td><b>Güvenlik Kodu</b></td>
<td><input type="text" id="captcha" name="captcha" maxlength="5" class="input"/></td>
</tr>
<tr>
<td colspan="2" align="center"></td>
</tr>
<tr>
<td colspan="2" align="center"><asp:Button ID="gonder" runat="server" Text="Giriş Yap" CssClass="input2" onclick="submit_Click" /></td>
</tr>
</tbody>
</table>
</fieldset>
</form>
<!--FORM-->

</div>
<!--DEFAULT-->

</div>
<!--ORTA DIV-->

</div>
<!--ANA DIV-->
<include:alt2 ID="alt2" runat="server"/>
</body>
</html>