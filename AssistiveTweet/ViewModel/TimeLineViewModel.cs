using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CoreTweet;
using CoreTweet.Streaming;
using CoreTweet.Core;
using System.Diagnostics;
using CoreTweet.Rest;
using System.Windows.Controls;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using AssistiveTweet.Model;

namespace AssistiveTweet.ViewModel
{
    public class TimeLineViewModel : ViewModelBase
    {

        private TwitterUserStream stream = new TwitterUserStream();

        private ObservableCollection<Status> _TimeLineList;
        public ObservableCollection<Status> TimeLineList
        {
            get { return _TimeLineList ?? (_TimeLineList = new ObservableCollection<Status>()); }
            set { _TimeLineList = value; }
        }

        private RelayCommand _StreamCommand;
        public RelayCommand StreamCommand
        {
            get { return _StreamCommand ?? (_StreamCommand = new RelayCommand(StartStream, x => !stream.IsRunning)); }
            set { _StreamCommand = value; }
        }

        private RelayCommand _StopStreamCommand;
        public RelayCommand StopStreamCommand
        {
            get { return _StopStreamCommand ?? (_StopStreamCommand = new RelayCommand(StopStream, x => stream.IsRunning)); }
            set { _StopStreamCommand = value; }
        }

        private RelayCommand _PostFaviriteCommand;
        public RelayCommand PostFaviriteCommand
        {
            get { return _PostFaviriteCommand ?? (_PostFaviriteCommand = new RelayCommand(PostFavorite)); }
            set { _PostFaviriteCommand = value; }
        }

        public void PostFavorite(object statusId)
        {
            if (TwitterFavorite.PostFavorite((string)statusId))
            {
            }
        }

        public void StartStream(object paramter = null)
        {
            if (TwitterToken.IsAvailableToken())
            {
                stream = new TwitterUserStream();

                stream.StartStreaming(App.Usertoken, (status) =>
                 App.Current.Dispatcher.Invoke(() => TimeLineList.Insert(0, status)));
                new WpfMessageBox("Streaming を開始しました", System.Windows.MessageBoxButton.OK).ShowDialog();
            }
            else
            {
                TwitterToken.GetToken();
            }
        }

        public void StopStream(object parameter = null)
        {
            if (stream.IsRunning)
                stream.StopStreaming();
            new WpfMessageBox("Streaming を停止しました", System.Windows.MessageBoxButton.OK).ShowDialog();
        }
    }
}
