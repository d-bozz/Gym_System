global using SistemaVentaBlazor.Client.Servicios.Contrato;
global using SistemaVentaBlazor.Shared;
using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SistemaVentaBlazor.Client;
using SistemaVentaBlazor.Client.Servicios.Implementacion;
using SistemaVentaBlazor.Client.Utilidades;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ICategoriaService,CategoriaService>();
builder.Services.AddScoped<IProductoService,ProductoService>();
builder.Services.AddScoped<IRolService,RolService>();
builder.Services.AddScoped<IUsuarioService,UsuarioService>();
builder.Services.AddScoped<IVentaService,VentaService>();
builder.Services.AddScoped<IDashBoardService,DashBoardService>();
builder.Services.AddScoped<ISocioService, SocioService>();
builder.Services.AddScoped<IPagoMensualService, PagoMensualService>();
builder.Services.AddScoped<IHorarioActividadService, HorarioActividadService>();


builder.Services.AddMudServices();
builder.Services.AddSweetAlert2();

builder.Services.AddSyncfusionBlazor();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationExtension>();

await builder.Build().RunAsync();
