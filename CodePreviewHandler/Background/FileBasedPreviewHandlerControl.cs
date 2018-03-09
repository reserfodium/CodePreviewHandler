// Name: FileBasedPreviewHandlerControl.cs
// Author: Stephen Toub

using System;
using System.IO;
using PreviewHandler.COMInterop;

namespace PreviewHandler
{
    public abstract class FileBasedPreviewHandlerControl : PreviewHandlerControl
    {
        protected FileInfo hFile;

        public override void Load(FileInfo file)
        {
            hFile = file;
            DoPreview();
        }

        protected abstract void DoPreview();
    }
}
