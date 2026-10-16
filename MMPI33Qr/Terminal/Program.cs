using Terminal.LabsTests.Interfaces;
using Terminal.LabsTests.Tests.TestLab1;
using Terminal.LabsTests.Tests.TestLab2;

class Program
{
    static void Main(string[] args)
    {
        ITest test = new TestLab2();
        while (true)
        {
            test.Test();
            Console.ReadLine();
        }
    }
}
