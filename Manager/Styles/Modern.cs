using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Collections;

namespace Manager.Styles.Modern
{
    internal static class LocalExtensions
    {
        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            if (((FrameworkElement)templateFrameworkElement).TemplatedParent is Window window)
            {
                action(window);
            }
        }
    }

    public partial class Modern
    {
        void CloseButtonClick(object sender, RoutedEventArgs e) => sender.ForWindowFromTemplate(w => SystemCommands.CloseWindow(w));
    }
}