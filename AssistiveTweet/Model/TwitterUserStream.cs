using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using CoreTweet.Core;
using CoreTweet.Rest;
using CoreTweet.Streaming;
using CoreTweet.Streaming.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;


namespace AssistiveTweet.Model
{
    class TwitterUserStream
    {

        private bool _IsRunninng = false;
        public bool IsRunning { get { return _IsRunninng; } private set {_IsRunninng = value;} }

        public IDisposable streaming { get; set; }

        public void StopStreaming()
        {
            streaming.Dispose();
            IsRunning = false;
        }

        /// <summary>
        /// UserStream に接続します。
        /// </summary>
        /// <param name="token">Tokens</param>
        /// <param name="action"></param>
        public void StartStreaming(Tokens token, Action<Status> action, StreamingType type = StreamingType.User)
        {
            if (IsRunning)
                return;

            IsRunning = true;
            var streamRx = token.Streaming.StartObservableStream(type).Publish();

            Action<StatusMessage> getStatus = (message) =>
            {
                var status = (message as StatusMessage).Status;

                status.Source = Regex.Replace(status.Source, @"^(<.*>)(.*)(<.*>)$", "$2");
                action(status);
            };
            streamRx.OfType<StatusMessage>().Subscribe(getStatus);
            streaming = streamRx.Connect();
        }
    }
}
