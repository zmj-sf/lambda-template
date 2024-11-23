using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShareFile.Lambda.Template.Models;
using ShareFile.Lambda.Template.Services;

namespace ShareFile.Lambda.Template.Api;

public class UptimeApi : IApi
{
    public static void MapRoutes(IEndpointRouteBuilder routes)
    {
        routes.MapGroup("uptime")
            .MapGet("/", GetUptime);
    }

    public static Results<Ok<GetUptimeResponse>, NotFound> GetUptime(
        [FromServices] UptimeService uptimeService)
    {
        var uptime = uptimeService.GetUptime();
        var response = new GetUptimeResponse { Uptime = new(uptime) };
        return TypedResults.Ok(response);
    }
}
