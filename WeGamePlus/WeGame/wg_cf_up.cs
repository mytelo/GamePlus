using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WeGamePlus.Bar_Plug.Comments;
using WeGamePlus.Bar_Plug.Models;

namespace WeGamePlus.WeGame
{
    public class wg_cf_up : Form
    {
        private Timer timer;
        private string clickUrl = "";
        private Image m1;
        private int defHeight;
        private bool isOpen;
        private IContainer components;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;

        public wg_cf_up()
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(wg_cf_up));
            this.pictureBox1 = new PictureBox();
            this.pictureBox2 = new PictureBox();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            ((ISupportInitialize) this.pictureBox2).BeginInit();
            base.SuspendLayout();
            this.pictureBox1.Cursor = Cursors.Hand;
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0xf4, 0x3a);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
            this.pictureBox2.Cursor = Cursors.Hand;
            this.pictureBox2.Location = new Point(0xbc, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(0x38, 0x2e);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xf4, 0x3a);
            base.Controls.Add(this.pictureBox2);
            base.Controls.Add(this.pictureBox1);
            base.FormBorderStyle = FormBorderStyle.None;
            //base.Icon = (Icon) manager.GetObject("");
            this.MaximumSize = new Size(0xf4, 0x3a);
            this.MinimumSize = new Size(0xf4, 0x3a);
            base.Name = "wg_cf_up";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "CF活动";
            base.Load += new EventHandler(this.wg_cf_up_Load);
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
                StatisticsHelp.Statistics(3, "5000", "");
                Process.Start(this.clickUrl);
            }
            catch
            {
                Process.Start("iexplore.exe", this.clickUrl);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void SetLocation(object sender, EventArgs e)
        {
            IntPtr hWnd = Win32Native.FindWindow("TWINCONTROL", "WeGame");
            if (hWnd != IntPtr.Zero)
            {
                Win32Native.RECT lpRect = new Win32Native.RECT();
                Win32Native.GetWindowRect(hWnd, ref lpRect);
                int num = lpRect.Bottom - lpRect.Top;
                if (this.defHeight != num)
                {
                    this.defHeight = num;
                    int num2 = (num - base.Height) - 0x21;
                    base.Top = num2;
                    base.Left = 500;
                    if (!this.isOpen)
                    {
                        this.isOpen = true;
                        try
                        {
                            base.WindowState = FormWindowState.Maximized;
                            Win32Native.SetParent(base.Handle, hWnd);
                        }
                        catch
                        {
                            this.isOpen = false;
                        }
                    }
                }
            }
            else
            {
                this.timer.Stop();
                this.isOpen = false;
                base.Close();
            }
        }

        private void wg_cf_up_Load(object sender, EventArgs e)
        {
            this.clickUrl = XmlHelper.GetPublicXmlValue("url_cf", false);
            if (!this.clickUrl.Contains("http"))
            {
                this.clickUrl = XmlHelper.GetPublicXmlValue("webip", true) + this.clickUrl;
            }
            this.pictureBox1.Image = this.m1 = UrlManage.SetImage("wg_cf_up", "png");
            this.pictureBox2.Image = UrlManage.SetImage("wg_cf_close", "png");
            this.timer = new Timer();
            this.timer.Interval = 10;
            this.timer.Tick += new EventHandler(this.SetLocation);
            this.timer.Start();
        }
    }
}

