using Api.Extensions;
using Api.Middleware;
using Infra;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;

string secretKey = "XKeYqViwIeC7D5rLdmtVeae751wgTrPYFcrGfTfhL0DIzHaTtvAFZK6HuHduBpjm";

var builder = WebApplication.CreateBuilder(args);

// Configurar JWT e SignalR usando a extensão
builder.Services.AddJwtAuthentication(secretKey);
builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";

    config.AddSecurity("JWT", new OpenApiSecurityScheme()
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu-token-jwt}"
    });
    
    // Configuração para exigir o token JWT em rotas protegidas
    config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// app.UseRouting(); -- o que é ?

app.UseHttpsRedirection();

app.RegisterEndpointsDefinitions();

app.Run();