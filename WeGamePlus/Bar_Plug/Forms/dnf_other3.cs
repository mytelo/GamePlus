using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WeGamePlus.Bar_Plug.Comments;
using WeGamePlus.Bar_Plug.Models;

namespace WeGamePlus.Bar_Plug.Forms
{
    public class dnf_other3 : Form
    {
        private Timer timer;
        private string clickUrl = "";
        private Point p;
        private Size s;
        private IContainer components;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;

        public dnf_other3()
        {
            this.InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            base.WindowState = FormWindowState.Minimized;
            base.Shown += new EventHandler(this.MainFrom_Shown);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dnf_other3_Load(object sender, EventArgs e)
        {
            this.clickUrl = XmlHelper.GetPublicXmlValue("url_dnf", false);
            if (!this.clickUrl.Contains("http"))
            {
                this.clickUrl = XmlHelper.GetPublicXmlValue("webip", true) + this.clickUrl;
            }
            this.pictureBox1.Image = UrlManage.SetImage(XmlHelper.GetPublicXmlAttributeValue("DNF5", "image", false), "png");
            this.pictureBox2.Image = UrlManage.SetImage(XmlHelper.GetPublicXmlAttributeValue("DNF5", "hoverimage", false), "png");
            base.Icon = Icon.FromHandle(((Bitmap) UrlManage.SetImage("icon", "ico")).GetHicon());
            char[] separator = new char[] { ',' };
            string[] strArray = XmlHelper.GetPublicXmlAttributeValue("DNF5", "size1", false).Split(separator);
            char[] chArray2 = new char[] { ',' };
            string[] strArray2 = XmlHelper.GetPublicXmlAttributeValue("DNF5", "location1", false).Split(chArray2);
            this.s = new Size(int.Parse(strArray[0].ToString()), int.Parse(strArray[1].ToString()));
            this.p = new Point(int.Parse(strArray2[0].ToString()), int.Parse(strArray2[1].ToString()));
            this.timer.Interval = 0x3e8;
            this.timer.Tick += new EventHandler(this.tick);
            this.timer.Start();
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new PictureBox();
            this.pictureBox2 = new PictureBox();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            ((ISupportInitialize) this.pictureBox2).BeginInit();
            base.SuspendLayout();
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x9a, 0x73);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
            this.pictureBox2.Location = new Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(20, 20);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x9a, 0x73);
            base.Controls.Add(this.pictureBox2);
            base.Controls.Add(this.pictureBox1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "dnf_other3";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "dnf_other3";
            base.Load += new EventHandler(this.dnf_other3_Load);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            ((ISupportInitialize) this.pictureBox2).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void MainFrom_Shown(object sender, EventArgs e)
        {
            base.Visible = false;
            base.Shown -= new EventHandler(this.MainFrom_Shown);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                StatisticsHelp.Statistics(2, XmlHelper.GetPublicXmlAttributeValue("DNF5", "statistics", false), "");
                Process.Start(this.clickUrl);
            }
            catch
            {
                Process.Start("iexplore.exe", this.clickUrl);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.timer.Stop();
            base.Close();
        }

        public static void SuspendDrawing(Control parent)
        {
            Win32Native.SendMessage(parent.Handle, 11, false, 0);
        }

        private void tick(object sender, EventArgs e)
        {
            try
            {
                Win32Native.ClearMemory(base.Handle);
                IntPtr hWnd = Win32Native.FindWindow("地下城与勇士", "地下城与勇士");
                if (hWnd != IntPtr.Zero)
                {
                    Win32Native.RECT lpRect = new Win32Native.RECT();
                    Win32Native.GetWindowRect(hWnd, ref lpRect);
                    try
                    {
                        base.WindowState = FormWindowState.Maximized;
                        Win32Native.SetWindowPos(base.Handle, (int) hWnd, lpRect.Left + this.p.X, lpRect.Top + this.p.Y, this.s.Width, this.s.Height, 0x40);
                    }
                    catch
                    {
                    }
                }
                else
                {
                    this.timer.Stop();
                    base.Close();
                }
            }
            catch
            {
            }
        }
    }
}

