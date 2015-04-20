using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Repl;

namespace Company.VisualStudioHaskell.Interaction
{
    [Export(typeof(IReplEvaluatorProvider))]
    class InteractionEvaluatorProvider : IReplEvaluatorProvider
    {
        [ImportingConstructor]
        public InteractionEvaluatorProvider(
            //[Import] IInterpreterOptionsService interpreterService,
            [Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider
        )
        {
        }

        public IReplEvaluator GetEvaluator(string replId)
        {
            if (replId.StartsWith("Haskell"))
            {
                return new InteractionEvaluator();
            }
            return null;
        }


    }
}
