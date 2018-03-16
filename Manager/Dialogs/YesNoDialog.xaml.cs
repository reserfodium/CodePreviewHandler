using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Manager.Dialogs
{
    public partial class YesNoDialog : ModernDialog
    {
        public new bool DialogResult { get; private set; }

        public YesNoDialog(string message, Bitmap messageIcon, Window owner) : base(
            message: message,
            messageIcon: messageIcon,
            owner: owner)
        {
            InitializeComponent();

            Text.Text = Message;
            Img.Source = MessageIcon;
        }

        public new bool ShowDialog()
        {
            base.ShowDialog();
            return DialogResult;
        }

        #region Events

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Hide();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Hide();
        }

        #endregion
    }
}
