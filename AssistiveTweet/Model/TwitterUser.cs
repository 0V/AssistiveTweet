using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistiveTweet.Model
{
    class TwitterUser
    {
        public static CoreTweet.UserResponse GetUserInfomation(CoreTweet.Tokens tokens)
        {
            if (tokens == null)
                return null;
            return tokens.Account.VerifyCredentials();
        }
    }
}
