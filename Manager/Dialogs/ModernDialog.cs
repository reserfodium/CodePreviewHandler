using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Manager.Dialogs
{
    public class ModernDialog : Window
    {
        #region Properties

        /* MessageIcon */

        protected BitmapSource _MessageIcon;
        public BitmapSource MessageIcon
        {
            get { return _MessageIcon; }
        }

        /* Message */

        protected string _Message;
        public string Message
        {
            get { return _Message; }
        }

        #endregion

        public ModernDialog(string message, Bitmap messageIcon, Window owner)
        {
            this._Message = message;
            this._MessageIcon = LoadBitmap(messageIcon);

            this.Style = this.FindResource("ModernDialog") as Style;

            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Owner = owner;
        }

        #region Functions

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
    }
}
