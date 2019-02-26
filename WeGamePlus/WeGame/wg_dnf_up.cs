using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WeGamePlus.Bar_Plug.Comments;
using WeGamePlus.Bar_Plug.Models;

namespace WeGamePlus.WeGame
{
    public class wg_dnf_up : Form
    {
        private string clickUrl = "";
        private Image m1;
        private bool isOpen;
        private int defHeight;
        private Timer timer;
        private IContainer components;
        private PictureBox pictureBox1;

        public wg_dnf_up()
        {
            this.InitializeComponent();
            base.WindowState = FormWindowState.Minimized;
            Control.CheckForIllegalCrossThreadCalls = false;
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
            this.pictureBox1 = new PictureBox();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.pictureBox1.Cursor = Cursors.Hand;
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x13b, 0x2e);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x13b, 0x2e);
            base.Controls.Add(this.pictureBox1);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(0x13b, 0x2e);
            this.MinimumSize = new Size(0x13b, 0x2e);
            base.Name = "wg_dnf_up";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "DNF活动";
            base.Load += new EventHandler(this.wg_dnf_up_Load);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
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
                StatisticsHelp.Statistics(2, "6000", "");
                Process.Start(this.clickUrl);
            }
            catch
            {
                Process.Start("iexplore.exe", this.clickUrl);
            }
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
                    int num2 = (num - base.Height) - 30;
                    base.Top = num2;
                    base.Left = 380;
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

        private void wg_dnf_up_Load(object sender, EventArgs e)
        {
            this.clickUrl = XmlHelper.GetPublicXmlValue("url_dnf", false);
            if (!this.clickUrl.Contains("http"))
            {
                this.clickUrl = XmlHelper.GetPublicXmlValue("webip", true) + this.clickUrl;
            }
            this.pictureBox1.Image = this.m1 = UrlManage.SetImage("wg_dnf_up", "png");
            this.SetLocation(null, null);
        }
    }
}

