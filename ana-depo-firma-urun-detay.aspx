<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ana-depo-firma-urun-detay.aspx.cs" Inherits="ana_depo_firma_urun_detay" EnableEventValidation="False" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

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

<asp:literal runat="server" id="sayfalama_literal1"></asp:literal>

<table summary="" cellspacing="0" cellpadding="0" style="margin-bottom:5px">
<tbody>
<tr>
<td align="center" colspan="4" class="tdb3"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Depodaki İşlemler (Firma Bazlı) > Hareketler</h3></td>
</tr>
<tr>
<td width="25%" class="tdb" align="center">Bölge Adı</td>
<td width="25%" class="tdb" align="center">Firma Adı</td>
<td width="25%" class="tdb" align="center">Depodaki Ürün Adeti</td>
<td width="25%" class="tdb" align="center">Depodaki İşlem Adeti</td>
</tr>
<tr>
<td align="center"><asp:label id="bolge_lbl" runat="server" /></td>
<td align="center"><asp:label id="firma_lbl" runat="server" /></td>
<td align="center"><asp:label id="urun_adet_lbl" runat="server" /></td>
<td align="center"><asp:label id="islem_adet_lbl" runat="server" /></td>
</tr>
</tbody>
</table>

<anthem:GridView ID="tablo1" runat="server" onload="GridYuklenince" onsorting="GridSirala" onrowdatabound="OnRowDataBound" onrowcreated="MouseOverStilleri">
<Columns>
<asp:TemplateField HeaderText="NO"><HeaderStyle CssClass="tdb2" Width="1%"/></asp:TemplateField>
<asp:BoundField DataField="urun" HeaderText="Ürün Adı" SortExpression="urun"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="kod" HeaderText="Ürün Kodu" SortExpression="kod"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="durum" HeaderText="Durumu" SortExpression="durum"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="adet" HeaderText="Adet" SortExpression="adet"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="tip" HeaderText="Tip" SortExpression="tip"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="admin_id" HeaderText="İşlem Yap. Yön." SortExpression="admin_id"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="tarih" HeaderText="Tarih" SortExpression="tarih"><HeaderStyle CssClass="tdb"/></asp:BoundField>

<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="proje_id" runat="server" Text='<%# Bind("proje_id") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

</Columns>

<EmptyDataTemplate>
<!--KAYIT YOK-->
<p><img src="images/ikon/stop.png" class="ikon" alt=""/> <span class="alignmid">HERHANGİ BİR KAYIT BULUNAMADI.</span></p>
<!--KAYIT YOK-->
</EmptyDataTemplate>

</anthem:GridView>

<asp:literal runat="server" id="sayfalama_literal2"></asp:literal>

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