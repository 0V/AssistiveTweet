using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AssistiveTweet.Model;
namespace AssistiveTweet.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            App.UsertokenList = TokenIO.ReadSetting().ToList();
            if (App.UsertokenList.Count > 0)
            {
                App.Usertoken = App.UsertokenList[0];
                UserInfomation = App.Usertoken.Account.VerifyCredentials();
            }
            //UserInfomation = App.Usertoken[0].Users.Show(screen_name => App.Usertoken[0].ScreenName);
        }
        private static TweetWindow _TweetWindow = new TweetWindow();
        public static TweetWindow TweetWindowInstance { get { return _TweetWindow; } set { _TweetWindow = value; } }
        private static LoginWindow _LoginWindow = new LoginWindow();
        public static LoginWindow LoginWindowInstance { get { return _LoginWindow; } set { _LoginWindow = value; } }
        private static TimeLineWindow _TimeLineWindow = new TimeLineWindow();
        public static TimeLineWindow TimeLineWindowInstance { get { return _TimeLineWindow; } set { _TimeLineWindow = value; } }
        /*
        private CoreTweet.Core.ListedResponse<CoreTweet.User> _UserInfomation;
        public CoreTweet.Core.ListedResponse<CoreTweet.User> UserInfomation
        {
            get { return _UserInfomation; }
            set
            {
                _UserInfomation = value;
                OnPropertyChanged("UserInfomation");
            }
        }*/

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
            get { return _TweetCommand ?? (_TweetCommand = new RelayCommand(PostTweet)); }
            set { _TweetCommand = value; }
        }

        private RelayCommand _TimeLineCommand;
        public RelayCommand TimeLineCommand
        {
            get
            {
                return _TimeLineCommand ?? (_TimeLineCommand = new RelayCommand(x =>
                    {
                        if (!TimeLineWindowInstance.IsLoaded)
                            TimeLineWindowInstance = new TimeLineWindow();
                        TimeLineWindowInstance.Show();
                    }));
            }
            set { _TimeLineCommand = value; }
        }
        
        private RelayCommand _CloseCommand;
        public RelayCommand CloseCommand
        {
            get { return _CloseCommand ?? (_CloseCommand = new RelayCommand(CloseWindow)); }
            set { _CloseCommand = value; }
        }

        private RelayCommand _ReOauthCommand;
        public RelayCommand ReOauthCommand
        {
            get { return _ReOauthCommand ?? (_ReOauthCommand = new RelayCommand(ReOauth)); }
            set { _ReOauthCommand = value; }
        }


        private RelayCommand _NonCommand;
        public RelayCommand NonCommand
        {
            get
            {
                return _NonCommand ?? (_NonCommand = new RelayCommand(_ =>
                {

                    var checkWin = new WpfMessageBox("まだ実装してないよ☆", MessageBoxButton.OK);
                    checkWin.ShowDialog();
                }));
            }
            set { _NonCommand = value; }
        }


        /*
        private void GridChange(object visibility) {
            if (SettingVisibility.CompareTo(visibility) == 0)
                System.Diagnostics.Debug.WriteLine(visibility.ToString());

            if (ToolsVisibility.Equals(visibility))
                System.Diagnostics.Debug.WriteLine("ahaga");

            System.Diagnostics.Debug.WriteLine(SettingVisibility.CompareTo(visibility));

            System.Diagnostics.Debug.WriteLine(ToolsVisibility.CompareTo(visibility));
            try
            {
                if (SettingVisibility.Equals(visibility))
                {

                }
                
            }
            catch(Exception e)
            {
                App.ShowErrorMessage(e);
            }
        }*/

        private void PostTweet(object parameter = null)
        {
            if (TwitterToken.IsAvailableToken())
                TweetWindowInstance.Show();
            else
                GetToken();
        }

        private void ReOauth(object parameter = null)
        {
            var checkWin = new WpfMessageBox("現在のアカウント情報を削除し、新しいアカウントを認証します",MessageBoxButton.YesNo);
            checkWin.ShowDialog();
            if (checkWin.Result == MessageBoxResult.Yes)
            {
                TokenIO.ResetSetting();
                GetToken();
            }
        }

        private void GetToken()
        {
            if (!LoginWindowInstance.IsLoaded)
            {
                LoginWindowInstance = new LoginWindow();
                App.Session = CoreTweet.OAuth.Authorize(App.ConsumerKey, App.ConsumerSecret);
                var uri = App.Session.AuthorizeUri;
                System.Diagnostics.Process.Start(uri.AbsoluteUri);
            }
            LoginWindowInstance.Show();
        }

        private void CloseWindow(object parameter = null)
        {
            var checkWin = new WpfMessageBox("アプリを終了します",MessageBoxButton.YesNo);
            checkWin.ShowDialog();
            if(checkWin.Result == MessageBoxResult.Yes)
                App.Current.Shutdown(0);
        }

        private void OpenTimeLine(object parameter = null)
        {
        }

    }
}
