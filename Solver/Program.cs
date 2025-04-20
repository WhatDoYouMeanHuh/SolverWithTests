using Solver;

if (args.Length == 2)
{
    // Файловый режим
    using var input = new StreamReader(args[0]);
    using var output = new StreamWriter(args[1]);
    Solver.Solver.Do(input, output);
}
else
{
    // Консольный режим
    Solver.Solver.Do(Console.In, Console.Out);
}