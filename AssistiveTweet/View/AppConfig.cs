using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistiveTweet.View
{
    /// <summary>
    /// 各種設定を保存
    /// </summary>
    public static class AppConfig
    {
        public static bool TimeLineWindowIsTopMost = Properties.Settings.Default.TimeLineWindowIsTopMost; 
    }
}
