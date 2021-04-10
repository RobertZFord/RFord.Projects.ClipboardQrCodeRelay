using MediatR;
using QRCoder;
using RFord.Projects.ClipboardQrCodeRelay.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFord.Projects.ClipboardQrCodeRelay.Handlers
{
    public class GenerateNewQrCodeCommandHandler : IRequestHandler<GenerateNewQrCodeCommand>
    {
        private readonly IOutput _output;

        public GenerateNewQrCodeCommandHandler(IOutput output)
        {
            _output = output;
        }

        public Task<Unit> Handle(GenerateNewQrCodeCommand request, CancellationToken cancellationToken)
        {
            // clear the console for optimum display
            _output.Clear();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(
                plainText: request.Contents,
                eccLevel: 0
            );
            AsciiQRCode qrCode = new AsciiQRCode(qrCodeData);
            string render = qrCode.GetGraphic(1);
            _output.Write(render);

            return Task.FromResult(Unit.Value);
        }
    }
}
