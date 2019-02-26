using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WeGamePlus.Bar_Plug.Comments;
using WeGamePlus.Bar_Plug.Models;

namespace WeGamePlus.WeGame
{
    public class cfShow : Form
    {
        private bool isOpen;
        private string clickUrl = "";
        private string imgName = "";
        private Point mousePoint;
        private IContainer components;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;

        public cfShow()
        {
            this.InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void cfShow_Load(object sender, EventArgs e)
        {
            this.clickUrl = XmlHelper.GetPublicXmlValue("url_cf", false);
            if (!this.clickUrl.Contains("http"))
            {
                this.clickUrl = XmlHelper.GetPublicXmlValue("webip", true) + this.clickUrl;
            }
            this.imgName = "rtiao";
            if (UrlManage.ImageList.Keys.Contains<string>(this.imgName))
            {
                this.pictureBox1.Image = UrlManage.ImageList[this.imgName];
            }
            else
            {
                this.pictureBox1.Image = UrlManage.GetImage(UrlManage.BaseImgDir + this.imgName + ".png");
                UrlManage.ImageList.Add(this.imgName, this.pictureBox1.Image);
            }
            this.pictureBox1.Image = UrlManage.SetImage("rtiao", "png");
            this.pictureBox2.Image = UrlManage.SetImage("cfshow", "png");
            this.pictureBox3.Image = UrlManage.SetImage("min", "png");
            this.pictureBox4.Image = UrlManage.SetImage("rclose", "png");
            Icon icon = Icon.FromHandle(((Bitmap)UrlManage.SetImage("icon", "ico")).GetHicon());
            base.Icon = icon;
            this.SetLocation();
            base.TopMost = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int width = radius;
            Rectangle rectangle = new Rectangle(rect.Location, new Size(width, width));
            GraphicsPath path1 = new GraphicsPath();
            path1.AddArc(rectangle, 180f, 90f);
            rectangle.X = rect.Right - width;
            path1.AddArc(rectangle, 270f, 90f);
            rectangle.Y = rect.Bottom - width;
            path1.AddArc(rectangle, 0f, 90f);
            rectangle.X = rect.Left;
            path1.AddArc(rectangle, 90f, 90f);
            path1.CloseFigure();
            return path1;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(cfShow));
            this.pictureBox4 = new PictureBox();
            this.pictureBox3 = new PictureBox();
            this.pictureBox2 = new PictureBox();
            this.pictureBox1 = new PictureBox();
            ((ISupportInitialize)this.pictureBox4).BeginInit();
            ((ISupportInitialize)this.pictureBox3).BeginInit();
            ((ISupportInitialize)this.pictureBox2).BeginInit();
            ((ISupportInitialize)this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.pictureBox4.Cursor = Cursors.Hand;
            this.pictureBox4.Location = new Point(0x1ac, 12);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new Size(0x10, 0x10);
            this.pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 15;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new EventHandler(this.pictureBox4_Click);
            this.pictureBox3.Cursor = Cursors.Hand;
            this.pictureBox3.Location = new Point(0x194, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new Size(0x10, 0x10);
            this.pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new EventHandler(this.pictureBox3_Click);
            this.pictureBox2.Cursor = Cursors.Hand;
            this.pictureBox2.Location = new Point(0, 40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(0x1cd, 0x227);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x1cd, 40);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new MouseEventHandler(this.pictureBox1_MouseMove);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(460, 0x24f);
            base.Controls.Add(this.pictureBox4);
            base.Controls.Add(this.pictureBox3);
            base.Controls.Add(this.pictureBox2);
            base.Controls.Add(this.pictureBox1);
            base.FormBorderStyle = FormBorderStyle.None;
            //base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "cfShow";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "每日推荐";
            base.Load += new EventHandler(this.cfShow_Load);
            base.Resize += new EventHandler(this.wg_Resize);
            ((ISupportInitialize)this.pictureBox4).EndInit();
            ((ISupportInitialize)this.pictureBox3).EndInit();
            ((ISupportInitialize)this.pictureBox2).EndInit();
            ((ISupportInitialize)this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                base.Top = Control.MousePosition.Y - this.mousePoint.Y;
                base.Left = Control.MousePosition.X - this.mousePoint.X;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Win32Native.SetWindowPos(base.Handle, -2, 0, 0, 0, 0, 3);
            try
            {
                StatisticsHelp.Statistics(3, "2000", "");
                Process.Start(this.clickUrl);
            }
            catch
            {
                Process.Start("iexplore.exe", this.clickUrl);
            }
            base.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void SetLocation()
        {
            if (((Enumerable.Where<Process>(Process.GetProcessesByName("tgp_daemon"), (Process.GetProcessesByName("tgp_daemon") == null) ? null : ((ProcessFunc.func != null) ? ProcessFunc.func : (ProcessFunc.func = ele => ele.MainWindowTitle == "WeGame"))) == null) ? null : Enumerable.Where<Process>(Process.GetProcessesByName("tgp_daemon"), (Process.GetProcessesByName("tgp_daemon") == null) ? null : ((ProcessFunc.func != null) ? ProcessFunc.func : (ProcessFunc.func = ele => ele.MainWindowTitle == "WeGame"))).FirstOrDefault<Process>()) != null)
            {
                if (!this.isOpen)
                {
                    this.isOpen = true;
                }
            }
            else
            {
                this.isOpen = false;
                base.Close();
            }
        }

        public void SetWindowRegion()
        {
            GraphicsPath roundedRectPath = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
            roundedRectPath = this.GetRoundedRectPath(rect, 10);
            base.Region = new Region(roundedRectPath);
        }

        private void wg_Resize(object sender, EventArgs e)
        {
            this.SetWindowRegion();
        }

        private sealed class ProcessFunc
        {
            public static readonly cfShow.ProcessFunc processFunc = new cfShow.ProcessFunc();
            public static Func<Process, bool> func;
        }
    }
}

