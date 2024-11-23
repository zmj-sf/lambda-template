using Amazon.Lambda.APIGatewayEvents;
using ShareFile.Lambda.Template.Models;
using System.Text.Json.Serialization;

namespace ShareFile.Lambda.Template;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(APIGatewayHttpApiV2ProxyRequest))]
[JsonSerializable(typeof(APIGatewayHttpApiV2ProxyResponse))]
[JsonSerializable(typeof(GetUptimeResponse))]
public partial class SerializerContext : JsonSerializerContext;
