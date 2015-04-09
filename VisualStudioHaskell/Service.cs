using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.OLE.Interop;

namespace Company.VisualStudioHaskell
{
    class Service
    {
        private readonly IServiceContainer _container;
        private Editor.LanguagePreferences _langPrefs;

        internal Service(IServiceContainer container)
        {
            _container = container;

            var langService = new LanguageInfo(container);
            _container.AddService(langService.GetType(), langService, true);

            IVsTextManager textMgr = (IVsTextManager)container.GetService(typeof(SVsTextManager));
            if (textMgr != null)
            {
                var langPrefs = new LANGPREFERENCES[1];
                langPrefs[0].guidLang = typeof(LanguageInfo).GUID;
                ErrorHandler.ThrowOnFailure(textMgr.GetUserPreferences(null, null, langPrefs, null));
                _langPrefs = new Editor.LanguagePreferences(langPrefs[0]);

                Guid guid = typeof(IVsTextManagerEvents2).GUID;
                IConnectionPoint connectionPoint;
                ((IConnectionPointContainer)textMgr).FindConnectionPoint(ref guid, out connectionPoint);
                uint cookie;
                connectionPoint.Advise(_langPrefs, out cookie);
            }
        }
    }
}
