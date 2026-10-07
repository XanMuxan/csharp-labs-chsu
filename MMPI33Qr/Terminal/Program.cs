using Terminal.LabsTests.Interfaces;
using Terminal.LabsTests.Tests.TestLab1;

class Program
{
    static void Main(string[] args)
    {
        ITest test = new TestLab1();
        while (true)
        {
            test.Test();
        }
    }
}
