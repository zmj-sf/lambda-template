using Amazon.Lambda.Serialization.SystemTextJson;
using ShareFile.Lambda.Template;
using ShareFile.Lambda.Template.Api;
using ShareFile.Lambda.Template.Models;
using ShareFile.Lambda.Template.Services;
using System.Diagnostics;

var startup = Stopwatch.StartNew();

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Add(SerializerContext.Default);
    options.SerializerOptions.Converters.Add(new PrettyTimeSpanConverter());
});
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi, new SourceGeneratorLambdaJsonSerializer<SerializerContext>());
builder.Services.AddSingleton(startup);
builder.Services.AddSingleton<UptimeService>();

var app = builder.Build();
app.MapGroup("api")
    .MapApi<UptimeApi>();
app.Run();