using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wilbur.DataAccess;
using System.Reflection;

namespace Wilbur.Test
{
    /// <summary>
    /// Why mocking, an example of testing the dataccess project without mocking!
    /// Naming convention: MethodNameToTest_StateUnderTest_ExpectedBehavior
    /// </summary>
    [TestClass]
    public class DataAccess_NoMocking
    {

        [TestInitialize()]
        public void MyTestInitialize()
        {
            // If the non-mocked version runs first then it raises the OnJITCompliationStarted event 
            // which prevents JustMock from intercepting the target member. With the next two lines you are sure
            // JustMock is intercepting the target member during the "mocked" examples.
            Telerik.JustMock.Mock.Replace(() => DateTime.Now).In<Pioneers>();
            Telerik.JustMock.Mock.NonPublic.Arrange<IList<AviationPioneer>>(typeof(Pioneers), "_dataContext").CallOriginal();
        }

        /// <summary>
        /// Get all dutch pioneers from a dataset that contains serveral pioneers dutch and non-dutch.
        /// Rule: The GetDutchPioneers function has to return only dutch pioneers.
        /// </summary>
        [TestMethod]
        public void GetDutchPioneers_ContextWithSeveralPioneersAndNationalities_DutchPioneersOnly_NonMocked()
        {
            // Arrange. Mock without a mocking framework
            Pioneers pioneers = new Pioneers(Source.XmlFile); //1st problem, you create a real object, is this xmlfile available???

            //Arrange. private _datacontext object
            PropertyInfo prop = typeof(Pioneers).GetProperty("_dataContext", BindingFlags.NonPublic | BindingFlags.Instance);
            prop.SetValue(pioneers, DataSource.Pioneers(), null);

            //Act.
            var result = pioneers.GetDutchPioneers();

            //Assert
            Assert.IsTrue(result.All(p => p.Nationality.Equals("NL")));
        }

        /// <summary>
        /// Get dutch get all pioneers who are 65 years or older. The context contains one several pioneers
        /// Rule: The OlderThan65 function has to return only pioneers older than 65 and still living.
        /// </summary>
        [TestMethod]
        public void OlderThan65_ContextWithSeveralPioneersDeadAndAlive_PioneersCurrentlyAreOlderThan65_NonMocked()
        {
            // Arrange. Mock without a mocking framework
            Pioneers pioneers = new Pioneers(Source.XmlFile); //1st problem, you create a real object, is this xmlfile available???

            //Arrange. private _datacontext object
            PropertyInfo prop = typeof(Pioneers).GetProperty("_dataContext", BindingFlags.NonPublic | BindingFlags.Instance);
            prop.SetValue(pioneers, DataSource.Pioneers(), null);

            /* We can't mock the calculate function!
            Mock.Arrange(() => Pioneers.CalculateAge(new DateTime(1931, 5, 13)))
                .Returns(....*/

            /* * * * * * * * * * * * Test will fail after some time.. * * * * * * * * * * * * * * * */

            //Act. 
            var result = pioneers.OlderThan65();

            //Assert
            Assert.IsTrue(result.Count() == 2);
        }

        /// <summary>
        /// Test the CalculateAge function. To avoid the test fails after a year or less we have
        /// to Mock the DateTime
        /// Rule: CalculateAge has to calculate the total of years from given date until today.
        /// 1.1 years = 2 and 0.8 years = 0
        /// </summary>
        [TestMethod]
        public void CalculateAge_Born19820520_Today_Age29_NonMocked()
        {
            //Arrange datetime now, today is 2 feb 2012
            var dt = new DateTime(2012, 2, 5);

            /* * * * * * * * * * * * Test will fail after some time.. * * * * * * * * * * * * * * * */

            //Act
            var result = Pioneers.CalculateAge(new DateTime(1982, 05, 20));

            //Assert
            Assert.IsFalse(result == 29); //Currently is 30, so it's not 29 any more
        }
    }
}
