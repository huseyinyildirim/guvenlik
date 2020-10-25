<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ana-depo-islemler-firma.aspx.cs" Inherits="ana_depo_islemler_firma" EnableEventValidation="False" %>

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

<anthem:GridView ID="tablo1" runat="server" onload="GridYuklenince" onsorting="GridSirala" onrowdatabound="OnRowDataBound" onrowcreated="MouseOverStilleri">
<Columns>

<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="id" runat="server" Text='<%# Bind("id") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="NO"><HeaderStyle CssClass="tdb2" Width="1%"/></asp:TemplateField>
<asp:BoundField DataField="bolge" HeaderText="Bağlı Olduğu Bölge" SortExpression="bolge"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="adi" HeaderText="Firma Adı" SortExpression="adi"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="urun_sayisi" HeaderText="Depo Ürün Adeti" SortExpression="urun_sayisi"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="islem_sayisi" HeaderText="Depo İşlem Adeti" SortExpression="islem_sayisi"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField HeaderText="Gör"><HeaderStyle CssClass="tdb2" Width="1%"/></asp:BoundField>
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