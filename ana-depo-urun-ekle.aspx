<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ana-depo-urun-ekle.aspx.cs" Inherits="ana_depo_urun_ekle"  EnableEventValidation="false"%>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/depo-urun-ekle.js"></script>
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
<img src="images/ikon/light.png" class="ikon" alt=""/> <span class="alignmid">Ana Depoya girmiş olduğunuz ürünler kullanılmaya başladıktan sonra güncelleyemez veya pasif edemezsiniz.</span>
</div>

<!--BASLANGIC-->
<div class="beyaz">

<table id="tbl1" summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Depodaki Tüm İşlemler > Depoya Ürün Ekle</h3></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">* Ürün</td>
<td>

<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td>
<input type="text" id="urun" name="urun" class="input"/>
<input type="text" id="urun_getir" name="urun_getir" class="input" style="display:none"/>
</td>
<td width="40">
<anthem:Button ID="sec" runat="server" Text="SEÇ »" CssClass="input4" onclick="sec_Click"/>
</td>
</tr>
</tbody>
</table> 

<anthem:Panel ID="goster" runat="server" AutoUpdateAfterCallBack="True" Visible="false" CssClass="margin5">
<table summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="50%" class="tdb" align="center">Seçilen Ürünün Adı</td>
<td class="tdb" align="center">Seçilen Ürünün Kodu</td>
</tr>
<tr>
<td align="center"><asp:label id="urun_id_lbl" runat="server" /></td>
<td align="center"><asp:label id="urun_kodu_lbl" runat="server" /></td>
</tr>
</tbody>
</table>
</anthem:Panel>
<anthem:TextBox ID="urun_id" runat="server" AutoUpdateAfterCallBack="True" ReadOnly="true" CssClass="inputtemiz">0</anthem:TextBox>
</td>
</tr>

<tr>
<td width="165" class="tdb">* Durumu</td>
<td>
<select id="durum" name="durum" class="input">
<option value="9">------ SEÇİNİZ ------</option>
<option value="0">Eski</option>
<option value="1">Yeni</option>
</select>
</td>
</tr>
<tr>
<td width="165" class="tdb">Adet</td>
<td><input type="text" id="adet" name="adet" maxlength="10" class="input" value="1"/></td>
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

<table id="tbl_gonder" summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td align="center"><asp:Button ID="gonder" runat="server" Text="Ekle" CssClass="input2" onclick="submit_Click"/></td>
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