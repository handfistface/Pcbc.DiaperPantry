using Dapper;
using MySql.Data.MySqlClient;
using Pcbc.DiaperPantry.SheetIngestion.MySql.Objects;
using Pcbc.DiaperPantry.SheetIngestion.Objects;
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
        private readonly IRegexService _regexService;

        public ChildMySqlService(
            IConfigFile configFile, 
            IRegexService regexService)
        {
            _configFile = configFile;
            _regexService = regexService;
        }

        public ChildSql? GetChildById(int childId)
        {
            using var connection = new MySqlConnection(_configFile.DiaperPantryConnectionString);
            var sql = $"CALL `Pcbc.DiaperPantry`.Get_Child_ById({childId})";
            var reader = connection.ExecuteReader(sql);

            var children = ParseReader(reader);
            return children.FirstOrDefault();
        }

        public List<ChildSql> GetChildrenParentId(int parentId)
        {
            using var connection = new MySqlConnection(_configFile.DiaperPantryConnectionString);
            var sql = $"CALL `Pcbc.DiaperPantry`.Get_Child_ByParentId({parentId})";
            var reader = connection.ExecuteReader(sql);

            var children = ParseReader(reader);
            return children;
        }

        public void CreateChild(ChildSql child)
        {
            ArgumentNullException.ThrowIfNull(child);

            //sanitize the name, diaperstyle, and diapersize
            child.Name = _regexService.SanitizeMySqlString(child.Name);
            child.DiaperStyle = _regexService.SanitizeMySqlString(child.DiaperStyle);
            child.DiaperSize = _regexService.SanitizeMySqlString(child.DiaperSize);

            using var connection = new MySqlConnection(_configFile.DiaperPantryConnectionString);
            var sql = $"CALL `Pcbc.DiaperPantry`.Create_Child" +
                //"@NAME, @DIAPERSTYLE, @DIAPERSIZE, @BIRTHDAY, @PARENTID";
                $"('{child.Name}', '{child.DiaperStyle}', '{child.DiaperSize}', '{child.Birthday.ToString("yyyy-MM-dd")}', '{child.ParentId}' )";
            connection.Execute(sql);
        }

        public void UpdateChild(ChildSql child)
        {
            ArgumentNullException.ThrowIfNull(child);

            //sanitize the name, diaperstyle, and diapersize
            child.Name = _regexService.SanitizeMySqlString(child.Name);
            child.DiaperStyle = _regexService.SanitizeMySqlString(child.DiaperStyle);
            child.DiaperSize = _regexService.SanitizeMySqlString(child.DiaperSize);

            using var connection = new MySqlConnection(_configFile.DiaperPantryConnectionString);
            var sql = $"CALL `Pcbc.DiaperPantry`.Update_Child" +
                //@CHILDID, @NAME, @DIAPERSTYLE, @DIAPERSIZE, @BIRTHDAY, @PARENTID
                $"('{child.Id}', '{child.Name}', '{child.DiaperSize}', '{child.DiaperStyle}', '{child.Birthday.ToString("yyyy-MM-dd")}', '{child.ParentId}' )";
            connection.Execute(sql);
        }

        public bool DoesChildExist(string name, DateTime birthday, int parentId)
        {
            ArgumentNullException.ThrowIfNull(name);

            name = _regexService.SanitizeMySqlString(name);

            using var connection = new MySqlConnection(_configFile.DiaperPantryConnectionString);
            var sql = $"CALL `Pcbc.DiaperPantry`.Exists_Child('{name}', '{birthday.ToString("yyyy-MM-dd")}', '{parentId}')";

            var children = connection.QuerySingle(sql);
            if (children == null)
                return false;
            return children.DoesChildExist == 1;
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
