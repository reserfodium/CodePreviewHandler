using System;
using System.Windows;

namespace Manager.Styles
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