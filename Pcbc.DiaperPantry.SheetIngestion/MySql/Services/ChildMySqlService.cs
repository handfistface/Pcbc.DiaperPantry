using Dapper;
using MySql.Data.MySqlClient;
using Pcbc.DiaperPantry.SheetIngestion.MySql.Objects;
using Pcbc.DiaperPantry.SheetIngestion.Utility;
using System.Data;

namespace Pcbc.DiaperPantry.SheetIngestion.MySql.Services
{
    /// <summary>
    ///     Interacts with the child table
    /// </summary>
    public class ChildMySqlService
    {
        private readonly IConfigFile _configFile;

        public ChildMySqlService(IConfigFile configFile)
        {
            _configFile = configFile;
        }

        public ChildSql? GetChildById(int childId)
        {
            using var connection = new MySqlConnection(_configFile.DiaperPantryConnectionString);
            var sql = $"CALL `Pcbc.DiaperPantry`.Get_Child_ById({childId})";
            var reader = connection.ExecuteReader(sql);

            var children = ParseReader(reader);
            return children.FirstOrDefault();
        }

        /// <summary>
        ///     Parses a collection of children from the query result
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<ChildSql> ParseReader(IDataReader reader)
        {
            var parser = reader.GetRowParser<ChildSql>(typeof(ChildSql));
            var children = new List<ChildSql>();
            while (reader.Read())
            {
                var myObject = parser(reader);
                children.Add(myObject);
            }
            return children;
        }
    }
}
