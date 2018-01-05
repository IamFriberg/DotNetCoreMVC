using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace DotNetCoreMVC.Middleware
{
    /*
     * Class for adding support for node modules
    */
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(
            this IApplicationBuilder app, string root)
        {
            //Set the path for the project
            var path = Path.Combine(root, "node_modules");
            var fileProvider = new PhysicalFileProvider(path);
            //Create the options for static files
            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            //Set fileprovider
            options.FileProvider = fileProvider;
            //Use the static files with the options we provide
            app.UseStaticFiles(options);

            return app;
        }
    }
}
