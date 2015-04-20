using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudioTools.Project;
using Microsoft.VisualStudioTools.Navigation;

namespace Company.VisualStudioHaskell.Navigation
{
    interface IHaskellLibraryManager : ILibraryManager
    {
    }

    class HaskellLibraryManager : LibraryManager, IHaskellLibraryManager
    {
        private readonly VisualStudioHaskellPackage _package;

        public HaskellLibraryManager(VisualStudioHaskellPackage package)
            : base(package)
        {
            _package = package;
        }

        protected override LibraryNode CreateLibraryNode(LibraryNode parent, IScopeNode subItem, string namePrefix, IVsHierarchy hierarchy, uint itemid)
        {
            return null;
        }

        public override LibraryNode CreateFileLibraryNode(LibraryNode parent, HierarchyNode hierarchy, string name, string filename, LibraryNodeType libraryNodeType)
        {
            return ((LibraryManager)this).CreateFileLibraryNode(parent, hierarchy, name, filename, libraryNodeType);
        }
    }
}
