using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.EventArgs
{
    public class EventArguments
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }
        public EventArguments()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
