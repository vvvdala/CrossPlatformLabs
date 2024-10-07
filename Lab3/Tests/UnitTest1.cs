using System.Text;

namespace CPP_Lab_3_Tests
{
    [TestClass]
    public class UnitTest1
    {
        private string inputFile;
        private string outputFile;

        [TestInitialize]
        public void Init()
        {
            inputFile = Path.GetTempFileName();
            outputFile = Path.GetTempFileName();
        }

        [TestMethod]
        public void Test_FindMaxIndigo_DataFromTask()
        {
            var inputData = "3\n1 2\n2 3\nIB\nIV\nIB";
            var expectedOutputData = "2";

            File.WriteAllText(inputFile, inputData);
            CPP_Lab_3.Program.FindMaxIndigo(inputFile,outputFile);

            var actualOutputData = File.ReadAllText(outputFile).Trim();

            Assert.AreEqual(expectedOutputData, actualOutputData);
        }

        [TestMethod]
        public void Test_FindMaxIndigo_ImpossibleColoring()
        {
            var inputData = "3\n1 2\n2 3\nI\nI\nI";
            var expectedOutputData = "-1";

            File.WriteAllText(inputFile, inputData);
            CPP_Lab_3.Program.FindMaxIndigo(inputFile, outputFile);

            var actualOutputData = File.ReadAllText(outputFile).Trim();

            Assert.AreEqual(expectedOutputData, actualOutputData);
        }


        [TestMethod]
        public void Test_FindMaxIndigo_ShouldReturnErrorMessage_Nless()
        {
            var inputData = "-10\n1 2\n2 3\nIB\nIV\nIB";

            File.WriteAllText(inputFile, inputData);
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));

            CPP_Lab_3.Program.FindMaxIndigo(inputFile, outputFile);

            var expectedMessage = "n must be more than 0 and less than or equal 50000";

            Assert.IsTrue(consoleOutput.ToString().Contains(expectedMessage));
        }

        [TestMethod]
        public void Test_FindMaxIndigo_ShouldReturnErrorMessage_InvalidColor()
        {
            var inputData = "3\n1 2\n2 3\nPB\nIV\nIB";

            File.WriteAllText(inputFile, inputData);
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));

            CPP_Lab_3.Program.FindMaxIndigo(inputFile, outputFile);

            var expectedMessage = "Invalid color";

            Assert.IsTrue(consoleOutput.ToString().Contains(expectedMessage));
        }

        [TestMethod]
        public void Test_FindMaxIndigo_ShouldReturnErrorMessage_EdgeNotANumber()
        {
            var inputData = "3\na 2\n2 3\nIB\nIV\nIB";

            File.WriteAllText(inputFile, inputData);
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));

            CPP_Lab_3.Program.FindMaxIndigo(inputFile, outputFile);

            var expectedMessage = "Invalid input, not a number";

            Assert.IsTrue(consoleOutput.ToString().Contains(expectedMessage));
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(inputFile))
            {
                File.Delete(inputFile);
            }
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }
        }
    }
}