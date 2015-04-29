using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Company.VisualStudioHaskell.Interaction
{
    class InteractionStreamReader: IDisposable
    {
        private StreamReader _reader;
        private volatile bool _shouldStop;

        public event EventHandler<string> DataReceived;

        public InteractionStreamReader(StreamReader streamReader)
        {
            _reader = streamReader;
        }

        public Thread CreateThread()
        {
            return new Thread(ReadThread);
        }

        private void ReadThread()
        {
            var prompt1 = "Prelude> ";
            var prompt2 = "Prelude| ";

            var builder = new StringBuilder();

            while (!_shouldStop)
            {
                var ch = _reader.Read();

                if (ch == -1)
                {
                    return;
                }
                else if ((char)ch == '\r')
                {
                    DataReceived(this, builder.ToString());
                    builder.Clear();
                }
                else if ((char)ch == '\n')
                {
                }
                else
                {
                    builder.Append((char)ch);

                    if (builder.Length == prompt1.Length && builder.StartsWith(prompt1))
                    {
                        builder.Clear();
                    }
                    else if (builder.Length == prompt2.Length && builder.StartsWith(prompt2))
                    {
                        builder.Clear();
                    }
                }
            }
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }

        public void Dispose()
        {
        }
    }
}
