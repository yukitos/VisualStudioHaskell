using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Designer.Interfaces;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudioTools.Project;

using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Company.VisualStudioHaskell.Editor
{
    using Navigation;

    /// <summary>
    /// Factory for creating our editor object.
    /// </summary>
    [Guid(GuidList.guidVisualStudioHaskellEditorFactoryString)]
    public sealed class EditorFactory : CommonEditorFactory, IDisposable
    {
        public EditorFactory(VisualStudioHaskellPackage package)
            : base(package)
        {
        }

        public EditorFactory(VisualStudioHaskellPackage package, bool promptForEncoding)
            : base(package, promptForEncoding)
        {
        }

        public void Dispose()
        {
        }

        protected override void InitializeLanguageService(IVsTextLines textLines)
        {
            InitializeLanguageService(textLines, typeof(LanguageInfo).GUID);
        }
    }
}
