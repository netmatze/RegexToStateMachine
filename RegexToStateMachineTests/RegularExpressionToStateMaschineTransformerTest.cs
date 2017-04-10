using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexToStateMaschine;

namespace RegexToStateMaschineTests
{
    [TestClass]
    public class RegularExpressionToStateMaschineTransformerTest
    {
        [TestMethod]
        public void StateMaschineBuilderTest()
        {
            StateMaschineBuilder stateMaschineBuilder = new StateMaschineBuilder();
            StateMaschine stateMaschine = stateMaschineBuilder.Match("ABC");
            var result = stateMaschineBuilder.Recognize("ABC", stateMaschine);
        }

        [TestMethod]
        public void StateMaschineBuilderStarTest()
        {
            StateMaschineBuilder stateMaschineBuilder = new StateMaschineBuilder();
            StateMaschine stateMaschine = stateMaschineBuilder.Match("A*");
            var result = stateMaschineBuilder.Recognize("AAAAA", stateMaschine);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void StateMaschineBuilderBracketTest()
        {
            StateMaschineBuilder stateMaschineBuilder = new StateMaschineBuilder();
            StateMaschine stateMaschine = stateMaschineBuilder.Match("((AB|EF)*GH)I");
            var result = stateMaschineBuilder.Recognize("ABABEFEFGHI", stateMaschine);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void StateMaschineBuilderOrAndStarExampleTest()
        {
            var regularExpression = "(((AB|CD)|(EF|GH*))|(IJ))";
            StateMaschineBuilder stateMaschineBuilder = new StateMaschineBuilder();
            StateMaschine stateMaschine = stateMaschineBuilder.Match(regularExpression);
            stateMaschine.Debug(regularExpression);
            var result = stateMaschineBuilder.Recognize("GHH", stateMaschine);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void StateMaschineBuilderOrExampleTest()
        {
            var regularExpression = "((AB|DC)*)";            
            StateMaschineBuilder stateMaschineBuilder = new StateMaschineBuilder();
            StateMaschine stateMaschine = stateMaschineBuilder.Match(regularExpression);
            stateMaschine.Debug(regularExpression);
            var result = stateMaschineBuilder.Recognize("ABDC", stateMaschine);
            Assert.AreEqual(result, true);
        }
    }
}
