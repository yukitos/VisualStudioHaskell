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

using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Company.VisualStudioHaskell.Editor
{
    using Navigation;

    /// <summary>
    /// Factory for creating our editor object. Extends from the IVsEditoryFactory interface
    /// </summary>
    [Guid(GuidList.guidVisualStudioHaskellEditorFactoryString)]
    public sealed class EditorFactory : IVsEditorFactory, IDisposable
    {
        private VisualStudioHaskellPackage _package;
        private ServiceProvider _vsServiceProvider;
        private bool _promptEncoding;

        public EditorFactory(VisualStudioHaskellPackage package, bool promptEncoding)
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering {0} constructor", this.ToString()));

            _package = package;
            _promptEncoding = promptEncoding;
        }
        public EditorFactory(VisualStudioHaskellPackage package)
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering {0} constructor", this.ToString()));

            _package = package;
        }

        /// <summary>
        /// Since we create a ServiceProvider which implements IDisposable we
        /// also need to implement IDisposable to make sure that the ServiceProvider's
        /// Dispose method gets called.
        /// </summary>
        public void Dispose()
        {
            if (_vsServiceProvider != null)
            {
                _vsServiceProvider.Dispose();
            }
        }

        #region IVsEditorFactory Members

        /// <summary>
        /// Used for initialization of the editor in the environment
        /// </summary>
        /// <param name="psp">pointer to the service provider. Can be used to obtain instances of other interfaces
        /// </param>
        /// <returns></returns>
        public int SetSite(Microsoft.VisualStudio.OLE.Interop.IServiceProvider psp)
        {
            _vsServiceProvider = new ServiceProvider(psp);
            return VSConstants.S_OK;
        }

        public object GetService(Type serviceType)
        {
            return _vsServiceProvider.GetService(serviceType);
        }

        // This method is called by the Environment (inside IVsUIShellOpenDocument::
        // OpenStandardEditor and OpenSpecificEditor) to map a LOGICAL view to a 
        // PHYSICAL view. A LOGICAL view identifies the purpose of the view that is
        // desired (e.g. a view appropriate for Debugging [LOGVIEWID_Debugging], or a 
        // view appropriate for text view manipulation as by navigating to a find
        // result [LOGVIEWID_TextView]). A PHYSICAL view identifies an actual type 
        // of view implementation that an IVsEditorFactory can create. 
        //
        // NOTE: Physical views are identified by a string of your choice with the 
        // one constraint that the default/primary physical view for an editor  
        // *MUST* use a NULL string as its physical view name (*pbstrPhysicalView = NULL).
        //
        // NOTE: It is essential that the implementation of MapLogicalView properly
        // validates that the LogicalView desired is actually supported by the editor.
        // If an unsupported LogicalView is requested then E_NOTIMPL must be returned.
        //
        // NOTE: The special Logical Views supported by an Editor Factory must also 
        // be registered in the local registry hive. LOGVIEWID_Primary is implicitly 
        // supported by all editor types and does not need to be registered.
        // For example, an editor that supports a ViewCode/ViewDesigner scenario
        // might register something like the following:
        //        HKLM\Software\Microsoft\VisualStudio\<version>\Editors\
        //            {...guidEditor...}\
        //                LogicalViews\
        //                    {...LOGVIEWID_TextView...} = s ''
        //                    {...LOGVIEWID_Code...} = s ''
        //                    {...LOGVIEWID_Debugging...} = s ''
        //                    {...LOGVIEWID_Designer...} = s 'Form'
        //
        public int MapLogicalView(ref Guid rguidLogicalView, out string pbstrPhysicalView)
        {
            pbstrPhysicalView = null;    // initialize out parameter

            // we support only a single physical view
            if (VSConstants.LOGVIEWID_Primary == rguidLogicalView ||
                VSConstants.LOGVIEWID_Code == rguidLogicalView ||
                VSConstants.LOGVIEWID_TextView == rguidLogicalView)
            {
                // Our editor supports FindInFiles, therefore we need to declare support for LOGVIEWID_TextView.
                // In addition our EditorPane implements IVsCodeWindow and we also provide the 
                // VSSettings (pkgdef) metadata statement that we support LOGVIEWID_TextView via the following
                // attribute on our Package class:
                // [ProvideEditorLogicalView(typeof(EditorFactory), VSConstants.LOGVIEWID.TextView_string)]

                return VSConstants.S_OK;        // primary view uses NULL as pbstrPhysicalView
            }
            else if (VSConstants.LOGVIEWID.Designer_guid == rguidLogicalView)
            {
                pbstrPhysicalView = "Design";
                return VSConstants.S_OK;
            }
            else
            {
                return VSConstants.E_NOTIMPL;   // you must return E_NOTIMPL for any unrecognized rguidLogicalView values
            }
        }

        public int Close()
        {
            return VSConstants.S_OK;
        }

        /// <summary>
        /// Used by the editor factory to create an editor instance. the environment first determines the 
        /// editor factory with the highest priority for opening the file and then calls 
        /// IVsEditorFactory.CreateEditorInstance. If the environment is unable to instantiate the document data 
        /// in that editor, it will find the editor with the next highest priority and attempt to so that same 
        /// thing. 
        /// NOTE: The priority of our editor is 32 as mentioned in the attributes on the package class.
        /// 
        /// Since our editor supports opening only a single view for an instance of the document data, if we 
        /// are requested to open document data that is already instantiated in another editor, or even our 
        /// editor, we return a value VS_E_INCOMPATIBLEDOCDATA.
        /// </summary>
        /// <param name="grfCreateDoc">Flags determining when to create the editor. Only open and silent flags 
        /// are valid
        /// </param>
        /// <param name="pszMkDocument">path to the file to be opened</param>
        /// <param name="pszPhysicalView">name of the physical view</param>
        /// <param name="pvHier">pointer to the IVsHierarchy interface</param>
        /// <param name="itemid">Item identifier of this editor instance</param>
        /// <param name="punkDocDataExisting">This parameter is used to determine if a document buffer 
        /// (DocData object) has already been created
        /// </param>
        /// <param name="ppunkDocView">Pointer to the IUnknown interface for the DocView object</param>
        /// <param name="ppunkDocData">Pointer to the IUnknown interface for the DocData object</param>
        /// <param name="pbstrEditorCaption">Caption mentioned by the editor for the doc window</param>
        /// <param name="pguidCmdUI">the Command UI Guid. Any UI element that is visible in the editor has 
        /// to use this GUID. This is specified in the .vsct file
        /// </param>
        /// <param name="pgrfCDW">Flags for CreateDocumentWindow</param>
        /// <returns></returns>
        //[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public int CreateEditorInstance(
                        uint grfCreateDoc,
                        string pszMkDocument,
                        string pszPhysicalView,
                        IVsHierarchy pvHier,
                        uint itemid,
                        System.IntPtr punkDocDataExisting,
                        out System.IntPtr ppunkDocView,
                        out System.IntPtr ppunkDocData,
                        out string pbstrEditorCaption,
                        out Guid pguidCmdUI,
                        out int pgrfCDW)
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering {0} CreateEditorInstace()", this.ToString()));

            // Initialize to null
            ppunkDocView = IntPtr.Zero;
            ppunkDocData = IntPtr.Zero;
            pguidCmdUI = Guid.Empty;
            pgrfCDW = 0;
            pbstrEditorCaption = null;

            // Validate inputs
            if ((grfCreateDoc & (VSConstants.CEF_OPENFILE | VSConstants.CEF_SILENT)) == 0)
            {
                return VSConstants.E_INVALIDARG;
            }
            if (_promptEncoding && punkDocDataExisting != IntPtr.Zero)
            {
                return VSConstants.VS_E_INCOMPATIBLEDOCDATA;
            }

            IVsTextLines lines;

            if (punkDocDataExisting == IntPtr.Zero)
            {
                Guid clsid = typeof(VsTextBufferClass).GUID;
                Guid iid = typeof(IVsTextLines).GUID;
                lines = _package.CreateInstance(ref clsid, ref iid, typeof(IVsTextLines)) as IVsTextLines;
                ((IObjectWithSite)lines).SetSite(_vsServiceProvider.GetService(typeof(IOleServiceProvider)));
            }
            else
            {
                lines = Marshal.GetObjectForIUnknown(punkDocDataExisting) as IVsTextLines;
                if (lines == null)
                {
                    var provider = lines as IVsTextBufferProvider;
                    if (provider != null)
                    {
                        provider.GetTextBuffer(out lines);
                    }
                }
                if (lines == null)
                {
                    ErrorHandler.ThrowOnFailure(VSConstants.VS_E_INCOMPATIBLEDOCDATA);
                }
            }

            if (punkDocDataExisting != IntPtr.Zero)
            {
                ppunkDocData = punkDocDataExisting;
                Marshal.AddRef(punkDocDataExisting);
            }
            else
            {
                ppunkDocData = Marshal.GetIUnknownForObject(lines);
            }

            try
            {
                if (string.IsNullOrEmpty(pszPhysicalView))
                {
                    ppunkDocView = CreateCodeView(pszMkDocument, lines, ref pbstrEditorCaption, ref pguidCmdUI);
                }
                else if (string.Compare(pszPhysicalView, "design", true, CultureInfo.InvariantCulture) == 0)
                {
                    ppunkDocView = CreateFormView(pvHier, itemid, lines, ref pbstrEditorCaption, ref pguidCmdUI);
                }
                else
                {
                    ErrorHandler.ThrowOnFailure(VSConstants.VS_E_UNSUPPORTEDFORMAT);
                }
            }
            finally
            {
                if (ppunkDocView == IntPtr.Zero)
                {
                    if (punkDocDataExisting != ppunkDocData && ppunkDocData != IntPtr.Zero)
                    {
                        Marshal.Release(ppunkDocData);
                        ppunkDocData = IntPtr.Zero;
                    }
                }
            }

            return VSConstants.S_OK;
        }

        #endregion

        private System.IntPtr CreateFormView(IVsHierarchy hierarchy, uint itemId, IVsTextLines lines, ref string caption, ref Guid cmdUI)
        {
            caption = string.Empty;
            cmdUI = Guid.Empty;

            var service = (IVSMDDesignerService)GetService(typeof(IVSMDDesignerService));
            var loader = (IVSMDDesignerLoader)service.CreateDesignerLoader("Microsoft.VisualStudio.Designer.Serialization.VSDesignerLoader");

            bool initialized = false;

            try
            {
                var provider = _vsServiceProvider.GetService(typeof(IOleServiceProvider)) as IOleServiceProvider;
                loader.Initialize(provider, hierarchy, (int)itemId, lines);
                initialized = true;

                var designer = service.CreateDesigner(provider, loader);

                caption = loader.GetEditorCaption((int)READONLYSTATUS.ROSTATUS_Unknown);
                cmdUI = designer.CommandGuid;

                return Marshal.GetIUnknownForObject(designer.View);
            }
            catch
            {
                if (initialized)
                {
                    loader.Dispose();
                }
                throw;
            }
        }

        private System.IntPtr CreateCodeView(string docMoniker, IVsTextLines lines, ref string caption, ref Guid cmdUI)
        {
            Type codeWindowType = typeof(IVsCodeWindow);
            Guid iid = codeWindowType.GUID;
            Guid clsid = typeof(VsCodeWindowClass).GUID;
            var window = (IVsCodeWindow)_package.CreateInstance(ref clsid, ref iid, codeWindowType);

            ErrorHandler.ThrowOnFailure(window.SetBuffer(lines));
            ErrorHandler.ThrowOnFailure(window.SetBaseEditorCaption(null));
            ErrorHandler.ThrowOnFailure(window.GetEditorCaption(READONLYSTATUS.ROSTATUS_Unknown, out caption));

            var data = lines as IVsUserData;
            if (data != null)
            {
                if (_promptEncoding)
                {
                    var guid = VSConstants.VsTextBufferUserDataGuid.VsBufferEncodingPromptOnLoad_guid;
                    data.SetData(ref guid, (uint)1);
                }

                Guid vsCoreGuid = new Guid("{8239bec4-ee87-11d0-8c98-00c04fc2ab22}");
                Guid langGuid = typeof(LanguageInfo).GUID;
                Guid curGuid;

                lines.GetLanguageServiceID(out curGuid);

                if (curGuid == vsCoreGuid)
                {
                    ErrorHandler.ThrowOnFailure(lines.SetLanguageServiceID(ref langGuid));
                }
                else if (curGuid != langGuid)
                {
                    ErrorHandler.ThrowOnFailure(VSConstants.VS_E_INCOMPATIBLEDOCDATA);
                }

                Guid detectLang = VSConstants.VsTextBufferUserDataGuid.VsBufferDetectLangSID_guid;
                ErrorHandler.ThrowOnFailure(data.SetData(ref detectLang, false));
            }

            cmdUI = VSConstants.GUID_TextEditorFactory;
            return Marshal.GetIUnknownForObject(window);
        }
    }
}
