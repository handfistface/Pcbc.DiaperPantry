using Moq;
using Pcbc.DiaperPantry.SheetIngestion.Utility;

namespace Pcbc.DiaperPantry.SheetIngestion.Tests.Utility
{
    [TestClass]
    public class Test_ConfigFile
    {
        [TestMethod]
        public void DiaperPantryConnectionString_ReturnsValue()
        {
            //Arrange
            var fileManipulator = new Mock<IFileManipulator>();
            var configFile = new ConfigFile(fileManipulator.Object);

            var fileContents = "{ \"DiaperPantryConnectionString\": \"testValue\" }";
            fileManipulator.Setup(o => o.ReadFile("./config.json")).Returns(fileContents);
            
            //Act
            var actual = configFile.DiaperPantryConnectionString;

            //Assert
            Assert.AreEqual("testValue", actual);
        }
    }
}
