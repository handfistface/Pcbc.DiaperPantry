using System.Dynamic;

namespace Pcbc.DiaperPantry.SheetIngestion.Utility
{
    public static class ExpandoUtilities
    {
        public static string GetByPropertyName(ExpandoObject obj, string propertyName)
        {
            return ((IDictionary<string, object>)obj)[propertyName] as string ?? "";
        }
    }
}
