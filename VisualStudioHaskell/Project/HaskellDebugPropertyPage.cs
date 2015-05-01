using Microsoft.VisualStudioTools.Project;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Company.VisualStudioHaskell.Project
{
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "object is owned by VS")]
    [Guid(Constants.DebugPropertyPageGuid)]
    public class HaskellDebugPropertyPage : CommonPropertyPage
    {
        private readonly HaskellDebugPropertyPageControl _pageControl;
        private readonly WpfPropertyPageControl<HaskellDebugPropertyPageControl> _control;

        public HaskellDebugPropertyPage()
        {
            _pageControl = new HaskellDebugPropertyPageControl(this);
            _control = new WpfPropertyPageControl<HaskellDebugPropertyPageControl>(_pageControl);
        }

        public override Control Control
        {
            get
            {
                return _control;
            }
        }

        public override string Name
        {
            get
            {
                return ProjectResources.GetString(ProjectResources.DebugPropertyPageTitle);
            }
        }

        public override void Apply()
        {
            //Project.SetProjectProperty(Constants.LaunchProvider, _pageControl.CurrentLauncher);
            //_pageControl.SaveSettings();

            //IsDirty = false;
        }

        public override void LoadSettings()
        {
            Loading = true;
            try
            {
                //_pageControl.LoadSettings();
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
