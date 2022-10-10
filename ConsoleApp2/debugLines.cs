using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleProj
{
   public static class debugLines
    {
        public static List<string> dataLines;
        public static bool enables = true;
        public static void writeLine(string input)
        {
            if(dataLines == null)
            {
                dataLines = new List<string>();
            }
            dataLines.Add(input);
        }
        public static List<string> returnList()
        {
            if (dataLines == null)
            {
                dataLines = new List<string>();
            }
            return dataLines;
        }
        public static void clear()
        {
            dataLines = null;
        }
    }
}
