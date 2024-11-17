
namespace cpp_lab_1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string nameInput = "INPUT.TXT";
            string nameOutput = "OUTPUT.TXT";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--input" && i + 1 < args.Length)
                {
                    nameInput = args[i + 1];
                }
                else if (args[i] == "--output" && i + 1 < args.Length)
                {
                    nameOutput = args[i + 1];
                }
            }
            try
            {
                FindMaxFriends(nameInput, nameOutput);
            }
            catch(Exception e)
            { 
                Console.WriteLine("Помилка:" + e.Message);
            }
        }

        public static void FindMaxFriends(string input, string output)
        {
            var inputFile = File.ReadAllLines(input);
            var firstLine = inputFile[0].Split();

            if(firstLine.Length > 2)
            {
                Console.WriteLine("First line can contain only 2 numebers");
                return;
            }

            if (!int.TryParse(firstLine[0], out int _))
            {
                Console.WriteLine("n must be number");
            }

            int n = int.Parse(firstLine[0]);

            if (n < 1 || n > 1000)
            {
                Console.WriteLine("n cant be less than 0 and more than 1000");
                return;
            }

            if (inputFile.Length - 1 != n)
            {
                Console.WriteLine("The number of friends does not match the value of n");
                return;
            }

            int currentAuthority = int.Parse(firstLine[1]);

            var posFriends = new List<Friend>();
            var negFiends = new List<Friend>();

            for (int i = 1; i <= n; i++)
            {
                var friendData = inputFile[i].Split();
                var a = int.Parse(friendData[0]);
                var b = int.Parse(friendData[1]);
                var friend = new Friend { A = a, B = b, Id = i };

                if (b > 0)
                {
                    posFriends.Add(friend);
                }
                else
                {
                    negFiends.Add(friend);
                }
            }

            posFriends = posFriends.OrderBy(f => f.A).ToList();

            var result = new List<int>();

            foreach (var friend in posFriends)
            {
                if (currentAuthority >= friend.A)
                {
                    result.Add(friend.Id);
                    currentAuthority += friend.B;
                }
            }

            var variants = new SortedSet<Friend>(Comparer<Friend>.Create((f1, f2) => f2.B.CompareTo(f1.B)));

            foreach (var friend in negFiends)
            {
                if (currentAuthority >= friend.A)
                {
                    result.Add(friend.Id);
                    currentAuthority += friend.B;
                    variants.Add(friend);
                }
                else
                {
                    if (variants.Any() && variants.Max.B < friend.B && currentAuthority - variants.Max.B >= friend.A)
                    {
                        var worst = variants.Max;
                        result.Remove(worst.Id);
                        variants.Remove(worst);
                        currentAuthority -= worst.B;

                        result.Add(friend.Id);
                        currentAuthority += friend.B;
                        variants.Add(friend);
                    }

                }
            }

            File.WriteAllText(output, $"{result.Count}\n{string.Join(" ", result)}");

        }
    }
}