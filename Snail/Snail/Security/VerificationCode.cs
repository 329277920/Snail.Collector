using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Security
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public sealed class VerificationCode
    {
        //#region 公共方法


        //public VerificationCode()
        //{
        //    this.AutoHotPixelColor = true;
        //    this.AutoLineColor = true;
        //    this.BackColor = Color.FromArgb(0, 230, 230, 230);
        //}

        ///// <summary>
        ///// 生成验证码
        ///// </summary>
        ///// <param name="code">验证码字符</param>
        ///// <returns>返回图片对象</returns>
        //public Image Generation(string code)
        //{
        //    // 创建位图对象
        //    Bitmap image = new Bitmap(this.Width, this.Height);

        //    // 创建GDI+画图对象
        //    Graphics g = Graphics.FromImage(image);

        //    // 设置图片背景颜色
        //    g.Clear(this.BackColor);

        //    this.DrawingLine(this.LineCount, g, image.Width, image.Height);

        //    this.DrawingHotPixel(this.HotPixelCount, image, image.Width, image.Height);

        //    int i = 0;

        //    int k1 = this._rand.Next(code.Length - 1);

        //    int k2 = this._rand.Next(code.Length - 1);

        //    foreach (var c in code)
        //    {
        //        this.DrawingCode(c, g, i++, code.Length, i == k1 || i == k2 ? this._rand.Next(this.MaxAngle) : 0);
        //    }

        //    return image;
        //}

        ///// <summary>
        ///// 生成验证码
        ///// </summary>
        ///// <param name="length">字符长度</param>
        ///// <param name="code">验证码字符</param>
        ///// <returns>返回图片对象</returns>
        //public Image Generation(int length, out string code)
        //{
        //    code = this.GetRandCodes(length);

        //    return this.Generation(code);
        //}

        //#endregion

        //#region 控制属性

        //private float _fontSize = 17;
        ///// <summary>
        ///// 获取或设置字体大小，默认为20
        ///// </summary>
        //public float FontSize
        //{
        //    get { return this._fontSize; }
        //    set { this._fontSize = value; }
        //}

        //private int _height = 39;

        ///// <summary>
        ///// 获取或设置验证码图片高度，默认为35
        ///// </summary>
        //public int Height
        //{
        //    get { return this._height; }
        //    set { this._height = value; }
        //}

        //private int _width = 76;
        ///// <summary>
        ///// 获取或设置验证码图片宽度，默认为120
        ///// </summary>
        //public int Width
        //{
        //    get { return _width; }
        //    set { _width = value; }
        //}


        //private int _lineCount = 5;
        ///// <summary>
        ///// 获取或设置干扰线数量（默认为25）
        ///// </summary>
        //public int LineCount
        //{
        //    get { return this._lineCount; }
        //    set { this._lineCount = value; }
        //}

        //private int _hotPixelCount = 25;
        ///// <summary>
        ///// 获取或设置噪点个数
        ///// </summary>
        //public int HotPixelCount
        //{
        //    get { return this._hotPixelCount; }
        //    set { this._hotPixelCount = value; }
        //}

        //private Color lineColor = Color.Black;
        ///// <summary>
        ///// 获取或设置干扰线颜色
        ///// </summary>
        //private Color LineColor
        //{
        //    get { return this.lineColor; }
        //    set { this.lineColor = value; }
        //}

        //private Color _hotPixelColor = Color.Black;
        ///// <summary>
        ///// 获取或设置噪点颜色
        ///// </summary>
        //public Color HotPixelColor
        //{
        //    get { return _hotPixelColor; }
        //    set { _hotPixelColor = value; }
        //}

        ///// <summary>
        ///// 获取或设置是否自动噪点颜色
        ///// </summary>
        //public bool AutoHotPixelColor { get; set; }

        ///// <summary>
        ///// 获取或设置是否自动干扰线颜色
        ///// </summary>
        //public bool AutoLineColor { get; set; }

        //private Color _backColor = Color.White;
        ///// <summary>
        ///// 获取或设置背景色（默认为白色）
        ///// </summary>
        //private Color BackColor
        //{
        //    get { return this._backColor; }
        //    set { this._backColor = value; }
        //}

        //private Color _beforeColor = Color.Black;
        ///// <summary>
        ///// 获取或设置前景色 
        ///// </summary>
        //public Color BeforeColor
        //{
        //    get { return _beforeColor; }
        //    set { _beforeColor = value; }
        //}

        //private Font _font;
        ///// <summary>
        ///// 获取或设置验证码字体
        ///// </summary>
        //public Font Font
        //{
        //    get { return this._font; }
        //    set { this._font = value; }
        //}

        //private int _maxAngle = 60;
        ///// <summary>
        ///// 获取或设置图像最大旋转度，默认为360度
        ///// </summary>
        //public int MaxAngle
        //{
        //    get { return this._maxAngle; }
        //    set { this._maxAngle = value; }
        //}

        //#endregion

        //#region 私有成员

        ///// <summary>
        ///// 私有随机数字生成器
        ///// </summary>
        //private Random _rand = new Random();

        ///// <summary>
        ///// 获得随机的字体
        ///// </summary>
        ///// <param name="strFonts"></param>
        ///// <returns></returns>
        //private Font GetRandFont(params string[] strFonts)
        //{
        //    if (strFonts == null)
        //    {
        //        strFonts = SystemFonts;
        //    }

        //    return new Font(strFonts[_rand.Next(0, strFonts.Length - 1)], this.FontSize);

        //}

        ///// <summary>
        ///// 获取随机字符
        ///// </summary>
        ///// <param name="length"></param>
        ///// <returns></returns>
        //public string GetRandCodes(int length)
        //{
        //    char[] result = new char[length];

        //    for (var i = 0; i < length; i++)
        //    {
        //        result[i] = EnglishAndNums[this._rand.Next(EnglishAndNums.Length - 1)];
        //    }

        //    return new string(result);
        //}

        ///// <summary>
        ///// 绘制干扰线
        ///// </summary>
        ///// <param name="lineCount"></param>
        ///// <param name="g"></param>
        ///// <param name="maxX"></param>
        ///// <param name="maxY"></param>
        //private void DrawingLine(int lineCount, Graphics g, int maxX, int maxY)
        //{
        //    if (lineCount <= 0)
        //    {
        //        return;
        //    }

        //    using (var pLine = new Pen(new SolidBrush(this.lineColor)))
        //    {
        //        for (var i = 0; i < lineCount; i++)
        //        {
        //            if (this.AutoLineColor)
        //            {
        //                pLine.Color = GetRandomColor();
        //            }
        //            g.DrawLine(pLine, this._rand.Next(maxX), this._rand.Next(maxY), this._rand.Next(maxX), this._rand.Next(maxY));
        //        }
        //    }
        //}

        ///// <summary>
        ///// 绘制噪点
        ///// </summary>
        ///// <param name="pixelCount"></param>
        ///// <param name="img"></param>
        ///// <param name="maxX"></param>
        ///// <param name="maxY"></param>
        //private void DrawingHotPixel(int pixelCount, Bitmap map, int maxX, int maxY)
        //{
        //    if (this.HotPixelCount <= 0)
        //    {
        //        return;
        //    }
        //    for (var i = 0; i < this.HotPixelCount; i++)
        //    {
        //        map.SetPixel(this._rand.Next(maxX), this._rand.Next(maxY), this.AutoHotPixelColor ? this.GetRandomColor() : this.HotPixelColor);
        //    }
        //}

        ///// <summary>
        ///// 绘制字符，并控制图像的旋转
        ///// </summary>
        ///// <param name="code"></param>
        ///// <param name="g"></param>
        ///// <param name="idx"></param>
        ///// <param name="length"></param>
        ///// <param name="angle">旋转度</param>
        //private void DrawingCode(char code, Graphics g, int idx, int length, int angle)
        //{
        //    var bc = this.GetRandomColor();

        //    // Brush brush = new SolidBrush(this.BeforeColor);

        //    Brush brush = new SolidBrush(bc);

        //    using (Bitmap img = new Bitmap((int)(Math.Floor((double)this.FontSize) + 5), this.Height))
        //    {
        //        using (Graphics gImg = Graphics.FromImage(img))
        //        {
        //            // 透明背景
        //            gImg.Clear(Color.FromArgb(0, 0, 0, 0));

        //            // var font = this.CreateFont(@"E:\项目\Snail\Snail.Win\bin\Debug\fonts\font1.ttf", this.FontSize, FontStyle.Bold, GraphicsUnit.Point,0);

        //            // gImg.DrawString(code.ToString(), this.Font == null ? this.GetRandFont(null) : this.Font, brush, 2, 2);

        //            gImg.DrawString(code.ToString(), this.Font == null ? this.GetRandFont(null) : this.Font, brush, 2, 2);

        //            //  gImg.DrawString(code.ToString(),font,brush,new RectangleF(1,gImg.))



        //            float x = this.GetCodeLeft(idx, length);

        //            // 旋转图像
        //            using (var rImg = RotateImg(img, angle))
        //            {
        //                // g.DrawImageUnscaled(rImg, idx * (int)(this.FontSize), 0, rImg.Width, rImg.Height);
        //                g.DrawImageUnscaled(rImg, (int)x, 0, rImg.Width, rImg.Height);
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// 获取随机的验证码位置
        ///// </summary>
        ///// <param name="idx"></param>
        ///// <param name="length"></param>
        ///// <returns></returns>
        //private float GetCodeLeft(int idx, int length)
        //{
        //    double offset = 0.2;

        //    float charWidth = this.FontSize + 2;

        //    if (idx == 0)
        //    {
        //        return 0;
        //        // return this._rand.Next(0, (int)(this.FontSize * offset));
        //    }

        //    if (idx == length - 1)
        //    {
        //        return this._rand.Next((int)(idx * charWidth) - (int)(charWidth * offset), (int)(idx * charWidth));
        //    }

        //    return this._rand.Next((int)(idx * charWidth) - (int)(charWidth * offset), (int)(idx * charWidth) + (int)(charWidth * offset));
        //}

        ///// <summary>
        ///// 控制图像的旋转（取自博客园：http://blog.csdn.net/liehuo123/article/details/5733116）
        ///// </summary>
        ///// <param name="b">需要旋转的图像</param>
        ///// <param name="angle">旋转度</param>
        ///// <returns>返回旋转后的图像</returns>
        //public Image RotateImg(Image b, int angle)
        //{

        //    angle = angle % 360;


        //    //弧度转换  

        //    double radian = angle * Math.PI / 180.0;

        //    double cos = Math.Cos(radian);

        //    double sin = Math.Sin(radian);


        //    //原图的宽和高  

        //    int w = b.Width;

        //    int h = b.Height;

        //    int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));

        //    int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));


        //    //目标位图  

        //    Bitmap dsImage = new Bitmap(W, H);

        //    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);

        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;

        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


        //    //计算偏移量  

        //    Point Offset = new Point((W - w) / 2, (H - h) / 2);


        //    //构造图像显示区域：让图像的中心与窗口的中心点一致  

        //    Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);

        //    Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);


        //    g.TranslateTransform(center.X, center.Y);

        //    g.RotateTransform(360 - angle);


        //    //恢复图像在水平和垂直方向的平移  

        //    g.TranslateTransform(-center.X, -center.Y);

        //    g.DrawImage(b, rect);


        //    //重至绘图的所有变换  

        //    g.ResetTransform();


        //    g.Save();

        //    g.Dispose();

        //    //保存旋转后的图片  

        //    b.Dispose();

        //    return dsImage;

        //}
        //#endregion

        //#region 初始化数据

        ///// <summary>
        ///// 存放系统颜色值
        ///// </summary>
        //private static List<Color> ColorList;

        ///// <summary>
        ///// 存放所有英文和数字字符（屏蔽掉0,1,l）
        ///// </summary>
        //private static char[] EnglishAndNums;

        ///// <summary>
        ///// 存放字体
        ///// </summary>
        //private static string[] SystemFonts;

        //static VerificationCode()
        //{
        //    ColorList = new List<Color>();
        //    foreach (var c in Enum.GetValues(typeof(KnownColor)))
        //    {
        //        ColorList.Add(Color.FromKnownColor((KnownColor)c));
        //    }



        //    EnglishAndNums = new char[] { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'P', 'R', 'S', 'T', 'X', 'Y' };

        //    // SystemFonts = new string[] { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
        //    SystemFonts = new string[] { "Verdana" };
        //}

        ///// <summary>
        ///// 获取一个系统随机颜色
        ///// </summary>
        //private Color GetRandomColor()
        //{
        //    return Color.FromArgb(this._rand.Next(0, 255), this._rand.Next(0, 255), this._rand.Next(0, 255));

        //    // return ColorList[this._rand.Next(ColorList.Count - 1)];
        //}

        //#endregion

        //public Font CreateFont(string fontFile, float fontSize)
        //{
        //    /*PrivateFontCollection 类允许应用程序安装现有字体的私有版本，而无需替换该字体的系统版本。例如，除系统使用的 Arial 字体外，GDI+ 还可以创建 Arial 字体的私有版本。PrivateFontCollection 还可以用于安装操作系统中不存在的字体。这种临时的字体安装不会影响系统安装的字体集合。若要查看已安装的字体集合，请使用 InstalledFontCollection 类。*/

        //    var pfc = new PrivateFontCollection();
        //    pfc.AddFontFile(fontFile);

        //    return new Font(pfc.Families[0], this.FontSize);
        //}

        //public Font CreateFont(string fontFile, float fontSize, FontStyle fontStyle, GraphicsUnit graphicsUnit, byte gdiCharSet)
        //{
        //    /*PrivateFontCollection 类允许应用程序安装现有字体的私有版本，而无需替换该字体的系统版本。例如，除系统使用的 Arial 字体外，GDI+ 还可以创建 Arial 字体的私有版本。PrivateFontCollection 还可以用于安装操作系统中不存在的字体。这种临时的字体安装不会影响系统安装的字体集合。若要查看已安装的字体集合，请使用 InstalledFontCollection 类。*/

        //    var pfc = new PrivateFontCollection();
        //    pfc.AddFontFile(fontFile);

        //    return new Font(pfc.Families[0], this.FontSize);

        //    return new Font(pfc.Families[0], fontSize, fontStyle, graphicsUnit, gdiCharSet);
        //}
    }
}
