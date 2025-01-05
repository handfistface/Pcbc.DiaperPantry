using AutoFixture;
using Moq;
using Pcbc.DiaperPantry.SheetIngestion.MySql.Objects;
using Pcbc.DiaperPantry.SheetIngestion.MySql.Services;
using Pcbc.DiaperPantry.SheetIngestion.Utility;

namespace Pcbc.DiaperPantry.SheetIngestion.Tests.MySql.Services
{
    [TestClass]
    public class Test_ChildMySqlService
    {
        private Mock<IRegexService> _regexService;

        private ChildMySqlService Arrange_ChildMySqlService()
        {
            var configFile = new Mock<IConfigFile>();
            _regexService = new Mock<IRegexService>();
            var childService = new ChildMySqlService(
                configFile.Object,
                _regexService.Object);

            var username = "";
            var password = "";
            var dbConnectionString = $"Server=192.168.0.35;Database=Pcbc.DiaperPantry;Uid={username};Pwd={password};";
            configFile.Setup(o => o.DiaperPantryConnectionString).Returns(dbConnectionString);

            return childService;
        }

        [TestMethod]
        [Ignore]
        public void GetByChildId_DryRun()
        {
            // Arrange
            var childService = Arrange_ChildMySqlService();
            // Act
            var actual = childService.GetChildById(1);
            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        [Ignore]
        public void GetChildrenParentId_DryRun()
        {
            // Arrange
            var childService = Arrange_ChildMySqlService();
            // Act
            var actual = childService.GetChildrenParentId(1);
            // Assert
            Assert.IsTrue(actual.Count() > 0);
        }

        [TestMethod]
        [Ignore]
        public void CreateChild_DryRun()
        {
            // Arrange
            var childService = Arrange_ChildMySqlService();
            var fixture = new Fixture();
            var child = fixture.Create<ChildSql>();
            _regexService.Setup(o => o.SanitizeMySqlString(child.Name))
                .Returns(child.Name);
            _regexService.Setup(o => o.SanitizeMySqlString(child.DiaperSize))
                .Returns(child.DiaperSize);
            _regexService.Setup(o => o.SanitizeMySqlString(child.DiaperStyle))
                .Returns(child.DiaperStyle);

            // Act
            // Assert
            childService.CreateChild(child);
        }

        [TestMethod]
        [Ignore]
        public void UpdateChild_DryRun()
        {
            // Arrange
            var childService = Arrange_ChildMySqlService();
            var fixture = new Fixture();
            var child = fixture.Create<ChildSql>();
            child.Id = 1;
            _regexService.Setup(o => o.SanitizeMySqlString(child.Name))
                .Returns(child.Name);
            _regexService.Setup(o => o.SanitizeMySqlString(child.DiaperSize))
                .Returns(child.DiaperSize);
            _regexService.Setup(o => o.SanitizeMySqlString(child.DiaperStyle))
                .Returns(child.DiaperStyle);

            // Act
            // Assert
            childService.UpdateChild(child);

            //Revert
            child.Name = "Abigail June";
            child.DiaperSize = "3";
            child.DiaperStyle = "Traditional";
            child.Birthday = new DateTime(2024, 06, 18);
            child.ParentId = 1;
            _regexService.Setup(o => o.SanitizeMySqlString(child.Name))
                .Returns(child.Name);
            _regexService.Setup(o => o.SanitizeMySqlString(child.DiaperSize))
                .Returns(child.DiaperSize);
            _regexService.Setup(o => o.SanitizeMySqlString(child.DiaperStyle))
                .Returns(child.DiaperStyle);
            childService.UpdateChild(child);
        }

        [TestMethod]
        [Ignore]
        public void DoesChildExist_DryRun()
        {
            //Arrange
            var childService = Arrange_ChildMySqlService();
            _regexService.Setup(o => o.SanitizeMySqlString(It.IsAny<string>()))
                .Returns("Abigail June");
            //Assert
            var actual = childService.DoesChildExist("Abigail June", new DateTime(2024, 06, 18), 1);
            //Act
            Assert.IsTrue(actual);
        }
    }
}
