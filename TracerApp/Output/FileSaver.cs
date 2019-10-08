using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TracerApp.Output
{
    class FileSaver:IOutputResult
    {
        private string saveadress;
        public void OutputTraceResult (string result)
        {
            using (StreamWriter sw = new StreamWriter(saveadress, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }
        FileSaver(string saveadress)
        {
            this.saveadress = saveadress;
        }
    }
}
