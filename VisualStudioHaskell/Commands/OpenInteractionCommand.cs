using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Repl;
using Microsoft.VisualStudioTools;

namespace Company.VisualStudioHaskell.Commands
{
    using Interaction;

    class OpenInteractionCommand : Command
    {
        private readonly int _cmdId;
        private readonly IServiceProvider _provider;
        // private readonly IInteractionFactory _factory;

        public OpenInteractionCommand(IServiceProvider provider, int cmdId/*, IInteractionFactory factory*/)
        {
            _cmdId = cmdId;
            _provider = provider;
        }

        public override void DoCommand(object sender, EventArgs e)
        {
            var window = (ToolWindowPane)ExecuteInInteractionCommand.EnsureReplWindow(_provider, null /* TODO: use _factory */, null);

            IVsWindowFrame frame = (IVsWindowFrame)window.Frame;
            ErrorHandler.ThrowOnFailure(frame.Show());
            ((IReplWindow)window).Focus();
        }
        public override int CommandId
        {
            get { return (int)_cmdId; }
        }
    }
}
