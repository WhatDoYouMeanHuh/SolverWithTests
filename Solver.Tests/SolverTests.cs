using Solver;
using Xunit;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Solver.Tests;

public class SolverTests
{
    private const string TestDataFolder = "TestData";
    
    public static IEnumerable<object[]> GetTestFilePairs()
    {
        var testDataDir = Path.Combine(Directory.GetCurrentDirectory(), TestDataFolder);
        
        if (!Directory.Exists(testDataDir))
            yield break;

        // Находим все файлы без расширений (основные тесты)
        var inputFiles = Directory.GetFiles(testDataDir)
            .Where(f => !Path.GetFileName(f).Contains('.'))
            .Select(Path.GetFileName)
            .OrderBy(f => f)
            .ToList();

        foreach (var inputFile in inputFiles)
        {
            var answerFile = $"{inputFile}a";
            var answerPath = Path.Combine(testDataDir, answerFile);
            
            if (File.Exists(answerPath))
            {
                yield return new object[] { inputFile, answerFile };
            }
        }
    }

    [Theory]
    [MemberData(nameof(GetTestFilePairs))]
    public void Solve_WithTestFiles_ProducesCorrectOutput(string inputFile, string expectedOutputFile)
    {
        // Arrange
        var inputPath = Path.Combine(TestDataFolder, inputFile);
        var expectedOutputPath = Path.Combine(TestDataFolder, expectedOutputFile);
        
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input file not found: {inputPath}");
        
        if (!File.Exists(expectedOutputPath))
            throw new FileNotFoundException($"Expected output file not found: {expectedOutputPath}");

        using var input = new StreamReader(inputPath);
        using var output = new StringWriter();
        var expectedOutput = File.ReadAllLines(expectedOutputPath);

        // Act
        Solver.Do(input, output);
        var actualOutput = output.ToString()
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        // Assert
        Assert.Equal(expectedOutput.Length, actualOutput.Length);
        for (int i = 0; i < expectedOutput.Length; i++)
        {
            Assert.Equal(expectedOutput[i], actualOutput[i]);
        }
    }

    //[Fact]
    //public void AtLeastOneTestFilePairExists()
    //{
    //    var testFilePairs = GetTestFilePairs().ToList();
    //    Assert.NotEmpty(testFilePairs);
    //}
}