using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace MyLibrary
{
    public class LabsLibrary
    {
        public static string ExecuteLabs(string pathProject)
        {
            string result = string.Empty;

            string inputFilePath = Environment.GetEnvironmentVariable("INPUT_FILE");
            string outputFilePath = Environment.GetEnvironmentVariable("OUTPUT_FILE");

            if (string.IsNullOrEmpty(inputFilePath) || string.IsNullOrEmpty(outputFilePath))
            {
                return "Не задані шляхи до тимчасових файлів.";
            }

            var argument = $"run --project \"{pathProject}\" --input \"{inputFilePath}\" --output \"{outputFilePath}\"";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = argument,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            try
            {
                process.Start();
                result += "Building...\n ";
                result += process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                result = $"Error: {ex.Message}";
            }

            return result;
        }


    }
}
