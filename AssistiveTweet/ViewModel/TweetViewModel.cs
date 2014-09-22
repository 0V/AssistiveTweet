using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AssistiveTweet.Model;
namespace AssistiveTweet.ViewModel
{
    class TweetViewModel : ViewModelBase
    {

        public TweetViewModel()
        {
            if(TwitterToken.IsAvailableToken())
                UserInfomation = TwitterUser.GetUserInfomation(App.Usertoken);
        }

        public bool IsTweeting = false;

        private string _TweetText = "";
        public string TweetText
        {
            get { return _TweetText; }
            set
            {
                _TweetText = value;
                OnPropertyChanged("TweetText");
            }
        }  

        private CoreTweet.UserResponse _UserInfomation;
        public CoreTweet.UserResponse UserInfomation
        {
            get { return _UserInfomation; }
            set
            {
                _UserInfomation = value;
                OnPropertyChanged("UserInfomation");
            }
        }

        private RelayCommand _TweetCommand;
        public RelayCommand TweetCommand
        {
            get
            {
                return _TweetCommand ?? (_TweetCommand = new RelayCommand(Tweet, x =>
                {
                    if (string.IsNullOrWhiteSpace(TweetText) || IsTweeting)
                        return false;
                    return true;
                }));
            }
            set { _TweetCommand = value; }
        }

        private void Tweet(object parameter = null)
        {
            if (!string.IsNullOrWhiteSpace(TweetText))
            {
                try
                {
                    IsTweeting = true;
                    App.Usertoken.Statuses.Update(status => TweetText);
                    TweetText = "";
//                    MainViewModel.TweetWindowInstance.Hide();
                }
                catch(Exception e)
                {
                    App.ShowErrorMessage(e);
                }
                finally
                {
                    IsTweeting = false;
                }
            }
        }

    }
}
