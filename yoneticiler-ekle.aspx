<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yoneticiler-ekle.aspx.cs" Inherits="yoneticiler_ekle" EnableEventValidation="False" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/facebox.js"></script>
<script type="text/javascript" src="js/yoneticiler-ekle.js"></script>
<!--SCRIPTS-->

<!--ANA DIV-->
<div id="orta">

<!--ORTA DIV-->
<div id="orta_ic">

<!--SOL KISIM-->
<div id="sol">
<include:menu ID="menu" runat="server"/>
</div>
<!--SOL KISIM-->

<!--SAG KISIM-->
<div id="sag_dis">

<!--SAYFA BASLIGI-->
<asp:panel runat="server" id="baslik_getir"></asp:panel>
<!--SAYFA BASLIGI-->

<div id="sag">

<!--FORM-->
<form id="form1" runat="server">
<fieldset>

<!--SAG IC-->
<div id="ic">

<!--MESAJ-->
<asp:panel runat="server" id="mesaj_getir"></asp:panel>
<!--MESAJ-->

<!--BASLANGIC-->
<div class="beyaz">

<table id="tbl1" summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Yöneticiler > Ekle</h3></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5" id="tbl5">
<tbody>
<tr>
<td width="165" class="tdb">* Bağlı Olduğu Bölge</td>
<td>
<anthem:DropDownList ID="bolge_id" CssClass="input" AutoCallBack="True" runat="server" onselectedindexchanged="bolge_SelectedIndexChanged">
<asp:ListItem Value="0" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
</anthem:DropDownList>
</td>
</tr>
<tr>
<td width="165" class="tdb">* Bölgeye Bağlı Firma</td>
<td>
<anthem:DropDownList ID="firma_id" CssClass="input" AutoUpdateAfterCallBack="True" runat="server" Enabled="false">
<asp:ListItem Value="0" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
</anthem:DropDownList>
</td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">* Ad Soyad</td>
<td><input type="text" id="adi_soyadi" name="adi_soyadi" maxlength="150" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">* Kullanıcı Adı</td>
<td>
<input type="text" id="kullanici_adi" name="kullanici_adi" maxlength="150" class="input" runat="server"/>
<div class="sari2" id="kullanici_adi_kontrol"><img src="images/ikon/info_k.png" class="ikon" alt=""/> <span class="alignmid">Seçtiğiniz kullanıcı adı kullanımdadır. Lütfen başka bir kullanıcı adı belirtiniz.</span></div>
</td>
</tr>
<tr>
<td class="tdb">* Şifre</td>
<td>
<input type="password" id="sifre" name="sifre" maxlength="32" class="input" runat="server"/>
</td>
</tr>
<tr>
<td class="tdb">* Yönetici Tipi</td>
<td>
<asp:DropDownList ID="tip" CssClass="input" runat="server">
<asp:ListItem Value="9" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
<asp:ListItem Value="0">Genel Yönetici (Root)</asp:ListItem>
<asp:ListItem Value="1">Bölge Yöneticisi</asp:ListItem>
<asp:ListItem Value="2">Firma Yöneticisi</asp:ListItem>
<asp:ListItem Value="3">Genel Depo Yöneticisi</asp:ListItem>
<asp:ListItem Value="4">Firma Depo Yöneticisi</asp:ListItem>
</asp:DropDownList>
</td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">İşlem Yap. Yön.</td>
<td><asp:label id="admin_id" runat="server" /></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5" runat="server">
<tbody>
<tr>
<td align="center"><asp:Button ID="gonder" runat="server" Text="Ekle" CssClass="input2" onclick="submit_Click" /></td>
</tr>
</tbody>
</table>

</div>
<!--BASLANGIC-->

</div>
<!--SAG IC-->

</fieldset>
</form>
<!--FORM-->

</div>
</div>
<!--SAG KISIM-->

</div>
<!--ORTA DIV-->

</div>
<!--ANA DIV-->
<include:alt ID="alt" runat="server"/>
</body>
</html>