namespace ShareFile.Lambda.Template.Api;

public interface IApi
{
    abstract static void MapRoutes(IEndpointRouteBuilder routes);
}

public static class IApiExtensions
{
    public static IEndpointRouteBuilder MapApi<T>(this IEndpointRouteBuilder routes) where T : IApi
    {
        T.MapRoutes(routes);
        return routes;
    }
}
