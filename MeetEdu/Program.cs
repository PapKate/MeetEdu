using MeetEdu;

using MudBlazor.Services;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//.AddNewtonsoftJson((options) => NewtonsoftHelpers.ConfigureSerializer(options.SerializerSettings));

// AutoMapper
builder.Services.AddMapper();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "MeetEdu API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(o =>
{
    o.DetailedErrors = true;
});

builder.Services.AddMudServices();

builder.Services.AddSingleton(provider => AccountsRepository.Instance);
builder.Services.AddSingleton(provider => UsersRepository.Instance);
builder.Services.AddSingleton(provider => SecretariesRepository.Instance);
builder.Services.AddSingleton(provider => ProfessorsRepository.Instance);
builder.Services.AddSingleton(provider => MembersRepository.Instance);
builder.Services.AddSingleton(provider => UniversitiesRepository.Instance);
builder.Services.AddSingleton(provider => DepartmentsRepository.Instance);
builder.Services.AddSingleton(provider => AppointmentsRepository.Instance);

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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
