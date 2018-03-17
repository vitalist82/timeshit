using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeshit
{
    public class Storage
    {
        public void AddRecord(DateTimeOffset dateTime, string text)
        {
            using (StreamWriter sw = File.AppendText(Settings.Instance.LogFile))
            {
                sw.WriteLine(string.Format("{0} - {1}", dateTime, text));
                sw.Close();
            }
        }
    }
}
