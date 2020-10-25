<%@ Page Language="C#"%>
<script runat="server" type="text/C#">
protected void Page_Load(object sender, EventArgs e)
{

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) != null)
{
//string SQL = "SELECT CAST(CONCAT(urun,IF(durum=0,' - (ESKİ)',' - (YENİ)'),' - ',(SELECT adet FROM depo_urunler_toplamlar WHERE urun_id=depo_urunler.urun_id and durum=depo_urunler.durum))AS CHAR) as urun FROM depo_urunler WHERE (SELECT adet FROM depo_urunler_toplamlar WHERE urun_id=depo_urunler.urun_id and durum=depo_urunler.durum) > 0 and urun LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' GROUP BY urun_id,durum ORDER BY urun ASC";
//string SQL = "SELECT CAST(IF((SELECT adet FROM depo_urunler_toplamlar WHERE urun_id=depo_urunler.urun_id and durum=depo_urunler.durum GROUP BY urun_id,durum)>0,CONCAT(urun,IF(durum=0,' - (ESKİ)',' - (YENİ)'),' - ',(SELECT adet FROM depo_urunler_toplamlar WHERE urun_id=depo_urunler.urun_id and durum=depo_urunler.durum GROUP BY urun_id,durum),'|'),NULL)AS CHAR(510)) AS urun FROM depo_urunler WHERE urun LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' GROUP BY urun_id,durum ORDER BY urun ASC";
string SQL = "SELECT CAST(CONCAT(urun,IF(durum=0,' - (ESKİ)',' - (YENİ)'), ' - ',adet)AS CHAR(510)) as urun FROM depo_urunler_toplamlar WHERE bolge_id='0' and firma_id='0' and adet > 0 and urun LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' GROUP BY urun_id,durum ORDER BY urun ASC";
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