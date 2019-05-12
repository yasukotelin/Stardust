using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stardust
{
    class ShortcutAlreadyExistException : Exception
    {
        public ShortcutAlreadyExistException(string message) : base(message)
        {
        }
    }


    class StartMenuRegister
    {
        private readonly string directoryPlacedShortcut =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Stardust");

        private string ApplicationPath { get; set; }


        private string ShorcutLinkFileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(ApplicationPath) + ".lnk";
            }
        }

        private string ShortcutPath
        {
            get
            {
                return Path.Combine(directoryPlacedShortcut, ShorcutLinkFileName);
            }
        }

        public StartMenuRegister(string applicationPath)
        {
            ApplicationPath = applicationPath;

            if (!File.Exists(ApplicationPath))
                throw new FileNotFoundException($"Application path {ApplicationPath} is not exist.");

            if (File.Exists(ShortcutPath))
                throw new ShortcutAlreadyExistException($"Shortcut {ShortcutPath} is already exist");
        }

        /// <summary>
        /// Register the application to start menu.
        /// </summary>
        public void Register()
        {
            CreateIfDirectoryNotExist();
            CreateShortcut();
        }

        private void CreateShortcut()
        {
            var shell = new IWshRuntimeLibrary.WshShell();
            var shorcutObj = (IWshRuntimeLibrary.IWshShortcut) shell.CreateShortcut(ShortcutPath);
            shorcutObj.TargetPath = ApplicationPath;
            shorcutObj.Save();

            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shorcutObj);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);
        }

        private void CreateIfDirectoryNotExist()
        {
            Directory.CreateDirectory(directoryPlacedShortcut);
        }
    }
}
