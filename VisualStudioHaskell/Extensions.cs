using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.ComponentModelHost;

namespace Company.VisualStudioHaskell
{
    static class Extensions
    {
        internal static IComponentModel GetComponentModel(this IServiceProvider provider)
        {
            if (provider == null)
            {
                return null;
            }
            return (IComponentModel)provider.GetService(typeof(SComponentModel));
        }

        internal static IContentType GetHaskellContentType(this IServiceProvider provider)
        {
            return provider.GetComponentModel().GetService<IContentTypeRegistryService>().GetContentType(CoreConstants.ContentType);
        }

        internal static Service GetHaskellService(this IServiceProvider provider)
        {
            if (provider == null)
            {
                return null;
            }
            return (Service)provider.GetService(typeof(Service));
        }
    }
}
