﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bolgeler-guncelle.aspx.cs" Inherits="bolgeler_guncelle"  EnableEventValidation="false"%>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/bolgeler-guncelle.js"></script>
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
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Bölgeler > Güncelle</h3></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">* Adı</td>
<td><input type="text" id="adi" name="adi" maxlength="500" class="input" runat="server"/></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb" style="vertical-align:top" rowspan="2">* Faaliyet Gösterilen Şehirler</td>
<td><asp:label id="il_kodlari_lbl" runat="server" /></td>
</tr>
<tr>
<td>

<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td width="20" style="border-right:1px solid #c4e2fc"><input type="checkbox" class="alignmid" id="checkbox" name="checkbox"/></td>
<td>
<asp:listview runat="server" ID="il_listele" ItemPlaceholderID="ItemPlaceHolder" GroupItemCount="5">

<LayoutTemplate>
<table summary="" cellspacing="0" cellpadding="0" class="temiz" id="sehirler">
<asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
</table>
</LayoutTemplate>

<GroupTemplate>
<tr>
<asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
</tr>
</GroupTemplate>
<ItemTemplate>
<td style="padding:5px"><input name="il_kodlari" id="il_kodlari_<%# Eval("il_kodu") %>" type="checkbox" value="<%# Eval("il_kodu") %>" class="alignmid" disabled="disabled"/> <span class="alignmid"><%# Eval("il") %></span></td>
</ItemTemplate>

</asp:listview>
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
<td width="165" class="tdb">İşlem Yap. Yön.</td>
<td><asp:label id="admin_id" runat="server" /></td>
</tr>
</tbody>
</table>

<table id="tbl_gonder" summary="" cellspacing="0" cellpadding="0" class="margin5" runat="server">
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