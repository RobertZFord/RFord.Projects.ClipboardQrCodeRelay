using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RFord.Projects.ClipboardQrCodeRelay
{
    public class GenerateNewQrCodeCommand : IRequest
    {
        public string Contents { get; set; }
    }
}
