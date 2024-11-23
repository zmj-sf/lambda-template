using System.Diagnostics;

namespace ShareFile.Lambda.Template.Services;

public class UptimeService(Stopwatch startup)
{
    private readonly object _lock = new();

    public TimeSpan GetUptime()
    {
        lock(_lock)
        {
            return startup.Elapsed;
        }
    }
}
