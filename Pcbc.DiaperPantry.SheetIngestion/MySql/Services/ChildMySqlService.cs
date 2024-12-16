using Pcbc.DiaperPantry.SheetIngestion.MySql.Objects;

namespace Pcbc.DiaperPantry.SheetIngestion.MySql.Services
{
    /// <summary>
    ///     Interacts with the child table
    /// </summary>
    public class ChildMySqlService
    {
        private static string _dbConnectionString { get; set; }

        public ChildMySqlService()
        {
            var username = "";
            var password = "";
            _dbConnectionString = $"Server=192.168.0.35;Database=Pcbc.DiaperPantry;Uid={username};Pwd={password};";
        }

        public ChildSql GetAllChildren()
        {
            return null;
        }
    }
}
