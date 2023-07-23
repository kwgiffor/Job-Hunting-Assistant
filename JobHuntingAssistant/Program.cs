using JobHuntingAssistant.Services;
using JobHuntingAssistant.AI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddScoped<IUserService, SingleUserService>();
builder.Services.AddScoped<IJobListingService, InMemoryJobListingService>();
builder.Services.AddScoped<IAIModel, GPT4Model>();
builder.Services.AddScoped<IResumeGenerationService, AIResumeGenerationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
