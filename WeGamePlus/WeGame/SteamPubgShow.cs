using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WeGamePlus.Bar_Plug.Comments;
using WeGamePlus.Bar_Plug.Models;

namespace WeGamePlus.WeGame
{
    public class SteamPubgShow : Form
    {
        private Image m1;
        private Image m2;
        private string clickUrl = "";
        private Point mousePoint;
        private IContainer components;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;

        public SteamPubgShow()
        {
            this.InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
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
            this.pictureBox2 = new PictureBox();
            this.pictureBox3 = new PictureBox();
            this.pictureBox4 = new PictureBox();
            this.pictureBox5 = new PictureBox();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            ((ISupportInitialize) this.pictureBox2).BeginInit();
            ((ISupportInitialize) this.pictureBox3).BeginInit();
            ((ISupportInitialize) this.pictureBox4).BeginInit();
            ((ISupportInitialize) this.pictureBox5).BeginInit();
            base.SuspendLayout();
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x2c4, 0x21);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox2.Cursor = Cursors.Hand;
            this.pictureBox2.Location = new Point(0, 0x21);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(0x2c4, 810);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
            this.pictureBox3.Cursor = Cursors.Hand;
            this.pictureBox3.Location = new Point(0x261, 0x312);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new Size(0x54, 0x18);
            this.pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new EventHandler(this.pictureBox3_Click);
            this.pictureBox3.MouseEnter += new EventHandler(this.pictureBox3_MouseEnter);
            this.pictureBox3.MouseLeave += new EventHandler(this.pictureBox3_MouseLeave);
            this.pictureBox4.Cursor = Cursors.Hand;
            this.pictureBox4.Location = new Point(0x291, 9);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new Size(14, 14);
            this.pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new EventHandler(this.pictureBox4_Click);
            this.pictureBox5.Cursor = Cursors.Hand;
            this.pictureBox5.Location = new Point(680, 9);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new Size(14, 14);
            this.pictureBox5.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new EventHandler(this.pictureBox3_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2c4, 0x34b);
            base.Controls.Add(this.pictureBox5);
            base.Controls.Add(this.pictureBox4);
            base.Controls.Add(this.pictureBox3);
            base.Controls.Add(this.pictureBox2);
            base.Controls.Add(this.pictureBox1);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(0x2c4, 0x34b);
            this.MinimumSize = new Size(0x2c4, 0x34b);
            base.Name = "SteamPubgShow";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "PUBG活动";
            base.Load += new EventHandler(this.SteamPubgShow_Load);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            ((ISupportInitialize) this.pictureBox2).EndInit();
            ((ISupportInitialize) this.pictureBox3).EndInit();
            ((ISupportInitialize) this.pictureBox4).EndInit();
            ((ISupportInitialize) this.pictureBox5).EndInit();
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
            Process.Start(this.clickUrl);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox3.Image = this.m1;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox3.Image = this.m2;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Minimized;
        }

        private void SteamPubgShow_Load(object sender, EventArgs e)
        {
            base.TopMost = true;
            this.clickUrl = XmlHelper.GetPublicXmlValue("url_steam", false);
            Size size2 = new Size(0x2c4, 0x34b);
            this.MinimumSize = size2;
            base.Size = this.MaximumSize = size2;
            this.pictureBox1.Image = UrlManage.SetImage("steamnewsup", "png");
            this.pictureBox2.Image = UrlManage.SetImage("steamnewsdown", "jpg");
            this.pictureBox3.Image = this.m2 = UrlManage.SetImage("steamnewsclose", "png");
            this.pictureBox4.Image = UrlManage.SetImage("steammin", "png");
            this.pictureBox5.Image = UrlManage.SetImage("steamclose", "png");
            this.m1 = UrlManage.SetImage("steamclosesel", "png");
            base.Icon = Icon.FromHandle(((Bitmap) UrlManage.SetImage("steamico", "ico")).GetHicon());
            base.TopMost = false;
        }
    }
}

