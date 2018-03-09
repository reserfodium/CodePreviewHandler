using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodePreview
{
    static class Manager
    {
        public const string Name = "CodePreviewHandler";
        public const string Exts = ".cs;.vb;.vbs;.vbg;.bas;.cls;.frm;.html;.xhtml;.htm;.xht;.php;.js;.lua;.kml;.xaml;.xml;.sql;.phtml;.phar;";
        public const string AppId = "{3D0EBA26-D0F4-4247-A9EC-B3F3D9CB4B15}";
        public const string ProgId = "CodePreview.CodePreviewHandler";
        public const string Guid = "C1C35562-50D6-4751-ACB3-85539E6FCC98";

        public static Dictionary<List<string>, Language> Languages { get; } = new Dictionary<List<string>, Language>()
        {
            {
                new List<string> {
                    ".cs"
                },
                Language.CSharp
            },
            {
                new List<string> {
                    ".vb",
                    ".vbs",
                    ".bas",
                    ".cls",
                    ".frm",
                    ".vbg"
                },
                Language.VB
            },
            {
                new List<string> {
                    ".html",
                    ".htm",
                    ".xhtm",
                    ".xht"
                },
                Language.HTML
            },
            {
                new List<string> {
                    ".xml",
                    ".kml",
                    ".xaml"
                },
                Language.XML
            },
            {
                new List<string> {
                    ".sql"
                },
                Language.SQL
            },
            {
                new List<string> {
                    ".php",
                    ".phtml",
                    ".phar"
                },
                Language.PHP
            },
            {
                new List<string> {
                    ".js"
                },
                Language.JS
            },
            {
                new List<string> {
                    ".lua"
                },
                Language.Lua
            },
        };
    }
}
