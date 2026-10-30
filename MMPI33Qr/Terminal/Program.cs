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
            int testNum = int.Parse(Console.ReadLine());

            test = tests[testNum];
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
