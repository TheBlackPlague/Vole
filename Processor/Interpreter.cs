#nullable enable
using System.IO;

namespace Processor
{

    public class Interpreter
    {

        private readonly StreamReader File;

        public Interpreter(string filePath)
        {
            File = new StreamReader(@filePath);
        }

        public void Run()
        {
            string? line;
            while ((line = File.ReadLine()) != null) {
                Core.RunInstructionLine(line[2..]);
            }
        }

    }

}