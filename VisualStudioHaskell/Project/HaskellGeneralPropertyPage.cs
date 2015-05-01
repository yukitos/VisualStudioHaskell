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
        private readonly HaskellGeneralPropertyPageControl _control;

        public HaskellGeneralPropertyPage()
        {
            _control = new HaskellGeneralPropertyPageControl(this);
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
        }
    }
}
