using System;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using MyLib;

namespace App
{
    [Command(Name = "App", Description = "Console application to manage lab projects")]
    [Subcommand(typeof(RunCommand), typeof(SetPathCommand), typeof(VersionCommand))]
    internal class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
        }
    }

    [Command(Name = "version", Description = "Displays the version information")]
    class VersionCommand
    {
        private void OnExecute()
        {
            Console.WriteLine("Author: Veronika Manoshyna");
            Console.WriteLine("Version: 1.0.0");
        }
    }

    [Command(Name = "run", Description = "Runs a specific lab with optional input and output files")]
    class RunCommand
    {
        [Argument(0, Description = "The lab to run (e.g., Lab1, Lab2, Lab3)")]
        public string LabName { get; set; }

        [Option("-I|--input", Description = "Specifies the input file")]
        public string InputFile { get; set; } = "INPUT.TXT";

        [Option("-o|--output", Description = "Specifies the output file")]
        public string OutputFile { get; set; } = "OUTPUT.TXT";

        private void OnExecute()
        {
            if (string.IsNullOrEmpty(LabName))
            {
                Console.WriteLine("Please specify a lab (e.g., Lab1, Lab2, Lab3)");
                return;
            }

            string labPath = Environment.GetEnvironmentVariable("LAB_PATH");
            string pathProject = $"C:\\Users\\VeronikaManoshyna\\Desktop\\унік\\3 курс\\кпп\\repo\\{LabName}\\App\\App.csproj";

            string inputPath = !string.IsNullOrEmpty(labPath) ? Path.Combine(labPath, InputFile) : InputFile;
            string outputPath = !string.IsNullOrEmpty(labPath) ? Path.Combine(labPath, OutputFile) : OutputFile;

            Class1.ExecuteLabs(pathProject, inputPath, outputPath);
            Console.WriteLine($"The {LabName} was launched");
        }
    }

    [Command(Name = "set-path", Description = "Sets the path to the directory containing input and output files")]
    class SetPathCommand
    {
        [Option("-d|--path", Description = "The path to the directory")]
        public string Path { get; set; }

        private void OnExecute()
        {
            if (string.IsNullOrEmpty(Path))
            {
                Console.WriteLine("Error: You must specify a path with -d or --path");
                return;
            }

            Environment.SetEnvironmentVariable("LAB_PATH", Path);
            Console.WriteLine($"Path '{Path}' has been set as the 'LAB_PATH' environment variable");
        }
    }
}
