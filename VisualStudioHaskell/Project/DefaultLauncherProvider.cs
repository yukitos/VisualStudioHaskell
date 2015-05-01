using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudioTools.Project;
using Microsoft.VisualStudio.Shell;

namespace Company.VisualStudioHaskell.Project
{
    //[Export(typeof(IHaskellLauncherProvider))]
    class DefaultLauncherProvider : IHaskellLauncherProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Service _service;
        internal const string DefaultLauncherName = "Standard Haskell launcher";

        [ImportingConstructor]
        public DefaultLauncherProvider([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _service = serviceProvider.GetHaskellService();
        }

        public string Description
        {
            get
            {
                return ProjectResources.GetString(ProjectResources.DefaultLauncherDescription);
            }
        }

        public string LocalizedName
        {
            get
            {
                return ProjectResources.GetString(ProjectResources.DefaultLauncherName);
            }
        }

        public string Name
        {
            get
            {
                return DefaultLauncherName;
            }
        }

        public int SortPriority
        {
            get
            {
                return 0;
            }
        }

        public IProjectLauncher CreateLauncher(IHaskellProject project)
        {
            throw new NotImplementedException();
        }

        public IHaskellLauncherOptions GetLauncherOptions(IHaskellProject properties)
        {
            throw new NotImplementedException();
        }
    }
}
