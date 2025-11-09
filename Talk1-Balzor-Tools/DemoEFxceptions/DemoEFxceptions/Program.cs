// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

namespace DemoEFxceptions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder webApplicationBuilder =
                WebApplication.CreateBuilder(args);

            webApplicationBuilder.Services.AddControllers();
            webApplicationBuilder.Services.AddOpenApi();

            WebApplication webApplication =
                webApplicationBuilder.Build();

            if (webApplication.Environment.IsDevelopment())
            {
                webApplication.MapOpenApi();
                webApplication.MapScalarApiReference();
            }

            webApplication.UseHttpsRedirection();
            webApplication.UseAuthorization();
            webApplication.MapControllers();
            webApplication.Run();
        }
    }
}
