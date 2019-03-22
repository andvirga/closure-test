using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <summary>
        /// Contains the corresponding 'closure' char for each 'opener' character.
        /// </summary>
        Dictionary<char, char> closingCharsDictionary;

        public MyChallenge()
        {
            closingCharsDictionary = new Dictionary<char, char>
            {
                { '{', '}' },
                { '(', ')' },
                { '[', ']' }
            };
        }

        public bool Execute(string str)
        {
            var result = this.CheckClosure(str, 0);

            return result;
        }

        /// <summary>
        /// Checks recursively if there is a match (), {} or [], if positive deletes the match and keeps evaluating the substring
        /// </summary>
        /// <param name="str">Substring to evaluate</param>
        /// <param name="index">Starting point to evaluate</param>
        /// <returns>Is Valid</returns>
        private bool CheckClosure(string str, int index)
        {
            bool isValid = false;

            while(str.Length > 1 && index < str.Length-1)
            {
                //--First we check if the character is closed correctly
                var match = CheckMatch(str[index], str[index+1]);

                //--If the string length is 2 we have the last substring to check, then if it doesn't match, the whole string is invalid
                if (str.Length == 2 && match)
                {
                    isValid = true;
                    break;
                }
                else if (str.Length == 2 && !match)
                {
                    isValid = false;
                    break;
                }

                //--If there is a match (eg: {}), then we remove this substring from the whole string and keeps iterating
                if (match)
                {
                    str = str.Remove(index, 2); //--Removing 2 chars (initial and closure).
                    index = 0;                  //--Restart the index to keep checking the next substring
                }
                else
                {
                    index += 1;                //--Incremeting the index to keep checking the next substring
                    CheckClosure(str, index);  //--Calling recursively this function to evaluate the substring
                }
            }

            return isValid;
        }

        /// <summary>
        /// Checks if initial char and final char matches the corresponding closure character
        /// </summary>
        /// <param name="initial">Initial Char</param>
        /// <param name="final">Final Char</param>
        /// <returns>Match</returns>
        private bool CheckMatch(char initial, char final)
        {
            try
            {
                var closure = closingCharsDictionary[initial];

                return (final == closure);
            }
            catch
            {
                return false;
            }
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

        [TestMethod]
        public void AnotherÍnValidString()
        {
            var input = "{{(}})";

            var result = challenge.Execute(input);

            Assert.IsFalse(result);
        }
    }
}