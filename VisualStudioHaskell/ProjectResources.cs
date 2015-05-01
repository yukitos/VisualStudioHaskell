using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Resources;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Company.VisualStudioHaskell
{
    internal class ProjectResources
    {
        public const string IdentifierClassificationType = "IdentifierClassificationType";
        public const string GeneralPropertyPageTitle = "GeneralPropertyPageTitle";
        public const string DebugPropertyPageTitle = "DebugPropertyPageTitle";

        private static readonly Lazy<ResourceManager> _manager = new Lazy<ResourceManager>(
            () => new ResourceManager("Company.VisualStudioHaskell.Resources", typeof(ProjectResources).Assembly),
            LazyThreadSafetyMode.ExecutionAndPublication
        );

        private static ResourceManager Manager
        {
            get
            {
                return _manager.Value;
            }
        }

        private static string GetStringInternal(string value, params object[] args)
        {
            string result = Manager.GetString(value, CultureInfo.CurrentUICulture);

            if (result == null)
            {
                return null;
            }

            if (args.Length == 0)
            {
                Debug.Assert(value.IndexOf("{0}") < 0);
                return result;
            }

            return string.Format(CultureInfo.CurrentUICulture, result, args);
        }

        public static string GetString(string value, params object[] args)
        {
            var result = GetStringInternal(value, args);
            Debug.Assert(result != null);
            return result ?? value;
        }

        internal static string GetUnhandledExceptionString(
            Exception ex,
            Type callerType,
            [CallerFilePath] string callerFile = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerName = null
            )
        {
            // not implemented
            return "";
        }
    }
}
