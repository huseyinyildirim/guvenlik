<%@ Page Language="C#"%>
<script runat="server" type="text/C#">
protected void Page_Load(object sender, EventArgs e)
{

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["bolge_id"]) != null && Class.Fonksiyonlar.SQLTemizle(Request.QueryString["firma_id"]) != null && Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) != null)
{
string SQL = "SELECT CAST(CONCAT(urun,IF(durum=0,' - (ESKİ)',' - (YENİ)'), ' - ',adet)AS CHAR(510)) as urun FROM depo_urunler_toplamlar WHERE bolge_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["bolge_id"]) + "' and firma_id='" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["firma_id"]) + "' and adet > 0 and urun LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' GROUP BY urun_id,durum ORDER BY urun ASC";
System.Data.DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "depo_urunler_toplamlar");

if (ds.Tables[0].Rows.Count > 0)
{
listele.DataSource = ds;
listele.DataBind();
}

}

}
</script>

<asp:Repeater ID="listele" runat="server">
<ItemTemplate>
<%#Eval("urun")%>|
</ItemTemplate>
</asp:Repeater>