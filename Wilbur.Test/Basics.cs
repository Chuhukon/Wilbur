using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wilbur.Basics;
using Telerik.JustMock;

namespace Wilbur.Test
{
    /// <summary>
    /// Class demonstrate basic unit testing senarios
    /// Naming convention: MethodNameToTest_StateUnderTest_ExpectedBehavior
    /// </summary>
    [TestClass]
    public class Basics
    {

        public Basics()
        {
            Mock.Replace(() => DateTime.Now).In<Basics>();
        }

        /// <summary>
        /// A basic example of a test. (no mocking used)
        /// Rule: the sum of 5 and 10. Results in 15.
        /// </summary>
        [TestMethod]
        public void Sum_TenAndFive_ReturnsTheSumOfTenAndFive()
        {
            Calculator c = new Calculator(10, 5);
            var actual = c.Sum();
            Assert.AreEqual(15, actual);

            //when using JustMock
            c = Mock.Create<Calculator>(new object[] { 10, 5 });
            actual = c.Sum();
            Assert.AreEqual(15, actual);
        }

        /// <summary>
        /// A very basic example of what mocking can do for you...
        /// The Sum functions itself is not tested in this case. 
        /// The mocking framework returns the value 5. When calling
        /// the Sum function.
        /// Rule: No rule tested here, just an example
        /// </summary>
        [TestMethod]
        public void Sum_MockTheSumFunction_ReturnsTheMockedValue()
        {
            //notice you don't need to initialize the class at all
            //that's because it's Mocking the interface
            var cal = Mock.Create<ICalculator>();
            Mock.Arrange(() => cal.Sum())
                .Returns(3);
            
            Assert.AreEqual(3, cal.Sum());
        }

        /// <summary>
        /// An example of mocking something where you don't haven't already make an implementation for.
        /// The HelloWorld function itself is not tested in this case.
        /// The mocking framework returns "Hello World" a string that starts with Hello
        /// Rule: No rule tested here, just an example
        /// </summary>
        [TestMethod]
        public void HelloWorld_ImplementationDoesNotExists_TheOutputStartsWithHello()
        {
            //Notice you don't made an implementation for IDosomething..
            var cal = Mock.Create<IDoSomeThings>();
            Mock.Arrange(() => cal.HelloWorld())
                .Returns("Hello world");

            Assert.IsTrue(cal.HelloWorld().StartsWith("Hello"));
        }

        /// <summary>
        /// Mocking functions with parameters explained.
        /// The Print function itself is not tested in this case.
        /// The mocking framework returns "Hello World" when you call the Print function
        /// with the string "1". When you use another string the Print function results in a NullReferenceException
        /// Rule: No rule tested here, just an example
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))] //for handling the expected fail of Print("Hello")
        public void Print_FunctionWithParameters_FunctionReturnsMockedValueSpecifiedDuringMocking()
        {
            //While mocking a function with a parameter
            //it's important that when you call the function you use
            //the exact same parameter, otherwise it fails..
            var some = Mock.Create<IDoSomeThings>();
            Mock.Arrange(() => some.Print("1"))
                .Returns("Hello world");

            Assert.IsTrue(some.Print("1").StartsWith("Hello"));

            //for example: this one fails:
            Assert.IsTrue(some.Print("Hello").StartsWith("Hello"));
        }

        /// <summary>
        /// MsCorLib example using Justmock.
        /// DateTime.Now has to return 2010-apr-12. After Arrange that with JustMock
        /// DateTime.Now is used the Mocked date
        /// Rule: No rule tested here, just an example
        /// </summary>
        [TestMethod]
        public void DateTimeNow_ArangeOtherDate_OtherDateIsUsed()
        {
            // Arrange
            Mock.Arrange(() => DateTime.Now).Returns(new DateTime(2010, 4, 12));

            // Act
            var now = DateTime.Now;

            // Assert
            Assert.AreEqual(2010, now.Year);
            Assert.AreEqual(4, now.Month);
            Assert.AreEqual(12, now.Day);


        }

        //example used in presentation (1)..
        [TestMethod]
        public void Print_HelloWorld_SuccessfullTest()
        {
            //1. Arrange
            var moq = Mock.Create<IDoSomeThings>();
            Mock.Arrange(() => moq.Print("Hello World"))
                .Returns("Hello World");

            //2. Act
            var result = moq.Print("Hello World");

            //3. Assert
            Assert.IsTrue(result.StartsWith("Hello"));
        }
    }
}
