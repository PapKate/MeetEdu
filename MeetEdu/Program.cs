using MeetEdu;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using MudBlazor.Services;

using System.Reflection;
using System.Text;

//var config = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json", optional: false)
//    .Build();

//var host = config["Host"]!;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost
//    .UseUrls(host)
//    .ConfigureKestrel((context, options) =>
//    {
//        options.Limits.MaxRequestBodySize = 737280000;
//    });

// Add services to the container.

builder.Services.AddControllers((options) =>
{
    options.EnableEndpointRouting = false;
    options.ModelBinderProviders.Insert(0, CommaSeparatedModelBinderProvider.Instance);
}).AddNewtonsoftJson((options) => NewtonsoftHelpers.ConfigureSerializer(options.SerializerSettings));

// AutoMapper
builder.Services.AddMapper();

builder.Services.AddEndpointsApiExplorer();
MongoDbHelpers.Configure();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "MeetEdu API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMudServices();

builder.Services.AddSingleton(provider => AccountsRepository.Instance);
builder.Services.AddSingleton(provider => UsersRepository.Instance);
builder.Services.AddSingleton(provider => SecretariesRepository.Instance);
builder.Services.AddSingleton(provider => ProfessorsRepository.Instance);
builder.Services.AddSingleton(provider => MembersRepository.Instance);
builder.Services.AddSingleton(provider => UniversitiesRepository.Instance);
builder.Services.AddSingleton(provider => DepartmentsRepository.Instance);
builder.Services.AddSingleton(provider => AppointmentsRepository.Instance);

builder.Services.AddSingleton<HeaderUserManager>();

builder.Services.AddSingleton<MeetEduController>();
builder.Services.AddSingleton<MeetCoreController>();

builder.Services.AddScoped<SearchManager>();

// Adds a JWT bearer token authentication used for the native applications
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((options) =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // Validate issuer
        ValidateIssuer = true,
        // Validate audience
        ValidateAudience = true,
        // Validate expiration
        ValidateLifetime = true,
        // Validate signature
        ValidateIssuerSigningKey = true,

        // Set issuer
        ValidIssuer = "MeetEdu",
        // Set audience
        ValidAudience = "MeetEdu",

        // Set signing key
        IssuerSigningKey = new SymmetricSecurityKey(
                // Get our secret key from configuration
                Encoding.UTF8.GetBytes("d7sd1rhq8QastquUv9idfdfxds4512fdfg67f")),
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/hubs/accounts")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
}).AddNewtonsoftJsonProtocol(options => NewtonsoftHelpers.ConfigureSerializer(options.PayloadSerializerSettings));
builder.Services.AddSingleton<ConnectionsManager>();
builder.Services.AddTransient<AccountsHubClient>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

// Hosting doesn't add IHttpContextAccessor by default
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



var app = builder.Build();

DI.Provider = app.Services;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<AccountsHub>(HubConstants.Route);

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
