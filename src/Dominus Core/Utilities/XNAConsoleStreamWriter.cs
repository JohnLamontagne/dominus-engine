using System;
using System.IO;
using System.Text;
using XNAGameConsole;



namespace Dominus_Core.Utilities
{
    public class XNAConsoleStreamWriter : TextWriter
    {
        GameConsole _output = null;

        public XNAConsoleStreamWriter(GameConsole output)
        {
            _output = output;
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(value);
            _output.WriteLine(value); // When character data is written, append it to the text box.
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
