using System.Collections.Generic;
using System.IO;
using System.Text;
using XNAGameConsole;

namespace Dominus_Core.Utilities
{
    public class XNAConsoleStreamWriter : TextWriter
    {
        private GameConsole _output = null;

        private Queue<string> _messageQueue;

        public XNAConsoleStreamWriter(GameConsole output)
        {
            _output = output;
            _messageQueue = new Queue<string>();
        }

        internal void Update()
        {
            lock (_messageQueue)
            {
                for (int i = 0; i < _messageQueue.Count; i++)
                {
                    _output.WriteLine(_messageQueue.Dequeue());
                }
            }
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(value);

            lock (_messageQueue)
            {
                _messageQueue.Enqueue(value);
            }
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}