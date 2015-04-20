using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Repl;

namespace Company.VisualStudioHaskell.Interaction
{
    class InteractionEvaluator : IReplEvaluator
    {

        public void Dispose()
        {
        }

        public Task<ExecutionResult> Reset()
        {
            return ExecutionResult.Succeeded;
        }

        public Task<ExecutionResult> Initialize(IReplWindow window)
        {
            return ExecutionResult.Succeeded;
        }

        public void AbortCommand()
        {
        }

        public void ActiveLanguageBufferChanged(ITextBuffer currentBuffer, ITextBuffer previousBuffer)
        {
        }

        public bool CanExecuteText(string text)
        {
            return false;
        }

        public void ExecuteFile(string filename)
        {
        }

        public Task<ExecutionResult> ExecuteText(string text)
        {
            return ExecutionResult.Succeeded;
        }

        public string FormatClipboard()
        {
            return "";
        }
    }
}
