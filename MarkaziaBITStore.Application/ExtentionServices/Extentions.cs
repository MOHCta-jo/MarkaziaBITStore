using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.Services;
using MarkaziaBITStore.Application.Services.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.ExtentionServices
{
    public static class Extentions
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.TryAddScoped<ICategory, CategoryService>();
            services.TryAddScoped<IColor, ColorService>();
            services.TryAddScoped<Iitem, ItemService>();
            services.TryAddScoped<IitemColor, ItemColorService>();
            services.TryAddScoped<IitemsColorImage, ItemColorImageService>();
            services.TryAddScoped<ISupplierInvoiceHeader, SupplierInvoiceHeaderService>();
            services.TryAddScoped<ISupplierInvoiceDetails, SupplierInvoiceDetailService>();


            return services;
        }
    }
}
