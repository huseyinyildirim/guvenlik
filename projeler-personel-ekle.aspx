<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projeler-personel-ekle.aspx.cs" Inherits="projeler_personel_ekle" EnableEventValidation="false"%>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/projeler-personel-ekle.js"></script>
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

<div id="sari">
<img src="images/ikon/light.png" class="ikon" alt=""/> <span class="alignmid">Projeye eklemiş olduğunuz personeli kaydet tuşuna bastıktan sonra sonra güncelleyemez veya silemezsiniz.</span>
</div>

<!--MESAJ-->
<asp:panel runat="server" id="mesaj_getir"></asp:panel>
<!--MESAJ-->

<!--BASLANGIC-->
<div class="beyaz">

<table id="tbl1" summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Projeler > Ekle > Personel Ekle</h3></td>
<td class="tdb" align="center" width="10%"><p><img src="images/ikon/tag.png" class="ikon" alt=""/> <span class="alignmid" id="sp">ADIM 3</span></p></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">Bağlı Olduğu Bölge</td>
<td><asp:label runat="server" id="bolge_id"/><asp:label runat="server" id="bolge_id_js" CssClass="inputtemiz"/></td>
</tr>
<tr>
<td width="165" class="tdb">Bölgeye Bağlı Firma</td>
<td><asp:label runat="server" id="firma_id"/><asp:label runat="server" id="firma_id_js" CssClass="inputtemiz"/></td>
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
<td width="165" class="tdb" style="vertical-align:top">Personel Ekle</td>
<td>

<anthem:GridView ID="tablo1" runat="server" onsorting="GridSirala" onrowdatabound="OnRowDataBound" onrowcreated="MouseOverStilleri">
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

<asp:TemplateField>
<ItemTemplate>
<asp:CheckBox runat="server" id="chck"></asp:CheckBox>
</ItemTemplate>
<ControlStyle CssClass="chck_span" />
<HeaderStyle CssClass="tdb2" Width="1%"/>
</asp:TemplateField>

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

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td align="center"><asp:Button ID="gonder" runat="server" Text="Kaydet ve Devam Et" CssClass="input2" onclick="submit_Click" /></td>
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