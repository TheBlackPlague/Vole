using System;
using System.Linq;

namespace Processor
{

    public static class InstructionSet
    {

        public static int ConvertBinToUInt(string binary)
        {
            int x = 0;
            for (int i = 0; i < binary.Length; i++) {
                int pow = (int)Math.Pow(2, binary.Length - i - 1);
                int binaryAtI = int.Parse(binary[i].ToString());
                x += binaryAtI * pow;
            }

            return x;
        }

        public static string ConvertUIntToBin(int x)
        {
            string binary = string.Empty;
            while (x != 0) {
                int z = x % 2;
                x = (x / 2) - (z / 2);
                binary += z.ToString();
            }

            return string.Join(string.Empty, binary.Reverse());
        }

        public static int ConvertBinToInt(string binary)
        {
            int sign = (int)Math.Pow(-1, int.Parse(binary[0].ToString()));
            int uInt = ConvertBinToUInt(binary[1..8]);

            return sign * uInt;
        }

        public static string ConvertIntToBin(int x)
        {
            string binary = "0";
            if (x < 0) binary = "1";

            string uBinary = ConvertUIntToBin(Math.Abs(x));
            while (uBinary.Length < 7) {
                uBinary = "0" + uBinary;
            }

            binary += uBinary;

            return binary;
        }

        public static string ConvertHexLineToBinary(string lineToRead)
        {
            string instruction = string.Join(
                string.Empty, 
                lineToRead.Select(
                    c => Convert.ToString(
                        Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                    )
                );

            if (instruction.Length != 16) throw new FormatException("The instruction is not 2 bytes.");

            return instruction;
        }
        
        public static bool ConvertBinToBool(string binary) => !binary.Contains("1");

        public static string ConvertBoolToBin(bool value) => value ? "00000000" : "00000001";

        public static void PassInstructionToHandler(string instruction)
        {
            int opCode = ConvertBinToUInt(instruction[..4]);
            string argument = instruction[4..16];
            switch (opCode) {
                case 1: // LOAD
                    Core.Load(argument);
                    break;
                case 2: // LOAD_C
                    Core.LoadC(argument);
                    break;
                case 3: // STORE
                    Core.Store(argument);
                    break;
                case 4: // MOVE
                    Core.Move(argument);
                    break;
                case 5: // ADD_I
                    Core.AddI(argument);
                    break;
                case 6: // ADD_F
                    Core.AddF(argument);
                    break;
                case 7: // OR
                    Core.Or(argument);
                    break;
                case 8:
                    Core.And(argument);
                    break;
                case 9:
                    Core.ExclusiveOr(argument);
                    break;
                case 10:
                    Core.Rotate(argument[..4] + argument[8..12]);
                    break;
                case 11:
                    Core.Jump(argument);
                    break;
                case 12:
                    Core.Halt();
                    break;
            }
        }

    }

}