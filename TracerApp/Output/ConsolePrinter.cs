using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerApp.Output
{
     class ConsolePrinter:IOutputResult
    {
       
        public void OutputTraceResult(string result)
        {
            Console.WriteLine(result);
        }
        ConsolePrinter()
        {            
        }
    }
}
