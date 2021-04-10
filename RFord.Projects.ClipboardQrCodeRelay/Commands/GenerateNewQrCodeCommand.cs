using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RFord.Projects.ClipboardQrCodeRelay.Commands
{
    public class GenerateNewQrCodeCommand : IRequest
    {
        public string Contents { get; set; }
    }
}
