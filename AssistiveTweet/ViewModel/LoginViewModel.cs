using CoreTweet;
using System.Windows;
using System.Windows.Controls;
namespace AssistiveTweet.ViewModel
{
    public class LoginViewModel
    {
        private RelayCommand _PinRunCommand;
        public RelayCommand PinRunCommand
        {
            get { return _PinRunCommand ?? (_PinRunCommand = new RelayCommand(PinRun)); }
            set { _PinRunCommand = value; }
        }

        private void PinRun(object pinBox)
        {
            var text = (TextBox)pinBox;

            if (string.IsNullOrWhiteSpace(text.Text))
            {
                new WpfMessageBox("PIN を入力してください", System.Windows.MessageBoxButton.OK).ShowDialog();
                return;
            }

            try
            {
                var pin = text.Text;
                var token = OAuth.GetTokensAsync(App.Session, pin);
                App.UsertokenList.Insert(0, token.Result);
                Model.TokenIO.WriteSetting(App.UsertokenList);
                App.Usertoken = App.UsertokenList[0];
                new WpfMessageBox("認証に成功しました", System.Windows.MessageBoxButton.OK).ShowDialog();
                MainViewModel.TimeLineWindowInstance.Close();
                MainViewModel.TimeLineWindowInstance = new TimeLineWindow();
                Window.GetWindow(text).Close();
            }
            catch
            {
                new WpfMessageBox("認証に失敗しました\n\n再度認証してください", System.Windows.MessageBoxButton.OK).ShowDialog();
                Window.GetWindow(text).Close();
            }
        }

    }
}
