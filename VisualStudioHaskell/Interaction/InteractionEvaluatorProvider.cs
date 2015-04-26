using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Repl;
using Company.VisualStudioHaskell.Interpreter;

namespace Company.VisualStudioHaskell.Interaction
{
    [Export(typeof(IReplEvaluatorProvider))]
    class InteractionEvaluatorProvider : IReplEvaluatorProvider
    {
        private IInterpreterOptionsService _interpreterService;
        private IServiceProvider _serviceProvider;

        [ImportingConstructor]
        public InteractionEvaluatorProvider(
            [Import] IInterpreterOptionsService interpreterService,
            [Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider
        )
        {
            _interpreterService = interpreterService;
            _serviceProvider = serviceProvider;
        }

        public IReplEvaluator GetEvaluator(string replId)
        {
            if (replId.StartsWith("Haskell"))
            {
                return new InteractionEvaluator(null, _serviceProvider, _interpreterService);
            }
            return null;
        }


    }
}
