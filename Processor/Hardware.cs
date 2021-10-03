using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BetterConsoles.Tables;
using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;

namespace Processor
{

    public static class Hardware
    {

        private const int PARTITION = 4;

        public static readonly List<string> RegisterMemory = new List<string>(new string[18]);
        public static readonly List<string> MainMemory = new List<string>(new string[256]);

        public static void DisplayRegisterMemory()
        {
            List<string> generalRegister = RegisterMemory.GetRange(0, 16);
            List<string> specialRegister = RegisterMemory.GetRange(16, 2);
            Console.WriteLine("GENERAL REGISTERS: (" + generalRegister.Count(s => s != null) + "/" +
                              generalRegister.Capacity + ")");
            Table table = ConvertListToTable(generalRegister);
            Console.WriteLine(table);
            Console.WriteLine("SPECIAL REGISTERS: (" + specialRegister.Count(s => s != null) + "/" +
                              specialRegister.Capacity + ")");
            table = ConvertListToTable(specialRegister);
            Console.WriteLine(table);
        }

        public static void DisplayMainMemory()
        {
            Console.WriteLine("MAIN MEMORY: (" + MainMemory.Count(s => s != null) + "/" +
                              MainMemory.Capacity + ")");
            Table table = ConvertListToTable(MainMemory);
            Console.WriteLine(table);
        }

        private static Table ConvertListToTable(List<string> listToConvert)
        {
            TableBuilder tableBuilder = new TableBuilder(new CellFormat(Alignment.Left, Color.Black));
            for (int i = 0; i < PARTITION; i++) {
                tableBuilder.AddColumn("*", rowsFormat: new CellFormat(foregroundColor: Color.Lime));
                tableBuilder.AddColumn("Value", rowsFormat: new CellFormat(foregroundColor: Color.Khaki));
            }

            Table table = tableBuilder.Build();
            
            int notAdded = listToConvert.Capacity;
            // (float) used to prevent int division
            for (int i = 0; i < (float)listToConvert.Capacity / PARTITION; i++) {
                const int rowCapacity = 2 * PARTITION;
                string[] rowValues = new string[rowCapacity];
                
                if (notAdded / PARTITION < 1) {
                    for (int k = 0; k < notAdded; k++) {
                        int index = i * 4 + k;
                        rowValues[k * 2] = index.ToString();
                        
                        if (listToConvert[index] is null) {
                            rowValues[k * 2 + 1] = "(null)";
                            continue;
                        }
                        
                        rowValues[k * 2 + 1] = listToConvert[index];
                    }

                    for (int k = PARTITION - 1; k >= notAdded; k--) {
                        rowValues[k * 2] = "--";
                        rowValues[k * 2 + 1] = "--";
                    }
                    
                } else {
                    for (int k = 0; k < PARTITION; k++) {
                        int index = i * 4 + k;
                        rowValues[k * 2] = index.ToString();
                        
                        if (listToConvert[index] is null) {
                            rowValues[k * 2 + 1] = "(null)";
                            continue;
                        }
                        
                        rowValues[k * 2 + 1] = listToConvert[index];
                    }

                    notAdded -= PARTITION;
                }

                table.AddRow(rowValues);
            }
            table.Config = TableConfig.Unicode();

            return table;
        }

    }

}