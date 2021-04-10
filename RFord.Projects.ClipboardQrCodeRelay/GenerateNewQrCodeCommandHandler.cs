using MediatR;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFord.Projects.ClipboardQrCodeRelay
{
    public class GenerateNewQrCodeCommandHandler : IRequestHandler<GenerateNewQrCodeCommand>
    {
        public Task<Unit> Handle(GenerateNewQrCodeCommand request, CancellationToken cancellationToken)
        {
            // clear the console for optimum display
            Console.Clear();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(
                plainText: request.Contents,
                eccLevel: 0
            );
            AsciiQRCode qrCode = new AsciiQRCode(qrCodeData);
            string render = qrCode.GetGraphic(1);
            Console.WriteLine(render);

            return Task.FromResult(Unit.Value);
        }
    }
}
