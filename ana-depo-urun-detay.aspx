<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ana-depo-urun-detay.aspx.cs" Inherits="ana_depo_urun_detay" EnableEventValidation="False" %>

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
<td align="center" colspan="4" class="tdb3"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Depodaki İşlemler (Ürün Bazlı) > Hareketler</h3></td>
</tr>
<tr>
<td width="25%" class="tdb" align="center">Adı</td>
<td width="25%" class="tdb" align="center">Kodu</td>
<td width="25%" class="tdb" align="center">Durumu</td>
<td width="25%" class="tdb" align="center">Kalan Adet</td>
</tr>
<tr>
<td align="center"><asp:label id="adi_lbl" runat="server" /></td>
<td align="center"><asp:label id="kodu_lbl" runat="server" /></td>
<td align="center"><asp:label id="durumu_lbl" runat="server" /></td>
<td align="center"><asp:label id="kalan_lbl" runat="server" /></td>
</tr>
</tbody>
</table>

<anthem:GridView ID="tablo1" runat="server" onload="GridYuklenince" onsorting="GridSirala" onrowdatabound="OnRowDataBound" onrowcreated="MouseOverStilleri">
<Columns>
<asp:TemplateField HeaderText="NO"><HeaderStyle CssClass="tdb2" Width="1%"/></asp:TemplateField>
<asp:BoundField DataField="bolge" HeaderText="Bölge Adı" SortExpression="bolge"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="firma" HeaderText="Firma Adı" SortExpression="firma"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="proje" HeaderText="Proje Adı" SortExpression="proje"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="tc_kimlik" HeaderText="T.C. Kimlik" SortExpression="tc_kimlik"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="adi_soyadi" HeaderText="Ad Soyad" SortExpression="adi_soyadi"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="adet" HeaderText="Adet" SortExpression="adet"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="tip" HeaderText="Tip" SortExpression="tip"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="admin_id" HeaderText="İşlem Yap. Yön." SortExpression="admin_id"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="tarih" HeaderText="Tarih" SortExpression="tarih"><HeaderStyle CssClass="tdb"/></asp:BoundField>
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