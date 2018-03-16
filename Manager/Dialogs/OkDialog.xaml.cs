using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Manager.Dialogs
{
    public partial class OkDialog : ModernDialog
    {
        public OkDialog(string message, Bitmap messageIcon, Window owner) : base(
            message: message,
            messageIcon: messageIcon,
            owner: owner)
        {
            InitializeComponent();

            Text.Text = Message;
            Img.Source = MessageIcon;
        }

        public new void ShowDialog()
        {
            base.ShowDialog();
        }

        #region Events

        private void Button_Click(object sender, RoutedEventArgs e) => Hide();

        #endregion
    }
}
