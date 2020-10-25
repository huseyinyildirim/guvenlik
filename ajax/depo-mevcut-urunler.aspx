<%@ Page Language="C#"%>
<script runat="server" type="text/C#">
protected void Page_Load(object sender, EventArgs e)
{

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) != null)
{
//string SQL = "SELECT urun_id,CAST(CONCAT(urun,IF(durum=0,' - (ESKİ)',' - (YENİ)'))AS CHAR(510)) as urun FROM depo_urunler WHERE (SELECT adet FROM depo_urunler_toplamlar WHERE urun_id=depo_urunler.urun_id and durum=depo_urunler.durum) > 0 and urun LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' GROUP BY urun_id,durum";
string SQL = "SELECT urun_id,CAST(CONCAT(urun,IF(durum=0,' - (ESKİ)',' - (YENİ)'))AS CHAR(510)) as urun FROM depo_urunler urun WHERE urun LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' GROUP BY urun_id,durum";
System.Data.DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "depo_urunler");

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
<%# Eval("urun") %>|
</ItemTemplate>
</asp:Repeater>