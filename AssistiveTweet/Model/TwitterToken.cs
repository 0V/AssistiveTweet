using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet.Rest;
using CoreTweet;
using System.Diagnostics;
using AssistiveTweet.ViewModel;
namespace AssistiveTweet.Model
{
    class TwitterToken
    {
        public static void GetToken(UserData token)
        {
            token.Session = OAuth.Authorize(App.ConsumerKey, App.ConsumerSecret);
            var url = token.Session.AuthorizeUri;
            Process.Start(url.ToString());
        }

        public static bool IsValidToken(Tokens token)
        {
            return token != null && !string.IsNullOrEmpty(token.AccessTokenSecret);
        }

        /// <summary>
        /// 使用可能な Token が App.Usertoken にセットされているかを返します
        /// </summary>
        /// <returns></returns>
        public static bool IsAvailableToken()
        {
            //            App.Usertoken = CPConverter.TokensReader();
            if (App.Usertoken == null && App.UsertokenList == null)
                return false;
            else if (App.Usertoken == null && App.UsertokenList.Count < 1)
                return false;
            else if (App.UsertokenList.Count > 0)
            {
                App.Usertoken = App.UsertokenList[0];
            }
            return true;
        }
        
        public static void GetToken()
        {
            if (!MainViewModel.LoginWindowInstance.IsLoaded)
            {
                MainViewModel.LoginWindowInstance = new LoginWindow();
                App.Session = CoreTweet.OAuth.Authorize(App.ConsumerKey, App.ConsumerSecret);
                var uri = App.Session.AuthorizeUri;
                System.Diagnostics.Process.Start(uri.AbsoluteUri);
            }
            MainViewModel.LoginWindowInstance.Show();
        }



    }
}
