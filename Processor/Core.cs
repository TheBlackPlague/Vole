using System;

namespace Processor
{

    public static class Core
    {

        public static void RunInstructionLine(string lineToRun)
        {
            InstructionSet.PassInstructionToHandler(InstructionSet.ConvertHexLineToBinary(lineToRun));
        }

        public static void Load(string argument)
        {
            int registerPointer = InstructionSet.ConvertBinToUInt(argument[..4]);
            int memoryPointer = InstructionSet.ConvertBinToUInt(argument[4..12]);

            Hardware.RegisterMemory[registerPointer] = Hardware.MainMemory[memoryPointer];
            Hardware.MainMemory[memoryPointer] = null;
        }

        public static void LoadC(string argument)
        {
            int registerPointer = InstructionSet.ConvertBinToUInt(argument[..4]);
            Hardware.RegisterMemory[registerPointer] = argument[4..12];
        }

        public static void Store(string argument)
        {
            Console.WriteLine("Works");
            int registerPointer = InstructionSet.ConvertBinToUInt(argument[..4]);
            int memoryPointer = InstructionSet.ConvertBinToUInt(argument[4..12]);
            Console.WriteLine(registerPointer);
            Console.WriteLine(memoryPointer);

            Hardware.MainMemory[memoryPointer] = Hardware.RegisterMemory[registerPointer];
            Hardware.RegisterMemory[registerPointer] = null;
        }

        public static void Move(string argument)
        {
            int sourceRegisterPointer = InstructionSet.ConvertBinToUInt(argument[4..8]);
            int destinationRegisterPointer = InstructionSet.ConvertBinToUInt(argument[8..12]);

            Hardware.RegisterMemory[destinationRegisterPointer] = Hardware.RegisterMemory[sourceRegisterPointer];
            Hardware.RegisterMemory[sourceRegisterPointer] = null;
        }

        public static void AddI(string argument)
        {
            int resultRegisterPointer = InstructionSet.ConvertBinToUInt(argument[..4]);
            int aRegisterPointer = InstructionSet.ConvertBinToUInt(argument[4..8]);
            int bRegisterPointer = InstructionSet.ConvertBinToUInt(argument[8..12]);

            int a = InstructionSet.ConvertBinToInt(Hardware.RegisterMemory[aRegisterPointer]);
            int b = InstructionSet.ConvertBinToInt(Hardware.RegisterMemory[bRegisterPointer]);

            Hardware.RegisterMemory[resultRegisterPointer] = InstructionSet.ConvertIntToBin(a + b);
        }

        public static void AddF(string argument)
        {
            // ... Need Implementation.
        }

        public static void Or(string argument)
        {
            int resultRegisterPointer = InstructionSet.ConvertBinToUInt(argument[..4]);
            int aRegisterPointer = InstructionSet.ConvertBinToUInt(argument[4..8]);
            int bRegisterPointer = InstructionSet.ConvertBinToUInt(argument[8..12]);

            bool a = InstructionSet.ConvertBinToBool(Hardware.RegisterMemory[aRegisterPointer]);
            bool b = InstructionSet.ConvertBinToBool(Hardware.RegisterMemory[bRegisterPointer]);

            Hardware.RegisterMemory[resultRegisterPointer] = InstructionSet.ConvertBoolToBin(a || b);
        }
        
        public static void And(string argument)
        {
            int resultRegisterPointer = InstructionSet.ConvertBinToUInt(argument[..4]);
            int aRegisterPointer = InstructionSet.ConvertBinToUInt(argument[4..8]);
            int bRegisterPointer = InstructionSet.ConvertBinToUInt(argument[8..12]);

            bool a = InstructionSet.ConvertBinToBool(Hardware.RegisterMemory[aRegisterPointer]);
            bool b = InstructionSet.ConvertBinToBool(Hardware.RegisterMemory[bRegisterPointer]);

            Hardware.RegisterMemory[resultRegisterPointer] = InstructionSet.ConvertBoolToBin(a && b);
        }
        
        public static void ExclusiveOr(string argument)
        {
            int resultRegisterPointer = InstructionSet.ConvertBinToUInt(argument[..4]);
            int aRegisterPointer = InstructionSet.ConvertBinToUInt(argument[4..8]);
            int bRegisterPointer = InstructionSet.ConvertBinToUInt(argument[8..12]);

            bool a = InstructionSet.ConvertBinToBool(Hardware.RegisterMemory[aRegisterPointer]);
            bool b = InstructionSet.ConvertBinToBool(Hardware.RegisterMemory[bRegisterPointer]);

            Hardware.RegisterMemory[resultRegisterPointer] = InstructionSet.ConvertBoolToBin(a ^ b);
        }
        
        public static void Rotate(string argument)
        {
            int registerPointer = InstructionSet.ConvertBinToUInt(argument[..4]);
            int iterationCount = InstructionSet.ConvertBinToUInt(argument[4..8]);
            
            string byteToRotate = Hardware.RegisterMemory[registerPointer];
            Hardware.RegisterMemory[registerPointer] =
                byteToRotate[^iterationCount..] + byteToRotate[..^iterationCount];
        }

        public static void Jump(string argument)
        {
            // ... Need Implementation.
        }

        public static void Halt()
        {
            Console.WriteLine("HALT - CURRENT MEMORY DUMPED");
            Console.WriteLine("------");
            Hardware.DisplayRegisterMemory();
            Hardware.DisplayMainMemory();
        }

    }

}