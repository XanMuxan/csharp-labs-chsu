using Interactive.Models;
using Terminal.LabsTests.Interfaces;
using Terminal.LabsTests.Tests.TestLab1;
using Terminal.LabsTests.Tests.TestLab2;
using Terminal.LabsTests.Tests.TestLab3;
using Terminal.LabsTests.Tests.TestLab4;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main(string[] args)
    {
        if (args.Contains("test"))
        {
            ITest test;
            ITest[] tests = { new TestLab1(), new TestLab2(), new TestLab3(), new TestLab4() };
            Console.WriteLine("Введите номер нужного теста (0-Test1, 1-Test2, 2-Test3, 3-Test4): ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 0 && idx < tests.Length)
                {
                    test = tests[idx];
                    break;
                }
            }
            
            while (true)
            {
                test.Test();
                Console.ReadLine();
            }
        }
        else
        {
            TerminalUI terminal = new TerminalUI();
            terminal.Run();
        }
    }
}
