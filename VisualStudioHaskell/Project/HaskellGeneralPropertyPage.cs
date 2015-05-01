using Microsoft.VisualStudioTools.Project;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace Company.VisualStudioHaskell.Project
{
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "object is owned by VS")]
    [Guid(Constants.GeneralPropertyPageGuid)]
    public class HaskellGeneralPropertyPage : CommonPropertyPage
    {
        private readonly HaskellGeneralPropertyPageControl _pageControl;
        private readonly WpfPropertyPageControl<HaskellGeneralPropertyPageControl> _control;

        public HaskellGeneralPropertyPage()
        {
            _pageControl = new HaskellGeneralPropertyPageControl(this);
            _control = new WpfPropertyPageControl<HaskellGeneralPropertyPageControl>(_pageControl);
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
                return ProjectResources.GetString(ProjectResources.GeneralPropertyPageTitle);
            }
        }

        public override void Apply()
        {
        }

        public override void LoadSettings()
        {
            Loading = true;
            try
            {
                _pageControl.LoadSettings();
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
