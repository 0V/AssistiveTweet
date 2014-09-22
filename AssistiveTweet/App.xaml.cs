using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CoreTweet;
namespace AssistiveTweet
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// API Key
        /// </summary>
        public static string ConsumerKey
        {
            get { return "API Key"; }
        }

        /// <summary>
        /// API Secret
        /// </summary>
        public static string ConsumerSecret
        {
            get { return "API Secret"; }
        }

        public static OAuth.OAuthSession Session { get; set; }

        public static List<Tokens> UsertokenList { get; set; }
        public static Tokens Usertoken { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += ((_sender, _e) =>
                ShowErrorMessage((Exception)_e.ExceptionObject));
        }

        public static void ShowErrorMessage(Exception e)
        {
            var twitterException = e as TwitterException;
            if (twitterException != null)
            {
                if (twitterException.Message != null)
                    MessageBox.Show(string.Format("Twitter のエラーが発生しました: \n{0}", twitterException.Message), "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show("Twitter のエラーが発生しました", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("エラーが発生しました:\n" + e.Message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*
     [global::System.Configuration.UserScopedSettingAttribute()]
        public global::System.Collections.Generic.List<CoreTweet.Tokens> UserTokens
        {
            get
            {
                return ((global::System.Collections.Generic.List<CoreTweet.Tokens>)(this["UserTokens"]));
            }
            set
            {
                this["UserTokens"] = value;
            }
        }
         */
    }
}
