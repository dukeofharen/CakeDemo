using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CakeBuildDemo.Tests
{
   [TestClass]
   public class ProgramFacts
   {
      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Program_Main_ArgsIsNull()
      {
         // arrange
         string[] args = null;

         // act / assert
         Program.Main(args);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Program_Main_ArgsIsEmpty()
      {
         // arrange
         string[] args = new string[0];

         // act / assert
         Program.Main(args);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Program_Main_NotAllArgsAreInt()
      {
         // arrange
         string[] args = {"1", "2", "d"};

         // act / assert
         Program.Main(args);
      }

      [TestMethod]
      public void Program_Main_HappyFlow()
      {
         // arrange
         string[] args = { "1", "2", "100" };

         // act / assert
         Program.Main(args);
      }
   }
}
