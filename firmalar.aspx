<%@ Page Language="C#" AutoEventWireup="true" CodeFile="firmalar.aspx.cs" Inherits="firmalar" EnableEventValidation="False" %>

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

<!--MESAJ-->
<asp:panel runat="server" id="mesaj_getir"></asp:panel>
<!--MESAJ-->

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
<asp:BoundField DataField="bolge_id" HeaderText="Bağlı Olduğu Bölge" SortExpression="bolge_id"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="adi" HeaderText="Firma Adı" SortExpression="adi"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="tel" HeaderText="Telefon" SortExpression="tel"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="admin_id" HeaderText="İşlem Yap. Yön." SortExpression="admin_id"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField DataField="onay" HeaderText="Durum" SortExpression="onay"><HeaderStyle CssClass="tdb"/></asp:BoundField>
<asp:BoundField HeaderText="Gör"><HeaderStyle CssClass="tdb2" Width="1%"/></asp:BoundField>
<asp:BoundField HeaderText="Gün."><HeaderStyle CssClass="tdb2" Width="1%"/></asp:BoundField>
<asp:BoundField HeaderText="Dur."><HeaderStyle CssClass="tdb2" Width="1%"/></asp:BoundField>
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