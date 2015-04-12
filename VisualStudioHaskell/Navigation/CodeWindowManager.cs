using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Company.VisualStudioHaskell.Navigation
{
    class CodeWindowManager : IVsCodeWindowManager, IVsCodeWindowEvents
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVsCodeWindow _window;
        private readonly ITextBuffer _textBuffer;
        private IWpfTextView _curView;
        private readonly Service _hsService;

        public int AddAdornments()
        {
            return VSConstants.E_NOTIMPL;
        }

        public int RemoveAdornments()
        {
            return VSConstants.E_NOTIMPL;
        }

        public int OnNewView(IVsTextView vsTextView)
        {
            return VSConstants.E_NOTIMPL;
        }

        int IVsCodeWindowEvents.OnNewView(IVsTextView vsTextView)
        {
            return VSConstants.E_NOTIMPL;
        }

        public int OnCloseView(IVsTextView vsTextView)
        {
            return VSConstants.E_NOTIMPL;
        }


    }
}
