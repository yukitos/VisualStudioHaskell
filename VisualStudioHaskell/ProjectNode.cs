using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.VisualStudioHaskell
{
    class ProjectNode : Microsoft.VisualStudio.Project.ProjectNode
    {
        private VisualStudioHaskellPackage _package;

        public ProjectNode(VisualStudioHaskellPackage package)
        {
            _package = package;
        }
        public override Guid ProjectGuid
        {
            get { return GuidList.guidVisualStudioHaskellProjectFactory; }
        }
        public override string ProjectType
        {
            get { return "Haskell Project"; }
        }
        public override void AddFileFromTemplate(
           string source, string target)
        {
            this.FileTemplateProcessor.UntokenFile(source, target);
            this.FileTemplateProcessor.Reset();
        }
    }
}
