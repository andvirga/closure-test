using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ParametersValidation
{
    /// <summary>
    /// Given a string containing just the characters '(', ')', '{', '}', '[' and ']',
    /// determine if the input string has valid parentheses.
    /// </summary>
    public interface IChallenge
    {
        bool Execute(string parameters);
    }

    public class MyChallenge : IChallenge
    {
        public bool Execute(string parameters)
        {
            //--Check the parameters using
            var length = parameters.Length;

            //"(){[]}";
            // 123456
            for (int pos = 0; pos < length; pos++)
            {
                int nextPos = pos + 1;

                //--Checking "()"
                if (parameters[pos] == '(' && parameters[nextPos] != ')')
                {
                    for (int i = nextPos + 1; i < length; i++)
                    {
                        if (parameters[i] == ')')
                            break;

                        //--If there is no match to any closing parenthesis. return false (is not valid)
                        return false;
                    }
                }

                //--Checking "{}"
                if (parameters[pos] == '{' && parameters[nextPos] != '}')
                {
                    for (int i = nextPos + 1; i < length; i++)
                    {
                        if (parameters[i] == '}')
                            break;

                        //--If there is no match to any closing brackets. return false (is not valid)
                        return false;
                    }
                }

                //--Checking "[]"
                if (parameters[pos] == '[' && parameters[nextPos] != ']')
                {
                    for (int i = nextPos + 1; i < length; i++)
                    {
                        if (parameters[i] == ']')
                            break;

                        //--If there is no match to any closing parenthesis. return false (is not valid)
                        return false;
                    }
                }
            }

            return true;
        }
    }

    [TestClass]
    public class ParametersValidationTests
    {
        IChallenge challenge;

        [TestInitialize]
        public void Setup()
        {
            challenge = new MyChallenge();
        }

        [TestMethod]
        public void ValidParameters()
        {
            var input = "(){[]}";

            var result = challenge.Execute(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidParametersMix()
        {
            var input = "(){[]}(((())))(){}[{(({}[]))}]";

            var result = challenge.Execute(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BadClosing()
        {
            var input = "()}[])";

            var result = challenge.Execute(input);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IncompleteClosing()
        {
            var input = "((()";

            var result = challenge.Execute(input);

            Assert.IsFalse(result);
        }

    }
}