using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Repl;
using Company.VisualStudioHaskell.Navigation;
using Company.VisualStudioHaskell.Interaction;

namespace Company.VisualStudioHaskell.Commands
{
    class ExecuteInInteractionCommand
    {
        internal static IReplWindow EnsureReplWindow(IServiceProvider serviceProvider, IInteractionFactory factory, ProjectNode project)
        {
            var model = serviceProvider.GetComponentModel();
            var replProvider = model.GetService<IReplWindowProvider>();

            // TODO: use ReplEvaluatorProvider.GetReplId
            string replId = "Haskell interaction test";
            var window = replProvider.FindReplWindow(replId);
            if (window == null)
            {
                window = replProvider.CreateReplWindow(
                    serviceProvider.GetHaskellContentType(),
                    "Haskell Interactive",
                    typeof(LanguageInfo).GUID,
                    replId
                    );

                var service = serviceProvider.GetHaskellService();
                window.SetOptionValue(
                    ReplOptions.UseSmartUpDown,
                    true  // service.GetInteractiveOptions(factory).ReplSmartHistory
                    );
            }

            // TODO: call project.AddActionOnClose

            return window;
        }
    }
}
