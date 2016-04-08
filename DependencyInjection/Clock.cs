using System;

namespace DependencyInjection
{
    public sealed class Clock : IClock
    {
        public DateTime TimeNow => DateTime.UtcNow;
    }
}