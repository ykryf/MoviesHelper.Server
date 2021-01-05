using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesHelper.Api.Extensions
{
    public static class ExtensionMethods
    {
        public static string GetOmdbApiKey(this IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                // Implement for production
                return null;
            }
            return configuration["OmdbApiKey"];
        }
    }
}
