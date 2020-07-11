using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Binottery.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Binottery.Test
{
    [TestClass]
    public class NumberGeneratorTest
    {
        [TestMethod]
        public void ValidateGeneratedNumbers()
        {
            var gen1 = new NumberGenerator();

            var result = gen1.GenerateXNumbers(1);
            Assert.AreEqual(Constants.MatrixNumberOfRows, result.Length);
            Assert.IsTrue(result.All(r => r >= 10 && r <= 19));
            Assert.AreEqual(result.Length, result.Distinct().Count());


            result = gen1.GenerateXNumbers(0);
            Assert.AreEqual(Constants.MatrixNumberOfRows, result.Length);
            Assert.IsTrue(result.All(r => r >= 0 && r <= 9));
            Assert.AreEqual(result.Length, result.Distinct().Count());


            result = gen1.GenerateXNumbers(8);
            Assert.AreEqual(Constants.MatrixNumberOfRows, result.Length);
            Assert.IsTrue(result.All(r => r >= 80 && r <= 89));
            Assert.AreEqual(result.Length, result.Distinct().Count());

            var gen2 = new NumberGenerator();

            result = gen2.GenerateWinningIndexes();
            Assert.AreEqual(result.Length, Constants.NumberOfWinningOptions);
            Assert.IsTrue(result.All(r => r >= 0 && r < Constants.MatrixNumberOfColumns * Constants.MatrixNumberOfRows));
            Assert.AreEqual(result.Length, result.Distinct().Count());

        }
    }
}