<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projeler-guncelle.aspx.cs" Inherits="projeler_guncelle"  EnableEventValidation="false"%>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="~/ascx/sitil.ascx" TagPrefix="include" TagName="sitil"%>
<%@ Register src="~/ascx/ust.ascx" TagPrefix="include" TagName="ust"%>
<%@ Register src="~/ascx/menu.ascx" TagPrefix="include" TagName="menu"%>
<%@ Register src="~/ascx/alt.ascx" TagPrefix="include" TagName="alt"%>

<include:sitil ID="sitil" runat="server"/>
<include:ust ID="ust" runat="server"/>

<!--SCRIPTS-->
<script type="text/javascript" src="js/projeler-guncelle.js"></script>
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
<img src="images/ikon/light.png" class="ikon" alt=""/> <span class="alignmid">Değiştirmek istediğiniz alanlar için yanında bulunan kutucuk veya kutucukları işaretleyiniz.</span>
</div>

<!--BASLANGIC-->
<div class="beyaz">

<table id="tbl1" summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td class="tdb2"><h3><img src="images/ikon/folder-open.png" class="ikon" alt=""/> Projeler > Güncelle</h3></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5" id="tbl5">
<tbody>
<tr>
<td width="165" class="tdb" rowspan="2">* Bağlı Olduğu Bölge</td>
<td><asp:label runat="server" id="bolge_id_lbl"/></td>
</tr>
<tr>
<td>
<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td width="20"><input type="checkbox" class="alignmid" id="checkbox1" name="checkbox1"/></td>
<td>
<anthem:DropDownList ID="bolge_id" CssClass="input3" AutoCallBack="True" runat="server" OnSelectedIndexChanged="bolge_SelectedIndexChanged">
<asp:ListItem Value="0" Selected="True">------ SEÇİNİZ ------</asp:ListItem>
</anthem:DropDownList>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
<tr>
<td width="165" class="tdb" rowspan="2">* Bölgeye Bağlı Firma</td>
<td><asp:label runat="server" id="firma_id_lbl"/></td>
</tr>
<tr>
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
<td width="165" class="tdb" rowspan="2">* Proje Yöneticisi <span><i>(Sorumlu)</i></span></td>
<td><asp:label runat="server" id="personel_id_lbl"/></td>
</tr>
<tr>
<td>

<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td width="20"><input type="checkbox" class="alignmid" id="checkbox2" name="checkbox2"/></td>
<td>
<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td>
<input type="text" id="personel" name="personel" class="input" disabled="disabled"/>
<input type="text" id="personel_getir" name="personel_getir" class="input" style="display:none" disabled="disabled"/>
</td>
<td width="40">
<anthem:Button ID="sec" runat="server" Text="SEÇ »" CssClass="input4" onclick="sec_Click"/>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>

