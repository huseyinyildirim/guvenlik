<%@ Page Language="C#" AutoEventWireup="true" CodeFile="personeller-detay.aspx.cs" Inherits="personeller_detay" EnableEventValidation="False"  EnableViewState="false"%>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/detay.js"></script>
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

<!--BASLANGIC-->
<div class="beyaz">

<table id="tbl1" summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Personeller > Detay</h3></td>
<td class="tdb2" align="center" width="10%"><p><img src="images/ikon/print.png" class="ikon" alt=""/> <span class="alignmid"><a href="#" id="yazdir">Yazdır</a></span></p></td>
</tr>
</tbody>
</table>

<!--BASILACAK-->
<div id="basilacak">

<table summary="" cellspacing="0" class="margin5" id="tbl5">
<tbody>
<tr>
<td width="165" class="tdb">Durum</td>
<td><asp:label id="durum" runat="server" /></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5" id="Table1">
<tbody>
<tr>
<td width="165" class="tdb">Bağlı Olduğu Bölge</td>
<td><asp:label runat="server" id="bolge_id"/></td>
</tr>
<tr>
<td width="165" class="tdb">Bölgeye Bağlı Firma</td>
<td><asp:label runat="server" id="firma_id"/></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">T.C. Kimlik No</td>
<td><asp:label id="tc_kimlik" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Ad Soyad</td>
<td><asp:label id="adi_soyadi" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Cinsiyet</td>
<td><asp:label id="cinsiyet" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Telefon</td>
<td><asp:label id="telefon" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Mail</td>
<td><asp:label id="mail" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Doğum Tarihi</td>
<td><asp:label id="dogum_tarihi" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Doğum Yeri</td>
<td><asp:label id="dogum_yeri" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Ana Adı</td>
<td><asp:label id="ana_adi" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Baba Adı</td>
<td><asp:label id="baba_adi" runat="server" /></td>
</tr>
<tr>
<td class="tdb">SSK No</td>
<td><asp:label id="ssk_no" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Eğitim Durumu</td>
<td><asp:label id="egitim_durumu" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Ehliyet</td>
<td><asp:label id="ehliyet" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Güvenlik Eğitimi</td>
<td><asp:label id="guvenlik_egitimi" runat="server" /></td>
</tr>
<tr>
<td class="tdb">İl</td>
<td><asp:label id="il_kodu" runat="server" /></td>
</tr>
<tr>
<td class="tdb">İlçe</td>
<td><asp:label id="ilce_kodu" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Adres</td>
<td><asp:label id="adres" runat="server" /></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">Notlar</td>
<td><asp:label id="notlar" runat="server" /></td>
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
</div>
<!--BASILACAK-->

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