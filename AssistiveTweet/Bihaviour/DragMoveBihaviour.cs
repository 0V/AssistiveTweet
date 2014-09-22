using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AssistiveTweet.Behaviour
{

    /// <summary>

    /// ドラッグムーブを実現する添付ビヘイビア
    /// </summary>
    public static class DragMoveBehaviour
    {
        public static bool GetCanDragMove(UIElement elem)
        {
            return (bool)elem.GetValue(CanDragMoveProperty);
        }

        public static void SetCanDragMove(UIElement elem, bool val)
        {
            elem.SetValue(CanDragMoveProperty, val);
        }

        public static readonly DependencyProperty CanDragMoveProperty =
                DependencyProperty.RegisterAttached(
                        "CanDragMove",
                        typeof(bool),
                        typeof(DragMoveBehaviour),
                        new PropertyMetadata(OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //Propertyが変更されたときにMouseDownのイベントハンドラを登録しておく

            var target = sender as UIElement;
            if (target == null) return;
            if ((bool)e.NewValue == true)
            {
                target.MouseDown += OnMouseDown;
            }
            else
            {
                //do nothing
            }
        }

        private static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //押されたオブジェクトが所属するWindowを探してDragMove()する

            var obj = sender as DependencyObject;
            if (obj == null) return;
            if (e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Pressed)
            {
                getParentWindow(obj).DragMove();
            }
        }

        private static Window getParentWindow(DependencyObject current)
        {
            //引数currentを内部に持っているwinを探して返す
            foreach (Window win in App.Current.Windows)
            {
                if (win.IsAncestorOf(current))
                {
                    return win;
                }
            }
            throw new InvalidOperationException("Couldn't find window");
        }
    }
}