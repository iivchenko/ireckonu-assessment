using System;

namespace Ireckonu.Application.Services.Events
{
    public abstract class Event
    {
        Guid Id { get; set; }

        DateTime Created { get; set; }
    }
}
