<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ana-depo-urun-guncelle.aspx.cs" Inherits="ana_depo_urun_guncelle"  EnableEventValidation="false"%>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/depo-urun-guncelle.js"></script>
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
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Depodaki Tüm İşlemler > Güncelle</h3></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5" id="tbl5">
<tbody>
<tr>
<td width="165" class="tdb">Durum</td>
<td><asp:label id="onay" runat="server" /></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">Adı</td>
<td><asp:label id="urun" runat="server" /></td>
</tr>
<tr>
<td width="165" class="tdb">Kodu</td>
<td><asp:label id="kodu" runat="server" /></td>
</tr>
<tr>
<td width="165" class="tdb" rowspan="2">Durumu</td>
<td><asp:label id="durum_lbl" runat="server" /></td>
</tr>
<tr>
<td>
<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td width="20"><input type="checkbox" class="alignmid" id="checkbox1" name="checkbox1"/></td>
<td><asp:DropDownList ID="durum" CssClass="input" runat="server" disabled="disabled">
<asp:ListItem Value="9" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
<asp:ListItem Value="0">Eski</asp:ListItem>
<asp:ListItem Value="1">Yeni</asp:ListItem>
</asp:DropDownList>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
<tr>
<td width="165" class="tdb">Adet</td>
<td><input type="text" id="adet" name="adet" maxlength="10" class="input" runat="server"/></td>
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