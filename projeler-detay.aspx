<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projeler-detay.aspx.cs" Inherits="projeler_detay" EnableEventValidation="False"%>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

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

<!--MESAJ-->
<asp:panel runat="server" id="mesaj_getir"></asp:panel>
<!--MESAJ-->

<!--BASLANGIC-->
<div class="beyaz">

<table id="tbl1" summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Projeler > Detay</h3></td>
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

<table summary="" cellspacing="0" class="margin5">
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
<tr>
<td class="tdb">Telefon</td>
<td><asp:label id="telefon" runat="server" /></td>
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
<td width="165" class="tdb" style="vertical-align:top">Proje Zimmet Listesi</td>
<td>
<anthem:GridView ID="tablo1" runat="server" onload="GridYuklenince1" onsorting="GridSirala1" onrowdatabound="OnRowDataBound1" onrowcreated="MouseOverStilleri" onpageindexchanging="GridSayfala1">
<Columns>

<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="id" runat="server" Text='<%# Bind("id") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="NO"><HeaderStyle CssClass="tdb2" Width="1%"/></asp:TemplateField>
<asp:BoundField DataField="urun" HeaderText="Ürün Adı" SortExpression="urun"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="kod" HeaderText="Kodu" SortExpression="kod"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="durum" HeaderText="Durumu" SortExpression="durum"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="adet" HeaderText="Adet" SortExpression="adet"><HeaderStyle CssClass="tdb"/></asp:BoundField>

</Columns>

</anthem:GridView>
</td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb" style="vertical-align:top">Projede Görevli Personeller</td>
<td>
<anthem:GridView ID="tablo2" runat="server" onload="GridYuklenince2" onsorting="GridSirala2" onrowdatabound="OnRowDataBound2" onrowcreated="MouseOverStilleri" onpageindexchanging="GridSayfala2">
<Columns>

<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="id" runat="server" Text='<%# Bind("id") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="NO"><HeaderStyle CssClass="tdb2" Width="1%"/></asp:TemplateField>
<asp:BoundField DataField="tc_kimlik" HeaderText="T.C. Kimlik No" SortExpression="tc_kimlik"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="adi_soyadi" HeaderText="Adı Soyadı" SortExpression="adi_soyadi"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="cinsiyet" HeaderText="Cinsiyet" SortExpression="cinsiyet"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="sertifika_durumu" HeaderText="Güvenlik Eğitimi" SortExpression="sertifika_durumu"><HeaderStyle CssClass="tdb"/></asp:BoundField>

</Columns>

</anthem:GridView>
</td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb" style="vertical-align:top">Personel Zimmeti</td>
<td>
<anthem:GridView ID="tablo3" runat="server" onload="GridYuklenince3" onsorting="GridSirala3" onrowdatabound="OnRowDataBound3" onrowcreated="MouseOverStilleri" onpageindexchanging="GridSayfala3">
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

<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="tamam" runat="server" Text='<%# Bind("tamam") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

</Columns>

</anthem:GridView>
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