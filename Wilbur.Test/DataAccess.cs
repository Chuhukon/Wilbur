using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wilbur.DataAccess;
using Telerik.JustMock;

namespace Wilbur.Test
{
    /// <summary>
    /// Naming convention: MethodNameToTest_StateUnderTest_ExpectedBehavior
    /// </summary>
    [TestClass]
    public class DataAccess
    {

        [TestInitialize()]
        public void MyTestInitialize() 
        {
            //This will replace datetime.now inside the Pioneers class with a mocked datetime
            Mock.Replace(() => DateTime.Now).In<Pioneers>();
        }


        /// <summary>
        /// Get all dutch pioneers from a dataset that contains serveral pioneers dutch and non-dutch.
        /// Rule: The GetDutchPioneers function has to return only dutch pioneers.
        /// </summary>
        [TestMethod]
        public void GetDutchPioneers_ContextWithSeveralPioneersAndNationalities_DutchPioneersOnly()
        {
            // Arrange. The CallOriginal method marks a mocked method/property call that 
            // should execute the original method/property implementation
            Pioneers pioneers = Mock.Create<Pioneers>(Behavior.CallOriginal);
            
            //Arrange all function you like to execute a different implementation.
            Mock.NonPublic.Arrange<IList<AviationPioneer>>(pioneers, "_dataContext")
                .Returns(DataSource.Pioneers());

            // Act. Call the orginal implementation, you already arranged a different 
            // implementation for the _datacontext..
            var result = pioneers.GetDutchPioneers();

            //Assert
            Assert.IsTrue(result.All(p => p.Nationality.Equals("NL")));
        }

        /// <summary>
        /// Get dutch get all pioneers who are 65 years or older. The context contains one several pioneers
        /// Rule: The OlderThan65 function has to return only pioneers older than 65 and still living.
        /// </summary>
        [TestMethod]
        public void OlderThan65_ContextWithSeveralPioneersDeadAndAlive_PioneersCurrentlyAreOlderThan65()
        {
            //Arrange. The CallOriginal method marks a mocked method/property call that 
            //should execute the original method/property implementation
            Pioneers pioneers = Mock.Create<Pioneers>(Behavior.CallOriginal);

            //Arrange. private _datacontext object
            Mock.NonPublic.Arrange<IList<AviationPioneer>>(pioneers, "_dataContext")
                .Returns(DataSource.Pioneers());

            //Mock the CalculateAge function used by the OlderThan65 function.
            //The CalculateAge function is already tested in CalculateAge_* test methods

            /* You can arrange the CalculateAge function for each pioneers available in the context
             * like this:
            Mock.Arrange(() => Pioneers.CalculateAge(new DateTime(1931, 5, 13)))
                .Returns(81); //date of birth 
             * 
            Mock.Arrange(() => Pioneers.CalculateAge(new DateTime(1982, 5, 20)))
                //when set this return value to 66 the result count is 3..
                .Returns(30);
             * 
            Mock.Arrange(() => Pioneers.CalculateAge(new DateTime(1930, 8, 5)))
                .Returns(82);
            */

            //Or make your own test implementation of the calculateAge function.
            Mock.Arrange(() => Pioneers.CalculateAge(Arg.IsAny<DateTime>()))
                .Returns((DateTime birth) =>
                {
                    //Your test implementation here, for example
                    return 2012 - birth.Year;
                });

            //Act. Call the orginal implementation, you already arranged a different 
            //implementation for the _datacontext.. and the CalculateAge function...
            var result = pioneers.OlderThan65();

            //Assert
            //Based on the test value for calculateAge and the test context 
            //there are two person older than 65
            Assert.IsTrue(result.Count() == 2);
        }

        /// <summary>
        /// Test the CalculateAge function. To avoid the test fails after a year or less we have
        /// to Mock the DateTime
        /// Rule: CalculateAge has to calculate the total of years from given date until today.
        /// 1.1 years = 2 and 0.8 years = 0
        /// </summary>
        [TestMethod]
        public void CalculateAge_Born19820520_Today_Age29()
        {
            //Arrange datetime now, today is 2 feb 2012
            var dt = new DateTime(2012, 2, 5);
            Mock.Arrange(() => DateTime.Now).Returns(dt);

            //Act
            var result = Pioneers.CalculateAge(new DateTime(1982, 05, 20));

            //Asset
            Assert.IsTrue(result == 29);
        }
    }
}
