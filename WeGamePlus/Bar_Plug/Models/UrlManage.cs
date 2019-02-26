using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WeGamePlus.Bar_Plug.Models
{
    public class UrlManage
    {
        public static string BaseHttp = "http://bang-plugs.oss-cn-beijing.aliyuncs.com/";
        public static string BaseImgDir = (BaseHttp + "IMG/");
        public static string BaseSetDir = (BaseHttp + "SET/");
        public static Dictionary<string, Image> ImageList = new Dictionary<string, Image>();
        public static List<UserForm> __FormsList = new List<UserForm>();
        public static bool lolBlock = false;
        public static bool cfBlock = false;
        public static bool dnfBlock = false;
        public static bool nzBlock = false;
        public static bool weGameBlock = false;

        public static Image GetImage(string url)
        {
            try
            {
                return Image.FromStream(new HttpClient(url).GetStream());
            }
            catch
            {
                return null;
            }
        }

        public static bool Init()
        {
            try
            {
                string[] commandLineArgs = Environment.GetCommandLineArgs();
                if (commandLineArgs.Length > 2)
                {
                    ProcessName = commandLineArgs[1].ToString() + commandLineArgs[2].ToString();
                    agentName = commandLineArgs[2].ToString();
                    lolBlock = commandLineArgs.Contains<string>("lol");
                    cfBlock = commandLineArgs.Contains<string>("cf");
                    dnfBlock = commandLineArgs.Contains<string>("dnf");
                    nzBlock = commandLineArgs.Contains<string>("nz");
                    weGameBlock = commandLineArgs.Contains<string>("wegame");
                }
                else
                {
                    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                    agentName = versionInfo.CompanyName;
                    ProcessName = versionInfo.CompanyName + versionInfo.LegalTrademarks;
                }
                dllSettingDoc = XDocument.Load(BaseSetDir + "set.xml");
                id_dllSettingDoc = XDocument.Load(BaseSetDir + ProcessName + "_set.xml");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static StringBuilder sb = new StringBuilder();
        public static Image SetImage(string imgName, string exc = "png")
        {
            if (ImageList.Keys.Contains<string>(imgName))
            {
                return ImageList[imgName];
            }
            Image image = GetImage(BaseImgDir + imgName + "." + exc);
            ImageList.Add(imgName, image);
            sb.Append(BaseImgDir + imgName + "." + exc+"\r\n");
            return image;
        }

        public static string ProcessName { get; set; }

        public static string agentName { get; set; }

        public static XDocument dllSettingDoc { get; set; }

        public static XDocument id_dllSettingDoc { get; set; }
    }
}

