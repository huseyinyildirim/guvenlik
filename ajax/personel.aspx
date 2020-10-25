<%@ Page Language="C#"%>
<script runat="server" type="text/C#">
protected void Page_Load(object sender, EventArgs e)
{

if (Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) != null)
{
string SQL = "SELECT CONCAT('(',tc_kimlik,')',' - ',adi_soyadi,' - (',IF(sertifika_durumu>0,'Silahlı','Silahsız'),')') as personel FROM personel WHERE tc_kimlik LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' OR adi_soyadi LIKE '%" + Class.Fonksiyonlar.SQLTemizle(Request.QueryString["q"]) + "%' ORDER BY adi_soyadi ASC";
System.Data.DataSet ds = Class.Fonksiyonlar.MySQL.DataSetGetir(SQL, "personel");

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
<%# Eval("personel") %>|
</ItemTemplate>
</asp:Repeater>