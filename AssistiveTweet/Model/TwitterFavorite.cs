using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
namespace AssistiveTweet.Model
{
    public class TwitterFavorite
    {
        public static bool IsFavorited(Status status)
        {
            return true;
        }

        public static bool PostFavorite(Status status)
        {
            try
            {
                App.Usertoken.Favorites.Create(id => status.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool PostFavorite(string statusId)
        {
            try
            {
                App.Usertoken.Favorites.Create(id => statusId);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
