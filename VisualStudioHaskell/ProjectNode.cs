using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudioTools.Project;
using Company.VisualStudioHaskell.Project;
using Microsoft.Build.Execution;

namespace Company.VisualStudioHaskell
{
    class ProjectNode : CommonProjectNode, IHaskellProject
    {
        private VisualStudioHaskellPackage _package;

        public ProjectNode(VisualStudioHaskellPackage package)
            : base(package, Utilities.GetImageList(typeof(ProjectNode).Assembly.GetManifestResourceStream(Constants.ProjectImageList)))
        {
            _package = package;
        }

        #region Abstract property implementation (incomplete)

        public override Guid ProjectGuid
        {
            get { return GuidList.guidVisualStudioHaskellProjectFactory; }
        }
        public override string ProjectType
        {
            get { return "Haskell Project"; }
        }
        internal override string IssueTrackerUrl
        {
            get { return ""; }
        }
        protected override Stream ProjectIconsImageStripStream
        {
            get { return typeof(ProjectNode).Assembly.GetManifestResourceStream("Company.VisualStudioHaskell.Project.Resources.imagelis.bmp"); }
        }

        #endregion

        #region Abstract method implementation (incomplete)

        public override Type GetProjectFactoryType()
        {
            return typeof(ProjectFactory);
        }

        public override Type GetEditorFactoryType()
        {
            return typeof(Editor.EditorFactory);
        }

        public override string GetProjectName()
        {
            return "";
        }

        public override string GetFormatList()
        {
            return "";
        }

        public override Type GetGeneralPropertyPageType()
        {
            return null;
        }

        public override Type GetLibraryManagerType()
        {
            return typeof(Navigation.HaskellLibraryManager);
        }

        public override IProjectLauncher/*!*/ GetLauncher()
        {
            return null;
        }

        protected override NodeProperties CreatePropertiesObject()
        {
            return new ProjectNodeProperties(this);
        }

        protected override Guid[] GetConfigurationIndependentPropertyPages()
        {
            return new[]
            {
                typeof(Project.HaskellGeneralPropertyPage).GUID,
                typeof(Project.HaskellDebugPropertyPage).GUID
            };
        }
        #endregion

        #region IHaskellProject implementation

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void SetProperty(string propertyName, string value)
        {

        }

        public new string GetStartupFile()
        {
            throw new NotImplementedException();
        }

        public string ProjectDirectory
        {
            get { throw new NotImplementedException(); }
        }

        public string ProjectName
        {
            get { throw new NotImplementedException(); }
        }

        public bool Publish(PublishProjectOptions options)
        {
            throw new NotImplementedException();
        }

        public IAsyncCommand FindCommand(string canonicalName)
        {
            throw new NotImplementedException();
        }

        public ProjectInstance GetMSBuildProjectInstance()
        {
            throw new NotImplementedException();
        }

        public void AddActionOnClose(object key, Action<object> action)
        {
            throw new NotImplementedException();
        }

        #endregion // IHaskellProject implementation
    }
}
