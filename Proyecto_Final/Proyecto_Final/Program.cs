using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Data.Entidades;
using Proyecto_Final.Helpers;
using Vereyon.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<DataContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

//Configuramos el program para decirle que tenemos usuarios (Requisitos para crear usuarios)
builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireDigit = false;
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<DataContext>();

//Si hay alguna anomalia en las direcciones de la pagina nos redirecciona a la accion NotAuthorize del AccountController
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Account/NotAuthorized";
	options.AccessDeniedPath = "/Account/NotAuthorized";
});

//Inyectamos la clase Helper
builder.Services.AddTransient<SeedDb>();
builder.Services.AddFlashMessage();
builder.Services.AddScoped<IBlobHelper, BlobHelper>();
builder.Services.AddScoped<IUserHelper, UserHelper>();

var app = builder.Build();

//Metodo para hacer la inyeccion manual!
seedData();
void seedData()
{
	IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();

	using (IServiceScope? scope = scopeFactory.CreateScope())
	{
		SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
		service.SeedAsync().Wait();
	}
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//nos muestra otro tipo de error por si hay alguna anomalia, se lo implementa en el HomeController
app.UseStatusCodePagesWithReExecute("/error/{0}");  

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //Con esta linea le decimos al program que usaremos autenticacion cada vez que larguemos

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
