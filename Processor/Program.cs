namespace Processor
{

    internal static class Program
    {

        private static void Main(string[] args)
        {
            Interpreter interpreter = new("VoleCode.txt");
            interpreter.Run();
        }

    }

}