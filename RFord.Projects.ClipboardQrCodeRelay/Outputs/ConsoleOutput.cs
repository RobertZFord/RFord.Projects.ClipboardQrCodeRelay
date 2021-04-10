using System;
using System.Collections.Generic;
using System.Text;

namespace RFord.Projects.ClipboardQrCodeRelay.Outputs
{
    public class ConsoleOutput : IOutput
    {
        public void Clear() => Console.Clear();

        public void Write(string contents) => Console.WriteLine(contents);
    }
}
