using Terminal.LabsTests.Interfaces;
using Terminal.LabsTests.Tests.TestLab1;
using Terminal.LabsTests.Tests.TestLab2;
using Terminal.LabsTests.Tests.TestLab3;

class Program
{
    static void Main(string[] args)
    {
        ITest test = new TestLab3();
        while (true)
        {
            test.Test();
            Console.ReadLine();
        }
    }
}
