using Microsoft.Extensions.DependencyInjection;
using Pcbc.DiaperPantry.SheetIngestion.Google;
using Pcbc.DiaperPantry.SheetIngestion.Mapping;

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
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
