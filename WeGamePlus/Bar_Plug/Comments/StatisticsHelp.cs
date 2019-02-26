using System;
using System.Drawing;
using WeGamePlus.Bar_Plug.Models;

namespace WeGamePlus.Bar_Plug.Comments
{
    public class StatisticsHelp
    {
        public static string ClolorStatistics(string okClolor, string currColor, Point p)
        {
            StatisticModel objectToSerialize = new StatisticModel {
                agentName = UrlManage.agentName,
                fromWhere = UrlManage.ProcessName,
                gameType = 0,
                otherInfo = okClolor,
                otherInfo2 = "2",
                otherInfo3 = currColor,
                ip = p.X + "*" + p.Y,
                timestamp = TimeHelp.GetTimeStamp(DateTime.Now, 13)
            };
            return new HttpClient("http://clientbi.gz.1251415748.clb.myqcloud.com/baozang/launcher?data=" + ComHelp.Serialize(objectToSerialize)).GetString();
        }

        public static string Statistics(int gameType = 0, string clickType = "", string startType = "") => 
            "1";
    }
}

