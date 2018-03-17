using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeshit
{
    public class Settings
    {
        private const string settingsFileName = "timeshit.config";

        private const int defaultInterval = 30;
        private const string defaultPath = "C:\\users\\tberanek\\desktop\\timeshit.txt";

        private int interval = defaultInterval;
        private string logFile = defaultPath;

        public int Interval { get; set; }
        public string LogFile { get; set; }

        private static Settings instance;

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                    instance = new Settings();
                return instance;
            }
        }

        private Settings()
        {
            LoadSettingsFromFile();
        }

        public void Update()
        {
            SaveSettingsToFile();
        }

        private void LoadSettingsFromFile()
        {
            if (! File.Exists(settingsFileName))
            {
                using (StreamWriter sw = File.CreateText(settingsFileName))
                {
                    sw.WriteLine(defaultInterval);
                    sw.WriteLine(defaultPath);
                    Interval = defaultInterval;
                    LogFile = defaultPath;
                }
            }
            else
            {
                using (StreamReader sr = File.OpenText(settingsFileName))
                {
                    int interval = defaultInterval;
                    Int32.TryParse(sr.ReadLine(), out interval);
                    Interval = interval;
                    LogFile = sr.ReadLine();
                    if (!File.Exists(LogFile))
                        LogFile = defaultPath;
                }
            }
        }

        private void SaveSettingsToFile()
        {
            using (StreamWriter sw = new StreamWriter(settingsFileName, false))
            {
                sw.WriteLine(Interval.ToString());
                sw.WriteLine(LogFile);
            }
        }
    }
}
