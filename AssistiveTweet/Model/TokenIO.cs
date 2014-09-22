using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistiveTweet.Model
{
    public class TokenIO
    {
        public static IEnumerable<CoreTweet.Tokens> ReadSetting(){
            var tokenList = new List<CoreTweet.Tokens>();

            // Check instance
            if (Properties.Settings.Default.TokenCollection == null)
                Properties.Settings.Default.TokenCollection = new System.Collections.Specialized.StringCollection();

            var tokens = Properties.Settings.Default.TokenCollection;

            tokens.Remove(null);
            tokens.Remove("");

            string accessToken = null;
            foreach (var token in tokens)
            {
                if (string.IsNullOrWhiteSpace(token))
                    break;

                if (accessToken == null)
                {
                    accessToken = token;
                }
                else
                {
                    tokenList.Add(CoreTweet.Tokens.Create(App.ConsumerKey, App.ConsumerSecret, accessToken, token));
                    accessToken = null;
                }
            }
            return tokenList.AsEnumerable();
        }

        public static void WriteSetting(List<CoreTweet.Tokens> tokenList)
        {
            if (tokenList == null)
                return;

            foreach (var token in tokenList)
            {
//                System.Diagnostics.Debug.WriteLine(token.AccessToken);
                if(Properties.Settings.Default.TokenCollection.Contains(token.AccessToken))
                    continue;

                System.Diagnostics.Debug.WriteLine(token.AccessToken);
                Properties.Settings.Default.TokenCollection.Add(token.AccessToken);
                Properties.Settings.Default.TokenCollection.Add(token.AccessTokenSecret);
            }
            Properties.Settings.Default.Save();
        }

        public static void ResetSetting()
        {
            Properties.Settings.Default.TokenCollection = new System.Collections.Specialized.StringCollection();
            App.UsertokenList = new List<CoreTweet.Tokens>();
        }
    }
}
