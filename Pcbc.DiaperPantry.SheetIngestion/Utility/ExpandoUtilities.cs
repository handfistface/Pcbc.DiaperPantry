using System.Dynamic;

namespace Pcbc.DiaperPantry.SheetIngestion.Utility
{
    public static class ExpandoUtilities
    {
        public static string GetByPropertyName(ExpandoObject obj, string propertyName)
        {
            var convertedDictionary = ((IDictionary<string, object>)obj);
            if (!convertedDictionary.Keys.Contains(propertyName))
                return "";
            return convertedDictionary[propertyName] as string;
        }
    }
}