<anthem:Panel ID="goster" runat="server" AutoUpdateAfterCallBack="True" Visible="false" CssClass="margin5">
<table summary="" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="50%" class="tdb" align="center">T.C. Kimlik No</td>
<td width="50%" class="tdb" align="center">Adı Soyadı</td>
</tr>
<tr>
<td align="center"><asp:label id="tc_kimlik_lbl" runat="server" /></td>
<td align="center"><asp:label id="adi_soyadi_lbl" runat="server" /></td>
</tr>
</tbody>
</table>
</anthem:Panel>
<anthem:TextBox ID="personel_id" runat="server" AutoUpdateAfterCallBack="True" ReadOnly="true" CssClass="inputtemiz">0</anthem:TextBox>
</td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb" rowspan="2">* Başlangıç Tarihi</td>
<td><asp:label runat="server" id="baslangic_lbl"/></td>
</tr>
<tr>
<td>
<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td width="20"><input type="checkbox" class="alignmid" id="checkbox3" name="checkbox3"/></td>
<td>
<select id="gun1" name="gun1" class="input3" disabled="disabled" runat="server">
<option>1</option>
<option>2</option>
<option>3</option>
<option>4</option>
<option>5</option>
<option>6</option>
<option>7</option>
<option>8</option>
<option>9</option>
<option>10</option>
<option>11</option>
<option>12</option>
<option>13</option>
<option>14</option>
<option>15</option>
<option>16</option>
<option>17</option>
<option>18</option>
<option>19</option>
<option>20</option>
<option>22</option>
<option>23</option>
<option>24</option>
<option>25</option>
<option>26</option>
<option>27</option>
<option>28</option>
<option>29</option>
<option>30</option>
<option>31</option>
</select>
<select id="ay1" name="ay1" class="input3" disabled="disabled" runat="server">
<option value="1">Ocak</option>
<option value="2">Şubat</option>
<option value="3">Mart</option>
<option value="4">Nisan</option>
<option value="5">Mayıs</option>
<option value="6">Haziran</option>
<option value="7">Temmuz</option>
<option value="8">Ağustos</option>
<option value="9">Eylül</option>
<option value="10">Ekim</option>
<option value="11">Kasım</option>
<option value="12">Aralık</option>
</select>
<select id="yil1" name="yil1" class="input3" disabled="disabled" runat="server">
<option>1930</option>
<option>1931</option>
<option>1932</option>
<option>1933</option>
<option>1934</option>
<option>1935</option>
<option>1936</option>
<option>1937</option>
<option>1938</option>
<option>1939</option>
<option>1940</option>
<option>1941</option>
<option>1942</option>
<option>1943</option>
<option>1944</option>
<option>1945</option>
<option>1946</option>
<option>1947</option>
<option>1948</option>
<option>1949</option>
<option>1950</option>
<option>1951</option>
<option>1952</option>
<option>1953</option>
<option>1954</option>
<option>1955</option>
<option>1956</option>
<option>1957</option>
<option>1958</option>
<option>1959</option>
<option>1960</option>
<option>1961</option>
<option>1962</option>
<option>1963</option>
<option>1964</option>
<option>1965</option>
<option>1966</option>
<option>1967</option>
<option>1968</option>
<option>1969</option>
<option>1970</option>
<option>1971</option>
<option>1972</option>
<option>1973</option>
<option>1974</option>
<option>1975</option>
<option>1976</option>
<option>1977</option>
<option>1978</option>
<option>1979</option>
<option>1980</option>
<option>1981</option>
<option>1982</option>
<option>1983</option>
<option>1984</option>
<option>1985</option>
<option>1986</option>
<option>1987</option>
<option>1988</option>
<option>1989</option>
<option>1990</option>
<option>1991</option>
<option>1992</option>
<option>1993</option>
<option>1994</option>
<option>1995</option>
<option>1996</option>
<option>1997</option>
<option>1998</option>
<option>1999</option>
<option>2000</option>
<option>2001</option>
<option>2002</option>
<option>2003</option>
<option>2004</option>
<option>2005</option>
<option>2006</option>
<option>2007</option>
<option>2008</option>
<option>2009</option>
<option>2010</option>
</select>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
<tr>
<td width="165" class="tdb" rowspan="2">Bitiş Tarihi</td>
<td><asp:label runat="server" id="bitis_lbl"/></td>
</tr>
<tr>
<td>
<table summary="" cellspacing="0" cellpadding="0" class="temiz">
<tbody>
<tr>
<td width="20"><input type="checkbox" class="alignmid" id="checkbox4" name="checkbox4"/></td>
<td>
<select id="gun2" name="gun2" class="input3" disabled="disabled" runat="server">
<option>1</option>
<option>2</option>
<option>3</option>
<option>4</option>
<option>5</option>
<option>6</option>
<option>7</option>
<option>8</option>
<option>9</option>
<option>10</option>
<option>11</option>
<option>12</option>
<option>13</option>
<option>14</option>
<option>15</option>
<option>16</option>
<option>17</option>
<option>18</option>
<option>19</option>
<option>20</option>
<option>22</option>
<option>23</option>
<option>24</option>
<option>25</option>
<option>26</option>
<option>27</option>
<option>28</option>
<option>29</option>
<option>30</option>
<option>31</option>
</select>
<select id="ay2" name="ay2" class="input3" disabled="disabled" runat="server">
<option value="1">Ocak</option>
<option value="2">Şubat</option>
<option value="3">Mart</option>
<option value="4">Nisan</option>
<option value="5">Mayıs</option>
<option value="6">Haziran</option>
<option value="7">Temmuz</option>
<option value="8">Ağustos</option>
<option value="9">Eylül</option>
<option value="10">Ekim</option>
<option value="11">Kasım</option>
<option value="12">Aralık</option>
</select>
<select id="yil2" name="yil2" class="input3" disabled="disabled" runat="server">
<option>1930</option>
<option>1931</option>
<option>1932</option>
<option>1933</option>
<option>1934</option>
<option>1935</option>
<option>1936</option>
<option>1937</option>
<option>1938</option>
<option>1939</option>
<option>1940</option>
<option>1941</option>
<option>1942</option>
<option>1943</option>
<option>1944</option>
<option>1945</option>
<option>1946</option>
<option>1947</option>
<option>1948</option>
<option>1949</option>
<option>1950</option>
<option>1951</option>
<option>1952</option>
<option>1953</option>
<option>1954</option>
<option>1955</option>
<option>1956</option>
<option>1957</option>
<option>1958</option>
<option>1959</option>
<option>1960</option>
<option>1961</option>
<option>1962</option>
<option>1963</option>
<option>1964</option>
<option>1965</option>
<option>1966</option>
<option>1967</option>
<option>1968</option>
<option>1969</option>
<option>1970</option>
<option>1971</option>
<option>1972</option>
<option>1973</option>
<option>1974</option>
<option>1975</option>
<option>1976</option>
<option>1977</option>
<option>1978</option>
<option>1979</option>
<option>1980</option>
<option>1981</option>
<option>1982</option>
<option>1983</option>
<option>1984</option>
<option>1985</option>
<option>1986</option>
<option>1987</option>
<option>1988</option>
<option>1989</option>
<option>1990</option>
<option>1991</option>
<option>1992</option>
<option>1993</option>
<option>1994</option>
<option>1995</option>
<option>1996</option>
<option>1997</option>
<option>1998</option>
<option>1999</option>
<option>2000</option>
<option>2001</option>
<option>2002</option>
<option>2003</option>
<option>2004</option>
<option>2005</option>
<option>2006</option>
<option>2007</option>
<option>2008</option>
<option>2009</option>
<option>2010</option>
</select>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">* Adı</td>
<td><input type="text" id="adi" name="adi" maxlength="500" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">* Sorumlu</td>
<td><input type="text" id="sorumlu" name="sorumlu" maxlength="150" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">* Telefon</td>
<td><input type="text" id="tel" name="tel" maxlength="13" class="input" runat="server"/></td>
</tr>
<tr>
<td class="tdb">Adres</td>
<td><asp:textbox runat="server" id="adres" TextMode="MultiLine" CssClass="input" Rows="4"/></td>
</tr>
</tbody>
</table>

<table summary="" cellspacing="0" cellpadding="0" class="margin5">
<tbody>
<tr>
<td width="165" class="tdb">Notlar</td>
<td><asp:textbox runat="server" id="notlar" TextMode="MultiLine" CssClass="input" Rows="4"/></td>
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

<table id="tbl_gonder" summary="" cellspacing="0" cellpadding="0" class="margin5" runat="server">
<tbody>
<tr>
<td align="center"><asp:Button ID="gonder" runat="server" Text="Güncelle" CssClass="input2" onclick="submit_Click" /></td>
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