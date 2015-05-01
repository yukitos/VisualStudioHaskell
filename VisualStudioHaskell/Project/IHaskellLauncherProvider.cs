using Microsoft.VisualStudioTools.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.VisualStudioHaskell.Project
{
    public interface IHaskellLauncherProvider
    {
        IHaskellLauncherOptions GetLauncherOptions(IHaskellProject properties);

        string Name
        {
            get;
        }

        string LocalizedName
        {
            get;
        }

        string Description
        {
            get;
        }

        int SortPriority
        {
            get;
        }

        IProjectLauncher CreateLauncher(IHaskellProject project);
    }
}
