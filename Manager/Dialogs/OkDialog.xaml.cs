using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Manager.Dialogs
{
    public partial class OkDialog : Window
    {
        public OkDialog(Window owner)
        {
            Owner = owner;

            InitializeComponent();
        }
        
        #region Functions

        public bool ShowDialog(Bitmap bitmap, string text)
        {
            Img.Source = LoadBitmap(bitmap);
            Text.Text = text;

            ShowDialog();

            return true;
        }

        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }

        #endregion

        #region Events

        private void Button_Click(object sender, RoutedEventArgs e) => Hide();

        #endregion
    }
}
