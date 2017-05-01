using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace movieapplication
{
    static class Logger
    {
        public static void Log (string message)
        {
            //if (File.Exists("log.txt"))
            //{
                using (FileStream fs = new FileStream("log.txt", FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine($"{DateTime.Now} - {message}");
                    }
                }
            //}
        }
    }
}
