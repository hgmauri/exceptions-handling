﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Serilog;

namespace Sample.Exceptions.WebApi.Core.Extensions;
public static class LogEnricherExtensions
{
    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        diagnosticContext.Set("UserName", httpContext?.User?.Identity?.Name);
        diagnosticContext.Set("UserAgent", httpContext?.Request?.Headers["User-Agent"].FirstOrDefault());
        diagnosticContext.Set("Resource", httpContext.GetMetricsCurrentResourceName());
    }

    public static string GetMetricsCurrentResourceName(this HttpContext httpContext)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        var endpoint = httpContext.Features?.Get<IEndpointFeature>()?.Endpoint;

        return endpoint?.Metadata?.GetMetadata<EndpointNameMetadata>()?.EndpointName;
    }
}
