using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Reflection;
using RFord.Projects.ClipboardQrCodeRelay.Services;

namespace RFord.Projects.ClipboardQrCodeRelay
{
    class Program
    {
        static async Task Main(string[] args) => await 
                Host
                .CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    // this will periodically query the clipboard contents and fire off an event if they have changed
                    services.AddHostedService<ClipboardMonitorService>();

                    // clear our logging output, as this would likely interfere with our ascii art qr code.
                    // TODO: enable STDERR logging; if an error occurs, the qr code output is irrelevant anyway, and we probably want to know if something went wrong.
                    services.AddLogging(config =>
                    {
                        config.ClearProviders();
                    });

                    // add the mediator, commands, and request handlers.
                    services.AddMediatR(Assembly.GetExecutingAssembly());

                    // add our console output implementation
                    services.AddTransient<IOutput, Outputs.ConsoleOutput>();
                })
                .RunConsoleAsync();
    }
}
