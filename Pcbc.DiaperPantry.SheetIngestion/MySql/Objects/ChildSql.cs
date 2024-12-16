namespace Pcbc.DiaperPantry.SheetIngestion.MySql.Objects
{
    /// <summary>
    ///     Represents a child in the database
    /// </summary>
    public class ChildSql
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DiaperStyle { get; set; }
        public string DiaperSize { get; set; }
        public DateTime Birthday { get; set; }
        public int ParentId { get; set; }
    }
}
