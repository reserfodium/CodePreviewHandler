using System.Windows;

namespace Manager.Dialogs
{
    public partial class OkDialog : Window
    {
        public OkDialog(Window owner)
        {
            Owner = owner;

            InitializeComponent();
        }
    }
}
