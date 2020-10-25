<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ana-depo-urun-sevki.aspx.cs" Inherits="ana_depo_urun_sevki"  EnableEventValidation="false"%>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/depo-urun-sevki.js"></script>
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
<img src="images/ikon/light.png" class="ikon" alt=""/> <span class="alignmid">Ana Depodan sevk etmiş olduğunuz ürünleri güncelleyemez veya pasif edemezsiniz. Bu sebeple lütfen bu formu dikkatli kullanınız.</span>
</div>

<!--BASLANGIC-->
<div class="beyaz">

<table id="tbl1" summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Depodaki Tüm İşlemler > Ürün Sevki</h3></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5" id="tbl5">
<tbody>
<tr>
<td width="165" class="tdb">* Bağlı Olduğu Bölge</td>
<td>
<anthem:DropDownList ID="bolge_id" CssClass="input" AutoCallBack="True" runat="server" onselectedindexchanged="bolge_SelectedIndexChanged">
<asp:ListItem Value="0" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
</anthem:DropDownList>
</td>
</tr>
<tr>
<td width="165" class="tdb">* Bölgeye Bağlı Firma</td>
<td>
<anthem:DropDownList ID="firma_id" CssClass="input" AutoUpdateAfterCallBack="True" runat="server" Enabled="false">
<asp:ListItem Value="0" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
</anthem:DropDownList>
</td>
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
<td width="25%" class="tdb" align="center">Seçilen Ürünün Adı</td>
<td width="25%" class="tdb" align="center">Seçilen Ürünün Kodu</td>
<td width="25%" align="center" class="tdb">Ürün Durumu</td>
<td width="25%" align="center" class="tdb">Kullanılabilir Adet</td>
</tr>
<tr>
<td align="center"><asp:label id="urun_id_lbl" runat="server" /></td>
<td align="center"><asp:label id="urun_kodu_lbl" runat="server" /></td>
<td align="center"><asp:label id="urun_durumu_lbl" runat="server" /></td>
<td align="center"><asp:label id="urun_adet_lbl" runat="server" /></td>
</tr>
</tbody>
</table>
<input type="text" id="durum" name="durum" maxlength="1" readonly="readonly" class="inputtemiz" runat="server"/>
</anthem:Panel>
<anthem:TextBox ID="kullanilabilir_adet" runat="server" AutoUpdateAfterCallBack="True" ReadOnly="true" CssClass="inputtemiz">1</anthem:TextBox>
<anthem:TextBox ID="urun_id" runat="server" AutoUpdateAfterCallBack="True" ReadOnly="true" CssClass="inputtemiz">0</anthem:TextBox>
</td>
</tr>

<tr>
<td width="165" class="tdb">* Gönderilecek Adet</td>
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
<td align="center"><asp:Button ID="gonder" runat="server" Text="İşlemi Yap" CssClass="input2" onclick="submit_Click"/></td>
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