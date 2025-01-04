using Moq;
using Pcbc.DiaperPantry.SheetIngestion.MySql.Services;
using Pcbc.DiaperPantry.SheetIngestion.Utility;

namespace Pcbc.DiaperPantry.SheetIngestion.Tests.MySql.Services
{
    [TestClass]
    public class Test_ChildMySqlService
    {
        private ChildMySqlService Arrange_ChildMySqlService()
        {
            var configFile = new Mock<IConfigFile>();
            var childService = new ChildMySqlService(configFile.Object);

            var username = "";
            var password = "";
            var dbConnectionString = $"Server=192.168.0.35;Database=Pcbc.DiaperPantry;Uid={username};Pwd={password};";
            configFile.Setup(o => o.DiaperPantryConnectionString).Returns(dbConnectionString);

            return childService;
        }

        [TestMethod]
        public void GetByChildId_DryRun()
        {
            // Arrange
            var childService = Arrange_ChildMySqlService();
            // Act
            var actual = childService.GetChildById(1);
            // Assert
            Assert.IsTrue(actual.Count() > 0);
        }
    }
}
