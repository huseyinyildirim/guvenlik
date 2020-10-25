﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="firmalar-guncelle.aspx.cs" Inherits="firmalar_guncelle"  EnableEventValidation="false"%>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/firmalar-guncelle.js"></script>
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

<div id="sari">
<img src="images/ikon/light.png" class="ikon" alt=""/> <span class="alignmid">Değiştirmek istediğiniz alanlar için yanında bulunan kutucuk veya kutucukları işaretleyiniz.</span>
</div>

<!--BASLANGIC-->
<div class="beyaz">

<table id="tbl1" summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Firmalar > Güncelle</h3></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5" id="tbl5">
<tbody>
<tr>
<td width="165" class="tdb" rowspan="2">* Bağlı Olduğu Bölge</td>
<td><asp:label runat="server" id="bolge_id_lbl"/></td>
</tr>
<tr>
<td>
<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td width="20"><input type="checkbox" class="alignmid" id="checkbox1" name="checkbox1"/></td>
<td>
<anthem:DropDownList ID="bolge_id" CssClass="input3" AutoCallBack="True" runat="server">
<asp:ListItem Value="0" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
</anthem:DropDownList>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">* Firma Adı</td>
<td><input type="text" id="adi" name="adi" maxlength="500" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">* Telefon</td>
<td><input type="text" id="tel" name="tel" maxlength="13" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">Faks</td>
<td><input type="text" id="faks" name="faks" maxlength="13" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">Mail</td>
<td><input type="text" id="mail" name="mail" maxlength="320" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">Vergi Dairesi</td>
<td><input type="text" id="vergi_dairesi" name="vergi_dairesi" maxlength="80" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">Vergi No</td>
<td><input type="text" id="vergi_no" name="vergi_no" maxlength="11" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">Ticaret Sicil No</td>
<td><input type="text" id="ticaret_sicil_no" name="ticaret_sicil_no" maxlength="255" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb" rowspan="2">İl</td>
<td><asp:label runat="server" id="il_kodu_lbl"/></td>
</tr>
<tr>
<td>
<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td width="20"><input type="checkbox" class="alignmid" id="checkbox2" name="checkbox2"/></td>
<td>
<anthem:DropDownList ID="il_kodu" CssClass="input3" AutoCallBack="True" runat="server" onselectedindexchanged="il_kodu_SelectedIndexChanged">
<asp:ListItem Value="0" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
</anthem:DropDownList>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
<tr>
<td class="tdb" rowspan="2">İlçe</td>
<td><asp:label runat="server" id="ilce_kodu_lbl"/></td>
</tr>
<tr>
<td>
<anthem:DropDownList ID="ilce_kodu" CssClass="input" AutoUpdateAfterCallBack="True" runat="server" Enabled="False">
<asp:ListItem Value="0" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
</anthem:DropDownList>
</td>
</tr>
<tr>
<td class="tdb">Adres</td>
<td><asp:textbox runat="server" id="adres" TextMode="MultiLine" CssClass="input" Rows="4"/></td>
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

<table summary="" cellspacing="0" cellpadding="0" class="margin5" id="tbl_gonder" runat="server">
<tbody>
<tr>
<td align="center"><asp:Button ID="gonder" runat="server" Text="Güncelle" CssClass="input2" onclick="submit_Click" /></td>
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