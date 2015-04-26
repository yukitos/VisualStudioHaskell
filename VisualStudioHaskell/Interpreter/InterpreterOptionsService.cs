using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace Company.VisualStudioHaskell.Interpreter
{
    [Export(typeof(IInterpreterOptionsService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class InterpreterOptionsService : IInterpreterOptionsService, IDisposable
    {
        IInterpreterFactory _interpreter;

        public IInterpreterFactory GetInterpreter()
        {
            return null;
        }

        public void Dispose()
        {
        }
    }
}
