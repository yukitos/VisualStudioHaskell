using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.ComponentModelHost;

namespace Company.VisualStudioHaskell
{
    [Guid(GuidList.guidVisualStudioHaskellLanguageServiceString)]
    class LanguageInfo : IVsLanguageInfo
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IComponentModel _componentModel;

        public LanguageInfo(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _componentModel = serviceProvider.GetService(typeof(SComponentModel)) as IComponentModel;
        }

        public int GetCodeWindowManager(IVsCodeWindow pCodeWin, out IVsCodeWindowManager ppCodeWinMgr)
        {
            var model = _serviceProvider.GetService(typeof(SComponentModel)) as IComponentModel;
            var service = model.GetService<IVsEditorAdaptersFactoryService>();

            IVsTextView textView;
            if (ErrorHandler.Succeeded(pCodeWin.GetPrimaryView(out textView)))
            {
                /*ppCodeWinMgr = new CodeWindowManager(_serviceProvider, pCodeWin, service.GetWpfTextView(textView));

                return VSConstants.S_OK;*/
            }

            ppCodeWinMgr = null;
            return VSConstants.E_FAIL;
        }

        public int GetFileExtensions(out string pbstrExtensions)
        {
            // This is the same extension the language service was
            // registered as supporting.
            pbstrExtensions = Constants.FileExtension;
            return VSConstants.S_OK;
        }


        public int GetLanguageName(out string bstrName)
        {
            // This is the same name the language service was registered with.
            bstrName = Constants.LanguageName;
            return VSConstants.S_OK;
        }

        /// <summary>
        /// GetColorizer is not implemented because we implement colorization using the new managed APIs.
        /// </summary>
        public int GetColorizer(IVsTextLines pBuffer, out IVsColorizer ppColorizer)
        {
            ppColorizer = null;
            return VSConstants.E_FAIL;
        }

        public IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }
    }
}
