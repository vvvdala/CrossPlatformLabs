using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;

namespace cpp_lab_1_tests
{
    [TestClass]
    public class UnitTest1
    {
        private string inputName;
        private string outputName;

        [TestInitialize] 
        public void Setup()
        {
            inputName = Path.GetTempFileName();
            outputName = Path.GetTempFileName();
        }
        [TestMethod]
        public void Test_FinfMaxFriends_DataFromTask()
        {
            var inputData = "5 1\n1 3\n6 -5\n6 -4\n2 2\n2 -1";
            var expctedOutputData = "4\n1 4 3 5";

            File.WriteAllText(inputName, inputData);
            cpp_lab_1.Program.FindMaxFriends(inputName, outputName);

            var actualOutputData = File.ReadAllText(outputName);

            Assert.AreEqual(expctedOutputData, actualOutputData);
        }

        [TestMethod]
        public void Test_FindMaxFriends_1Friend()
        {
            var inputData = "1 10\n5 10";
            var expctedOutputData = "1\n1";

            File.WriteAllText(inputName, inputData);
            cpp_lab_1.Program.FindMaxFriends(inputName, outputName);

            var actualOutputData = File.ReadAllText(outputName);


            Assert.AreEqual(expctedOutputData, actualOutputData);
        }

        [TestMethod]
        public void Test_FindMaxFriends_ShouldReturnErrorMessage_nLessOrMore()
        {
            var inputData = "1001 10\n5 10";

            File.WriteAllText(inputName, inputData);
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));

            cpp_lab_1.Program.FindMaxFriends(inputName, outputName);

            var expectedMessage = "n cant be less than 0 and more than 1000";

            Assert.IsTrue(consoleOutput.ToString().Contains(expectedMessage));
        }

        [TestMethod]
        public void Test_FindMaxFriends_ShouldReturnErrorMessage_nNotEqual()
        {
            var inputData = "2 10\n1 3\n6 -5\n6 -4\n2 2\n2 -1";

            File.WriteAllText(inputName, inputData);
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));

            cpp_lab_1.Program.FindMaxFriends(inputName, outputName);

            var expectedMessage = "The number of friends does not match the value of n";

            Assert.IsTrue(consoleOutput.ToString().Contains(expectedMessage));
        }

        [TestMethod]
        public void Test_FindMaxFriends_AllPositive()
        {
            var inputData = "4 2\n1 3\n2 4\n3 2\n4 1\n";
            var expctedOutputData = "4\n1 2 3 4";

            File.WriteAllText(inputName, inputData);
            cpp_lab_1.Program.FindMaxFriends(inputName, outputName);

            var actualOutputData = File.ReadAllText(outputName);


            Assert.AreEqual(expctedOutputData, actualOutputData);
        }

        [TestMethod]
        public void Test_FindMaxFriends_AllNegative()
        {
            var inputData = "3 10\n3 -2\n6 -3\n9 -1";
            var expctedOutputData = "2\n1 2";

            File.WriteAllText(inputName, inputData);
            cpp_lab_1.Program.FindMaxFriends(inputName, outputName);

            var actualOutputData = File.ReadAllText(outputName);


            Assert.AreEqual(expctedOutputData, actualOutputData);
        }


        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(inputName))
            {
                File.Delete(inputName);
            }
            if (File.Exists(outputName))
            {
                File.Delete(outputName);
            }
        }
    }
}