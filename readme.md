# Lambda Template

This repository is a starting point for C# deployed to AWS Lambda functions. It compiles directly to machine code, meaning no startup overhead for the .NET runtime and JIT. 

More reading:
* https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/
* https://docs.aws.amazon.com/lambda/latest/dg/dotnet-native-aot.html

Not all libraries are compatible with AOT compilation!

## Deployment

Install the lambda tools: `dotnet tool install -g Amazon.Lambda.Tools`

Create the ZIP file to upload (docker build): `dotnet lambda package`

Runtime can be set to dotnet or OS-only:
> Deploying .NET 8 Native AOT functions using the managed dotnet8 runtime rather than the OS-only provided.al2023 runtime gives your function access to .NET system libraries. For example, libicu, which is used for globalization, is not included by default in the provided.al2023 runtime but is in the dotnet8 runtime.

The handler name is the executable assembly name, which by default is the project name: `ShareFile.Lambda.Template`.

## Local Development

When run locally, Kestrel is used for a stand-alone web server (instead of the lambda runtime). `launchsettings.json` configures the port and startup behavior.