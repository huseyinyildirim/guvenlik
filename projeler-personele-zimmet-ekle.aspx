<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projeler-personele-zimmet-ekle.aspx.cs" Inherits="projeler_personele_zimmet_ekle"  EnableEventValidation="false"%>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/projeler-personel-zimmet-ekle.js"></script>
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
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Projeler > Ekle > Personel Zimmeti</h3></td>
<td class="tdb" align="center" width="10%"><p><img src="images/ikon/tag.png" class="ikon" alt=""/> <span class="alignmid" id="sp">ADIM 4</span></p></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">Bağlı Olduğu Bölge</td>
<td><asp:label runat="server" id="bolge_id"/><asp:label runat="server" id="bolge_id_js"/></td>
</tr>
<tr>
<td width="165" class="tdb">Bölgeye Bağlı Firma</td>
<td><asp:label runat="server" id="firma_id"/><asp:label runat="server" id="firma_id_js"/></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">Proje Yöneticisi</td>
<td><asp:label runat="server" id="personel_id"/></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">Başlangıç Tarihi</td>
<td><asp:label runat="server" id="baslangic"/></td>
</tr>
<tr>
<td width="165" class="tdb">Bitiş Tarihi</td>
<td><asp:label runat="server" id="bitis"/></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">Adı</td>
<td><asp:label id="adi" runat="server" /></td>
</tr>
<tr>
<td class="tdb">Sorumlu Kişi</td>
<td><asp:label id="sorumlu" runat="server" /></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb" style="vertical-align:top">Personele Zimmet Ekle</td>
<td>
<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td style="padding-right:10px" width="70%">
<input type="text" id="urun" name="urun" class="input"/>
<input type="text" id="urun_getir" name="urun_getir" class="input" style="display:none"/>
</td>
<td><input style="padding-left:10px" type="text" id="adet" name="adet" class="input" value="1"/></td>
<td width="40"><asp:CheckBox ID="tum_personel" runat="server" AutoCallBack="true" Checked="True"/></td>
<td>
<asp:dropdownlist runat="server" id="proje_personelleri" CssClass="input3">
<asp:ListItem Value="0" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
</asp:dropdownlist></td>
<td width="40" style="padding-left:5px">
<anthem:Button ID="sec" runat="server" Text="SEÇ »" CssClass="input4" onclick="sec_Click"/>
</td>
</tr>
</tbody>
</table>

<anthem:GridView ID="tablo1" runat="server" onload="GridYuklenince" onsorting="GridSirala" onrowdatabound="OnRowDataBound" onrowcreated="MouseOverStilleri" onpageindexchanging="GridSayfala" CssClass="margin5">
<Columns>

<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="id" runat="server" Text='<%# Bind("id") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="NO"><HeaderStyle CssClass="tdb2" Width="1%"/></asp:TemplateField>
<asp:BoundField DataField="tc_kimlik" HeaderText="T.C. Kimlik No" SortExpression="tc_kimlik"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="adi_soyadi" HeaderText="Adı Soyadı" SortExpression="adi_soyadi"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="urun" HeaderText="Ürün Adı" SortExpression="urun"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="kod" HeaderText="Kodu" SortExpression="kod"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="durum" HeaderText="Durumu" SortExpression="durum"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="adet" HeaderText="Adet" SortExpression="adet"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField HeaderText="SİL"><HeaderStyle CssClass="tdb2" Width="1%"/></asp:BoundField>

<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="tamam" runat="server" Text='<%# Bind("tamam") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

</Columns>

</anthem:GridView>

<anthem:TextBox ID="tr_count" runat="server" AutoUpdateAfterCallBack="True" ReadOnly="true" CssClass="inputtemiz">0</anthem:TextBox>

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

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td align="center"><asp:Button ID="gonder" runat="server" Text="Kaydet ve Bitir" CssClass="input2" onclick="submit_Click" /></td>
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