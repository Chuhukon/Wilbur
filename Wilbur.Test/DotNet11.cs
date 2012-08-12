using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wilbur.Dotnet11;
using Telerik.JustMock;

namespace Wilbur.Test
{
    /// <summary>
    /// Class demostrates .net 1.1 testing using a .net 4.0 test project..
    /// Naming convention: MethodNameToTest_StateUnderTest_ExpectedBehavior
    ///  * * * * * * Questions * * * * * * * * , 
    ///  - does nested MsCorlib work in .net 1.1??
    /// </summary>
    [TestClass]
    public class DotNet11
    {
        /// <summary>
        /// Basic test of a function build in .NET 1.1. No mocking
        /// Rule: The Sum of 10 and 5 returns 15.
        /// </summary>
        [TestMethod]
        public void Sum_TenAndFiveWithDotNet11_Returns15()
        {
            Calculator c = new Calculator(10, 5);
            Assert.AreEqual(15, c.Sum());
        }


        /// <summary>
        /// We are using .net 4.0 for testing. However when reference a dotNet 1.1 assembly 
        /// to your test project, you can also test your dotnet 1.1 code by using JustMock.
        /// 
        /// Moq and other similar mocking frameworks can only mock interfaces, 
        /// abstract methods/properties (on abstract classes) or 
        /// virtual methods/properties on concrete classes.
        /// 
        /// JustMock can nearly mock everything (see also the Divide_ methods
        /// for examples of testing a none virtual method.
        /// 
        /// Rule: No rule tested here, just an example
        /// </summary>
        [TestMethod]
        public void Sum_MockTheSumFunctionDotNet11_ReturnsTheMockedValue()
        {
            //A small example of mocking a 1.1 class
            //Notice that the Calculator doesn't has a parameter less constructor
            //and the class doesn't implement an interface either.
            //However by using JustMock you can create a mock without using any
            //default parameters..

            var cal = Mock.Create<Calculator>();
            Mock.Arrange(() => cal.Sum())
                .Returns(3);

            Assert.AreEqual(3, cal.Sum());
        }

        /// <summary>
        /// An example how to test a none virtual method with JustMock.
        /// With a framework like Moq this isn't possible, with JustMock it works
        /// the same as testing a virtual method.
        /// Rule: No rule tested here, just an example
        /// </summary>
        [TestMethod]
        public void Divide_MockANonVirtualMethodDotNet11_ReturnsTheMockedValue()
        {
            var cal = Mock.Create<Calculator>(); 
            Mock.Arrange(() => cal.Divide())
                .Returns(3);

            Assert.AreEqual(3, cal.Divide());
        }
    }
}
