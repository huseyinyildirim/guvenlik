<%@ Page Language="C#"%>
<script runat="server" type="text/C#">
protected void Page_Load(object sender, EventArgs e)
{

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) != null)
{
string SQL = "SELECT IFNULL(IF(kod<>'',CONCAT(urun,' - ', kod),NULL),urun) as urun FROM urun USE INDEX (onay) WHERE onay='1' and urun LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' OR kod LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' ORDER BY urun ASC";
System.Data.DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "urun");

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