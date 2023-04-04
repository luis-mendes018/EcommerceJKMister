using LojaJkMisterG.Areas.Admin.Servicos;
using System.Configuration;
using LojaJkMisterG.Context;
using LojaJkMisterG.Models;
using LojaJkMisterG.Repositories.Interfaces;
using LojaJkMisterG.Repositories;
using LojaJkMisterG.Servicos;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Identity;
using LojaJkMisterG.Areas.Admin.Servicos.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


#region política de senhas do identity
// services.Configure<IdentityOptions>(options => {
//    // Default Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 8;
//    options.Password.RequiredUniqueChars = 1;
//});
#endregion

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();




builder.Services.Configure<ConfigurationImagens>(builder.Configuration
    .GetSection("ConfigurationPastaImagens"));

builder.Services.AddTransient<IRoupaRepository, RoupaRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

builder.Services.AddScoped<RelatorioVendasService>();
builder.Services.AddScoped<GraficoVendasService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });

    options.AddPolicy("GerentePolicy", policy =>
    {
        policy.RequireRole("Admin", "Gerente");
    });

    options.AddPolicy("VendedorPolicy",policy =>
    {
        policy.RequireRole("Admin" ,"Vendedor");
    });

    


});

builder.Services.AddControllersWithViews();

builder.Services.AddPaging(options => {
    options.ViewName = "Bootstrap5";
    options.PageParameterName = "pageindex";
});

//Middlewares
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();


app.UseRouting();

CriarPerfisUsuarios(app);

////Cria os perfis
//seedUserRoleInitial.SeedRoles();

////Cria os usuários e atribui ao perfil
//seedUserRoleInitial.SeedUsers();


app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
         name: "areas",
         pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
     );

    endpoints.MapControllerRoute(
        name: "categoriaFiltro",
        pattern: "Roupa/{action}/{categoria?}",
        defaults: new { Controller = "Roupa", action = "List" });


    endpoints.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

async void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        await service.SeedRolesAync();
        await service.SeedUsersAync();
    }
}