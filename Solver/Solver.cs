namespace Solver;

public static class Solver
{
    public static void Do(TextReader input, TextWriter output)
    {
        int t = int.Parse(input.ReadLine() ?? "0");
        
        for (int i = 0; i < t; i++)
        {
            int n = int.Parse(input.ReadLine() ?? "0");
            long product = 1;
            
            for (int j = 0; j < n; j++)
            {
                long num = long.Parse(input.ReadLine() ?? "1");
                product *= num;
            }
            
            output.WriteLine(product);
        }
    }
}