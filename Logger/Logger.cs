using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public class Logger
    {
        public string FilePath { get; set; }
        public bool CreateConsoleOutput { get; set; }
        public LogLevel CaptureLevel { get; set; } 

        public Logger(string path, string filename, LogLevel logLevel = 0, bool ConsoleOutput = false) 
        {
            this.FilePath = Path.Combine(path,filename);
            this.CaptureLevel = logLevel;
            this.CreateConsoleOutput = ConsoleOutput;

            if (File.Exists(this.FilePath) == false)
            {
                File.Create(this.FilePath).Close();
            }
        }

        public void CreateLogEntry(string Message, LogLevel logLevel, bool withTime, bool ConsoleOutputOverride = false)
        {
            try
            {
                if (logLevel < CaptureLevel) { return; }

                Message = $"[{logLevel}] {Message}";
                if (withTime == true)
                {
                    Message = $"[{DateTime.Now.ToShortTimeString()}]{Message}";
                }
                if (this.CreateConsoleOutput == true && ConsoleOutputOverride == false)
                {
                    Console.WriteLine(Message);
                }

                using (StreamWriter sw = new StreamWriter(this.FilePath, true))
                {
                    sw.WriteLine(Message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public enum LogLevel
        {
            Debug = 0,
            Info = 1,
            Warn = 2,
            Error = 3
        }
    }
}
