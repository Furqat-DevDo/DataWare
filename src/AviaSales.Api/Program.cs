using System.Text.Json;
using System.Text.Json.Serialization;
using AviaSales.Api.Extensions;
using AviaSales.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });;

builder.Services.AddServices(builder.Configuration);
builder.Services.AddDb(builder.Configuration);
builder.Services.AddValidators();
builder.Services.AddExtensions(builder.Host);
builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalExceptionHandler();
app.UseCorrelationId();

app.MapControllers();

app.Run();