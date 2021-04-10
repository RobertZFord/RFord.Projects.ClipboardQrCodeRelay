using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace RFord.Projects.ClipboardQrCodeRelay
{
    // extremely boilerplate hosted process.
    // basically just starts a timer that periodically queries the clipboard contents, and if they change, fires off an event with the new contents.
    public class ClipboardMonitorService : IHostedService, IDisposable
    {
        private bool disposedValue;
        private System.Timers.Timer _timer;

        private string _previousContents = string.Empty;
        
        // only one thread at a time, please!
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly IMediator _mediator;

        public ClipboardMonitorService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new System.Timers.Timer(100);
            _timer.Elapsed += CheckContents;
            _timer.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            return Task.CompletedTask;
        }

        //public void CheckContents(object state)
        public async void CheckContents(object sender, System.Timers.ElapsedEventArgs args)
        {
            // if we still have processing pending, skip this particular call.
            if (_semaphore.CurrentCount == 0)
            {
                return;
            }

            await _semaphore.WaitAsync();
            try
            {
                string contents = await ClipboardService.GetTextAsync();

                // QR Codes can only encode so much data.  If we have > 1024 characters, ignore the data.
                if (contents.Length > 1024)
                {
                    return;
                }

                // if the current and previous lengths are different, then we definitely need to fire off an event
                bool triggerEvent = contents.Length != _previousContents.Length;

                // if the lengths are the same, we could have a false negative, and as such, need to investigate further
                if (!triggerEvent)
                {
                    // to investigate further, we simply inspect each character individually, and if a difference is found, trigger the qr code generation event
                    for (int i = 0; i < contents.Length && !triggerEvent; i++)
                    {
                        if (contents[i] != _previousContents[i])
                        {
                            triggerEvent = true;
                        }
                        i++;
                    }
                }

                // if we need to fire off the qr code generation event
                if (triggerEvent)
                {
                    // fire off the event
                    await _mediator.Send(new GenerateNewQrCodeCommand { Contents = contents });

                    // store the current contents as the previous contents
                    _previousContents = contents;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _semaphore.Release();
            }

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _timer?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ClipboardMonitorService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
