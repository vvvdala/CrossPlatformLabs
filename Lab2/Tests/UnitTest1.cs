using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace cpp_lab_2_tests
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
        public void Test_PaintingMethod_DataFromTask()
        {
            var inputData = "3";
            var expectedOutputData = "12";

            File.WriteAllText(inputFile, inputData);
            cpp_lab_2.Program.PaintingMethod(inputFile,outputFile);

            var actualOutputData = File.ReadAllText(outputFile);
            
            Assert.AreEqual(expectedOutputData, actualOutputData);
        }

        [TestMethod]
        public void Test_PaintingMethod_OneTree()
        {
            var inputData = "1";

            var expectedOutputData = "3";

            File.WriteAllText(inputFile, inputData);
            cpp_lab_2.Program.PaintingMethod(inputFile, outputFile);

            var actualOutputData = File.ReadAllText(outputFile);

            Assert.AreEqual(expectedOutputData, actualOutputData);
        }

        [TestMethod]
        public void Test_PaintingMethod_ShouldReturnErrorMessage_Nless()
        {
            var inputData = "-10";

            File.WriteAllText(inputFile, inputData);
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));

            cpp_lab_2.Program.PaintingMethod(inputFile, outputFile);

            var expectedMessage = "n must be less than 50 and more than 0";

            Assert.IsTrue(consoleOutput.ToString().Contains(expectedMessage));
        }

        [TestMethod]
        public void Test_PaintingMethod_ShouldReturnErrorMessage_Nmore()
        {
            var inputData = "55";

            File.WriteAllText(inputFile, inputData);
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));

            cpp_lab_2.Program.PaintingMethod(inputFile, outputFile);

            var expectedMessage = "n must be less than 50 and more than 0";

            Assert.IsTrue(consoleOutput.ToString().Contains(expectedMessage));
        }

        [TestMethod]
        public void Test_PaintingMethod_ShouldReturnErrorMessage_NNotNumber()
        {
            var inputData = "abhnx";

            File.WriteAllText(inputFile, inputData);
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));

            cpp_lab_2.Program.PaintingMethod(inputFile, outputFile);

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