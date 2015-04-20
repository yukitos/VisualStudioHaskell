﻿/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the Apache License, Version 2.0, please send an email to 
 * vspython@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Apache License, Version 2.0.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * ***************************************************************************/


using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudioTools.Project;
using Microsoft.VisualStudioTools.Project.Automation;
using VsShellUtil = Microsoft.VisualStudio.Shell.VsShellUtilities;

namespace Microsoft.VisualStudioTools {
    static class VsExtensions {
        public static string GetFilePath(this ITextView textView) {
            return textView.TextBuffer.GetFilePath();
        }
#if FALSE
        internal static ITrackingSpan CreateTrackingSpan(this IIntellisenseSession session, ITextBuffer buffer) {
            var triggerPoint = session.GetTriggerPoint(buffer);
            var position = session.GetTriggerPoint(buffer).GetPosition(session.TextView.TextSnapshot);

            var snapshot = buffer.CurrentSnapshot;
            if (position == snapshot.Length) {
                return snapshot.CreateTrackingSpan(position, 0, SpanTrackingMode.EdgeInclusive);
            } else {
                return snapshot.CreateTrackingSpan(position, 1, SpanTrackingMode.EdgeInclusive);
            }
        }
#endif
        internal static EnvDTE.Project GetProject(this IVsHierarchy hierarchy) {
            object project;

            ErrorHandler.ThrowOnFailure(
                hierarchy.GetProperty(
                    VSConstants.VSITEMID_ROOT,
                    (int)__VSHPROPID.VSHPROPID_ExtObject,
                    out project
                )
            );

            return (project as EnvDTE.Project);
        }

        public static CommonProjectNode GetCommonProject(this EnvDTE.Project project) {
            OAProject oaProj = project as OAProject;
            if (oaProj != null) {
                var common = oaProj.Project as CommonProjectNode;
                if (common != null) {
                    return common;
                }
            }
            return null;
        }

        public static string GetRootCanonicalName(this IVsProject project) {
            return ((IVsHierarchy)project).GetRootCanonicalName();
        }

        public static string GetRootCanonicalName(this IVsHierarchy heirarchy) {
            string path;
            ErrorHandler.ThrowOnFailure(heirarchy.GetCanonicalName(VSConstants.VSITEMID_ROOT, out path));
            return path;
        }

        internal static T[] Append<T>(this T[] list, T item) {
            T[] res = new T[list.Length + 1];
            list.CopyTo(res, 0);
            res[res.Length - 1] = item;
            return res;
        }

        internal static string GetFilePath(this ITextBuffer textBuffer) {
            ITextDocument textDocument;
            if (textBuffer.Properties.TryGetProperty<ITextDocument>(typeof(ITextDocument), out textDocument)) {
                return textDocument.FilePath;
            } else {
                return null;
            }
        }

        internal static IClipboardService GetClipboardService(this IServiceProvider serviceProvider) {
            return (IClipboardService)serviceProvider.GetService(typeof(IClipboardService));
        }

        internal static IUIThread GetUIThread(this IServiceProvider serviceProvider) {
            var uiThread = (IUIThread)serviceProvider.GetService(typeof(IUIThread));
            if (uiThread == null) {
                Trace.TraceWarning("Returning NoOpUIThread instance from GetUIThread");
                Debug.Assert(VsShellUtil.ShellIsShuttingDown, "No UIThread service but shell is not shutting down");
                return new NoOpUIThread();
            }
            return uiThread;
        }

        [Conditional("DEBUG")]
        public static void MustBeCalledFromUIThread(this IUIThread self, string message = "Invalid cross-thread call") {
            Debug.Assert(self is IMockUIThread || !self.InvokeRequired, message);
        }

        [Conditional("DEBUG")]
        public static void MustNotBeCalledFromUIThread(this IUIThread self, string message = "Invalid cross-thread call") {
            Debug.Assert(self is IMockUIThread || self.InvokeRequired, message);
        }


        #region NoOpUIThread class

        /// <summary>
        /// Provides a no-op implementation of <see cref="IUIThread"/> that will
        /// not execute any tasks.
        /// </summary>
        private sealed class NoOpUIThread : IMockUIThread {
            public void Invoke(Action action) { }

            public T Invoke<T>(Func<T> func) {
                return default(T);
            }

            public Task InvokeAsync(Action action) {
                return Task.FromResult<object>(null);
            }

            public Task<T> InvokeAsync<T>(Func<T> func) {
                return Task.FromResult<T>(default(T));
            }

            public Task InvokeTask(Func<Task> func) {
                return Task.FromResult<object>(null);
            }

            public Task<T> InvokeTask<T>(Func<Task<T>> func) {
                return Task.FromResult<T>(default(T));
            }

            public void MustBeCalledFromUIThreadOrThrow() { }

            public bool InvokeRequired {
                get { return false; }
            }
        }

        #endregion
    }
}
