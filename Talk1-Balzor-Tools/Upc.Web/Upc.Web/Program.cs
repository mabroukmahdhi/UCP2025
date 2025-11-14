// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Upc.Web.Views;

namespace Upc.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder webApplicationBuilder =
                WebApplication.CreateBuilder(args);
             
            webApplicationBuilder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            webApplicationBuilder.Services.AddHttpClient();

            WebApplication webApplication =
                webApplicationBuilder.Build();

            if (!webApplication.Environment.IsDevelopment())
            {
                webApplication.UseExceptionHandler("/Error");
                webApplication.UseHsts();
            }

            webApplication.UseStatusCodePagesWithReExecute(
                pathFormat: "/not-found",
                createScopeForStatusCodePages: true);

            webApplication.UseHttpsRedirection();
            webApplication.UseAntiforgery();
            webApplication.MapStaticAssets();

            webApplication.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            webApplication.Run();
        }
    }
}
