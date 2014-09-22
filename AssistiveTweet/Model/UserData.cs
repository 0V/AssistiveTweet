using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;

namespace AssistiveTweet.Model
{
    public class UserData
    {
        private Tokens _Token;
        public Tokens Token { 
            get{return _Token ??( _Token = new Tokens());}
            set { _Token = value; }
        }

        private OAuth.OAuthSession _Session;
        public OAuth.OAuthSession Session {
            get { return _Session ?? (_Session = OAuth.Authorize("", "")); }
            set { _Session = value; } 
        }
    }
}
