// Name: PreviewHandler.cs
// Author: Stephen Toub

using Microsoft.Win32;
using PreviewHandler.COMInterop;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PreviewHandler
{
    public abstract class PreviewHandler : IPreviewHandler, IPreviewHandlerVisuals, IOleWindow
    {
        #region Data

        private bool _showPreview;
        private PreviewHandlerControl _previewControl;
        private IntPtr _parentHwnd;
        private Rectangle _windowBounds;
        private IPreviewHandlerFrame _frame;

        #endregion

        protected PreviewHandler()
        {
            _previewControl = CreatePreviewHandlerControl();
            IntPtr forceCreation = _previewControl.Handle;
            _previewControl.BackColor = SystemColors.Window;
        }

        protected abstract PreviewHandlerControl CreatePreviewHandlerControl();

        private void InvokeOnPreviewThread(MethodInvoker d)
        {
            _previewControl.Invoke(d);
        }

        private void UpdateWindowBounds()
        {
            if (_showPreview)
            {
                InvokeOnPreviewThread(delegate ()
                {
                    SetParent(_previewControl.Handle, _parentHwnd);
                    _previewControl.Bounds = _windowBounds;
                    _previewControl.Visible = true;
                });
            }
        }

        #region Import

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetFocus(); 
        
        #endregion

        #region IPreviewHandler

        void IPreviewHandler.SetWindow(IntPtr hwnd, ref RECT rect)
        {
            _parentHwnd = hwnd;
            _windowBounds = rect.ToRectangle();
            UpdateWindowBounds();
        }

        void IPreviewHandler.SetRect(ref RECT rect)
        {
            _windowBounds = rect.ToRectangle();
            UpdateWindowBounds();
        }

        protected abstract void Load(PreviewHandlerControl c);

        void IPreviewHandler.DoPreview()
        {
            _showPreview = true;
            InvokeOnPreviewThread(delegate ()
            {
                try
                {
                    Load(_previewControl);
                }
                catch (Exception exc)
                {
                    _previewControl.Controls.Clear();
                    TextBox text = new TextBox();
                    text.ReadOnly = true;
                    text.Multiline = true;
                    text.Dock = DockStyle.Fill;
                    text.Text = exc.ToString();
                    _previewControl.Controls.Add(text);
                }
                UpdateWindowBounds();
            });
        }

        void IPreviewHandler.Unload()
        {
            _showPreview = false;
            InvokeOnPreviewThread(delegate ()
            {
                _previewControl.Visible = false;
                _previewControl.Unload();
            });
        }

        void IPreviewHandler.SetFocus()
        {
            InvokeOnPreviewThread(delegate () { _previewControl.Focus(); });
        }

        void IPreviewHandler.QueryFocus(out IntPtr phwnd)
        {
            IntPtr result = IntPtr.Zero;
            InvokeOnPreviewThread(delegate () { result = GetFocus(); });
            phwnd = result;
            if (phwnd == IntPtr.Zero) throw new Win32Exception();
        }

        uint IPreviewHandler.TranslateAccelerator(ref MSG pmsg)
        {
            if (_frame != null) return _frame.TranslateAccelerator(ref pmsg);
            const uint S_FALSE = 1;
            return S_FALSE;
        }

        void IPreviewHandlerVisuals.SetBackgroundColor(COLORREF color)
        {
            Color c = color.Color;
            InvokeOnPreviewThread(delegate () { _previewControl.BackColor = c; });
        }

        void IPreviewHandlerVisuals.SetTextColor(COLORREF color)
        {
            Color c = color.Color;
            InvokeOnPreviewThread(delegate () { _previewControl.ForeColor = c; });
        }

        void IPreviewHandlerVisuals.SetFont(ref LOGFONT plf)
        {
            Font f = Font.FromLogFont(plf);
            InvokeOnPreviewThread(delegate () { _previewControl.Font = f; });
        }

        #endregion

        #region IOleWindow

        void IOleWindow.GetWindow(out IntPtr phwnd)
        {
            phwnd = IntPtr.Zero;
            phwnd = _previewControl.Handle;
        }

        void IOleWindow.ContextSensitiveHelp(bool fEnterMode)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Registration

        [ComRegisterFunction]
        public static void Register(Type t)
        {
            if (t != null && t.IsSubclassOf(typeof(PreviewHandler)))
            {
                object[] attrs = (object[])t.GetCustomAttributes(typeof(PreviewHandlerAttribute), true);
                if (attrs != null && attrs.Length == 1)
                {
                    PreviewHandlerAttribute attr = attrs[0] as PreviewHandlerAttribute;
                    RegisterPreviewHandler(attr.Name, attr.Extension, t.GUID.ToString("B"), attr.AppId);
                }
            }
        }

        [ComUnregisterFunction]
        public static void Unregister(Type t)
        {
            if (t != null && t.IsSubclassOf(typeof(PreviewHandler)))
            {
                object[] attrs = (object[])t.GetCustomAttributes(typeof(PreviewHandlerAttribute), true);
                if (attrs != null && attrs.Length == 1)
                {
                    PreviewHandlerAttribute attr = attrs[0] as PreviewHandlerAttribute;
                    UnregisterPreviewHandler(attr.Extension, t.GUID.ToString("B"), attr.AppId);
                }
            }
        }

        protected static void RegisterPreviewHandler(string name, string extensions, string previewerGuid, string appId)
        {
            // Create a new prevhost AppID so that this always runs in its own isolated process
            using (RegistryKey appIdsKey = Registry.ClassesRoot.OpenSubKey("AppID", true))
            using (RegistryKey appIdKey = appIdsKey.CreateSubKey(appId))
            {
                appIdKey.SetValue("DllSurrogate", @"%SystemRoot%\system32\prevhost.exe", RegistryValueKind.ExpandString);
            }

            // Add preview handler to preview handler list
            using (RegistryKey handlersKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers", true))
            {
                handlersKey.SetValue(previewerGuid, name, RegistryValueKind.String);
            }

            // Modify preview handler registration
            using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
            using (RegistryKey idKey = clsidKey.OpenSubKey(previewerGuid, true))
            {
                idKey.SetValue("DisplayName", name, RegistryValueKind.String);
                idKey.SetValue("AppID", appId, RegistryValueKind.String);
                idKey.SetValue("DisableLowILProcessIsolation", 1, RegistryValueKind.DWord); // optional, depending on what preview handler needs to be able to do
            }

            foreach (string extension in extensions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Trace.WriteLine("Registering extension '" + extension + "' with previewer '" + previewerGuid + "'");

                // Set preview handler for specific extension
                using (RegistryKey extensionKey = Registry.ClassesRoot.CreateSubKey(extension))
                using (RegistryKey shellexKey = extensionKey.CreateSubKey("shellex"))
                using (RegistryKey previewKey = shellexKey.CreateSubKey("{8895b1c6-b41f-4c1c-a562-0d564250836f}"))
                {
                    previewKey.SetValue(null, previewerGuid, RegistryValueKind.String);
                }
            }
        }

        protected static void UnregisterPreviewHandler(string extensions, string previewerGuid, string appId)
        {
            foreach (string extension in extensions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Trace.WriteLine("Unregistering extension '" + extension + "' with previewer '" + previewerGuid + "'");
                using (RegistryKey shellexKey = Registry.ClassesRoot.OpenSubKey(extension + "\\shellex", true))
                {
                    shellexKey.DeleteSubKey("{8895b1c6-b41f-4c1c-a562-0d564250836f}");
                }
            }

            using (RegistryKey appIdsKey = Registry.ClassesRoot.OpenSubKey("AppID", true))
            {
                appIdsKey.DeleteSubKey(appId);
            }

            using (RegistryKey classesKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers", true))
            {
                classesKey.DeleteValue(previewerGuid);
            }
        }

        #endregion
    }
}