<%@ Page Language="C#"%>

<script runat="server" type="text/C#">
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Drawing.Bitmap ResimOlustur = new System.Drawing.Bitmap(180, 85);

        System.Drawing.Graphics GrafikOlustur = System.Drawing.Graphics.FromImage(ResimOlustur);
        GrafikOlustur.Clear(System.Drawing.Color.White);
        GrafikOlustur.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

        System.Drawing.Pen Kalem = new System.Drawing.Pen(System.Drawing.Color.BlueViolet, 2);

        GrafikOlustur.DrawLine(Kalem, 15, 5, 200, 200);
        GrafikOlustur.DrawLine(Kalem, -20, 5, 200, 200);
        GrafikOlustur.DrawLine(Kalem, 90, 5, 10, 20);
        GrafikOlustur.DrawLine(Kalem, 190, 5, 70, 70);

        System.Drawing.Font FontOlustur = new System.Drawing.Font("Trebuchet MS", 36, System.Drawing.FontStyle.Italic);


        string randomStr = string.Empty;
        int[] myIntArray = new int[5];
        int x;

        Random autoRand = new Random();
        for (x = 0; x < 5; x++)
        {
            myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
            randomStr += (myIntArray[x].ToString());
        }

        string captcha = randomStr; //Class.Fonksiyonlar.RastgeleSifreUret(5);
        Session.Add("captcha", captcha);
        GrafikOlustur.DrawString(captcha, FontOlustur, System.Drawing.Brushes.Black, 10, 10);

        /*
        System.Drawing.Imaging.EncoderParameters enkoder_parametresi;
        enkoder_parametresi = new System.Drawing.Imaging.EncoderParameters(1);
        enkoder_parametresi.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality,60L);
        ResimOlustur.Save(Response.OutputStream, EnkoderBul(System.Drawing.Imaging.ImageFormat.Jpeg), enkoder_parametresi);
        */

        System.Drawing.Graphics.FromImage(ResimOlustur).CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
        System.Drawing.Graphics.FromImage(ResimOlustur).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
        System.Drawing.Graphics.FromImage(ResimOlustur).PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
        System.Drawing.Graphics.FromImage(ResimOlustur).InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        ResimOlustur.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        Response.ContentType = "image/gif";

        FontOlustur.Dispose();
        GrafikOlustur.Dispose();
        ResimOlustur.Dispose();
    }

private System.Drawing.Imaging.ImageCodecInfo EnkoderBul(System.Drawing.Imaging.ImageFormat format)
{
    System.Drawing.Imaging.ImageCodecInfo[] resim_kodekleri = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders();
    foreach (System.Drawing.Imaging.ImageCodecInfo i in resim_kodekleri)
    {
        if (i.FormatID == format.Guid)
        {
            return i;
        }
    }
    return null;
}
</script>