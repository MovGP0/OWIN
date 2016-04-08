using System;

namespace DependencyInjection
{
    public interface IClock
    {
        DateTime TimeNow { get; }
    }
}