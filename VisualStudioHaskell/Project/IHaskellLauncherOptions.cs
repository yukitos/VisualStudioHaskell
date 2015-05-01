using Microsoft.VisualStudioTools.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company.VisualStudioHaskell.Project
{
    public interface IHaskellLauncherOptions
    {
        void SaveSettings();

        void LoadSettings();

        void ReloadSettings(string settingName);

        event EventHandler<DirtyChangedEventArgs> DirtyChanged;

        Control Control
        {
            get;
        }
    }
}
