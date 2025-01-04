using Pcbc.DiaperPantry.SheetIngestion;
using Pcbc.DiaperPantry.SheetIngestion.Google;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Pcbc.DiaperPantry.SheetIngestion.Tests")]

internal class Program
{
    private static void Main(string[] args)
    {
        var serviceProvider = new Startup().PerformDependencyInjection();

        var sheetDirector = serviceProvider.GetService(typeof(ISheetDirector)) as ISheetDirector;
        sheetDirector.PullData();
    }
}