
namespace cpp_lab_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = "INPUT.TXT";
            string output = "OUTPUT.TXT";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--input" && i + 1 < args.Length)
                {
                    input = args[i + 1];
                }
                else if (args[i] == "--output" && i + 1 < args.Length)
                {
                    output = args[i + 1];
                }
            }

            try
            {
                PaintingMethod(input, output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Помилка:" + e.Message);
            }

        }

        static public void PaintingMethod(string input, string output)
        {
            var inputFile = File.ReadAllLines(input);
            
            if (!int.TryParse(inputFile[0], out _))
            {
                Console.WriteLine("Invalid input, not a number");
                return;
            }

            int n = int.Parse(inputFile[0]);

            if (n > 50 || n < 0)
            {
                Console.WriteLine("n must be less than 50 and more than 0");
                return;
            }

            int res = 3;

            for (int i = 0; i < n - 1; i++)
            {
                res *= 2;
            }

            File.WriteAllText(output, res.ToString());

        }
       
    }
}