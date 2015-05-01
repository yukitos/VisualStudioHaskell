using Microsoft.VisualStudioTools.Project;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Company.VisualStudioHaskell.Project
{
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "object is owned by VS")]
    [Guid("A69FDF34-BAAA-43ED-B4DF-F47F4D4D845F")]
    public class HaskellDebugPropertyPage : CommonPropertyPage
    {
        private readonly HaskellDebugPropertyPageControl _control;

        public HaskellDebugPropertyPage()
        {
            _control = new HaskellDebugPropertyPageControl(this);
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
        }

        public override void LoadSettings()
        {
        }

    }
}
