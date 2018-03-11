using System.Windows;

namespace Manager.Dialogs.InstallationDialog
{
    public enum Installation
    {
        ForAllUsers,
        ForCurrentUser
    }

    public partial class InstallationDialog : Window
    {
        public new Installation DialogResult
        {
            get;
            private set;
        }

        public InstallationDialog(Window owner)
        {
            Owner = owner;

            InitializeComponent();
        }

        #region Functions

        private void CloseDialog(Installation i)
        {
            DialogResult = i;
            Hide();
        }

        #endregion

        #region Events

        private void InstallForAllUsersButton_Click(object sender, RoutedEventArgs e)
            => CloseDialog(Installation.ForAllUsers);

        private void InstallForCurrentUserButton_Click(object sender, RoutedEventArgs e)
            => CloseDialog(Installation.ForCurrentUser);

        #endregion
    }
}
