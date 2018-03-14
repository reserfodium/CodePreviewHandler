using System.Windows;

namespace Manager.Dialogs
{
    public class ModernDialog : Window
    {
        public ModernDialog(Window owner)
        {
            this.Owner = owner;
            Init();
        }

        private void Init()
        {
            this.Style = this.FindResource("ModernDialog") as Style;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
