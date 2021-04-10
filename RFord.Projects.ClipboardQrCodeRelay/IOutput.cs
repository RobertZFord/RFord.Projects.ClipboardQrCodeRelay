using System;
using System.Collections.Generic;
using System.Text;

namespace RFord.Projects.ClipboardQrCodeRelay
{
    public interface IOutput
    {
        void Clear();
        void Write(string contents);
    }
}
