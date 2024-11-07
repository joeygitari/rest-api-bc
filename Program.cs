using RestApi.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<TokenService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddMemoryCache();

// Register CustomerService with HttpClient and default credentials.
builder.Services.AddHttpClient<CustomerService>()
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        UseDefaultCredentials = true,
    });

// Register EmployeeService with HttpClient and default credentials.
builder.Services.AddHttpClient<EmployeeService>()
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        UseDefaultCredentials = true,
    });

var app = builder.Build();
app.UseMiddleware<TokenAuthenticationMiddleware>();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occurred. Try again later." });
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
