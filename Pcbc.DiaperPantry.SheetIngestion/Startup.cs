using Microsoft.Extensions.DependencyInjection;
using Pcbc.DiaperPantry.SheetIngestion.Google;
using Pcbc.DiaperPantry.SheetIngestion.Mapping;
using Pcbc.DiaperPantry.SheetIngestion.Utility;

namespace Pcbc.DiaperPantry.SheetIngestion
{
    public class Startup
    {
        public IServiceProvider PerformDependencyInjection()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IGoogleSheetService, GoogleSheetService>()
                .AddScoped<ISheetDirector, SheetDirector>()
                .AddScoped<IDynamoToChildForm, DynamoToChildForm>()
                .AddScoped<IFileManipulator, FileManipulator>()
                .AddScoped<IConfigFile, ConfigFile>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
