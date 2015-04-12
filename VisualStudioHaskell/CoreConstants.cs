using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace Company.VisualStudioHaskell
{
    internal static class CoreConstants
    {
        public const string ContentType = "Haskell";
        // public const string BaseRegistryKey = "";

        /*[Export, Name(ContentType), BaseDefinition("code")]
        internal static ContentTypeDefinition ContentTypeDefinition = null;

        [Export, FileExtension(Constants.FileExtension), ContentType(ContentType)]
        internal static FileExtensionToContentTypeDefinition FileExtensionToContentTypeDefinition = null;*/
    }
}
