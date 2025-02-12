﻿using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Contact_zoo_at_home.WebUI.Helpers
{
    public static class LocalizationOptionsExtention
    {
        public static IServiceCollection MyLocalizationOptions(this IServiceCollection services)
        {
            var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("uk"),
                };
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(culture: "en"),
                SupportedCultures = supportedCultures,
            };
            options.RequestCultureProviders = new[]
            {
                new RouteDataRequestCultureProvider() { Options = options }
            };

            services.AddSingleton(options);

            return services;
        }
    }
}
