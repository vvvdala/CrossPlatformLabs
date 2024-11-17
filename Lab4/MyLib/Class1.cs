using System.Diagnostics;

namespace MyLib
{
    public class Class1
    {
        public static void ExecuteLabs(string pathProject, string input = "", string output = "")
        {
            var argument = $"run --project \"{pathProject}\"";

            if (!string.IsNullOrEmpty(input))
            {
                argument += $" --input \"{input}\"";
            }

            if (!string.IsNullOrEmpty(output))
            {
                argument += $" --output \"{output}\"";
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = argument,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Console.WriteLine($"Executing command: dotnet {pathProject}");

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Console.WriteLine(e.Data);
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Console.WriteLine($"Error: {e.Data}");
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

            }


        }
    }
}
