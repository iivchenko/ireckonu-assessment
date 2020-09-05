using Ireckonu.Application.Services.Events;

namespace Ireckonu.Application.Events
{
    public sealed class TemporaryFileUploadedEvent : Event
    {
        public TemporaryFileUploadedEvent(string file)
        {
            File = file;
        }

        public string File { get; private set; }
    }
}
