using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssistiveTweet
{
    /// <summary>
    /// CheckWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CheckWindow : Window
    {
        public MessageBoxResult Result { get; set; }
        public string Message { get; set; }

        public CheckWindow(string messageBoxText, MessageBoxButton button)
        {
            InitializeComponent();
            this.Message = messageBoxText;
            MessageText.Text = this.Message;
            SetButton(button);
        }

        private void SetButton(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    MakeButton("OK",0,3,2);
                    break;
                case MessageBoxButton.OKCancel:
                    MakeButton("OK",0,3);
                    MakeButton("Cancel",3,1);
                    break;
                case MessageBoxButton.YesNo:
                    MakeButton("Yes",0,3);
                    MakeButton("No",1,3);
                    break;
                case MessageBoxButton.YesNoCancel:
                    MakeButton("Yes",0,3);
                    MakeButton("No",1,3);
                    break;
                default:
                    break;
            }
        }

        private Button MakeButton(string buttonName, int culumn, int row, int culumnSpan = 1, int rowSpan = 1)
        {
            var button = new Button();
            button.Name = buttonName + "Button";
            button.Content = buttonName;
            button.Margin = new Thickness(20);
            button.Click += new RoutedEventHandler(Button_Click);
            button.SetValue(Grid.ColumnProperty, culumn);
            button.SetValue(Grid.RowProperty, row);
            button.SetValue(Grid.ColumnSpanProperty, culumnSpan);
            button.SetValue(Grid.RowSpanProperty, rowSpan);
            MessageBoxGrid.Children.Add(button);
            return button;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var name = button.Name;
                Result = name == "CancelButton" ? MessageBoxResult.Cancel :
                        name == "YesButton" ? MessageBoxResult.Yes :
                        name == "NoButton" ? MessageBoxResult.No :
                        name == "OKButton" ? MessageBoxResult.OK : MessageBoxResult.None;
                this.Close();
            }
        }
    }
}
