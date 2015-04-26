using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.VisualStudioHaskell.Interpreter
{
    interface IInterpreterFactory
    {
        string Description { get; }

        // TODO: add other members

        IInterpreter CreateInterpreter();
    }
}
