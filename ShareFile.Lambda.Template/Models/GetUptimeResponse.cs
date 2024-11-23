namespace ShareFile.Lambda.Template.Models;

public class GetUptimeResponse
{
    public required PrettyTimeSpan Uptime { get; init; }
}
