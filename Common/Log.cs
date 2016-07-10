using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Common
{
    public static class Log
    {

        static private EventLog evtLog;

        public static EventLog GetLogObject(String AppName, String ModuleName)
        {
            evtLog = new EventLog();
            if (!EventLog.SourceExists(AppName))
            {
                EventLog.CreateEventSource(AppName, ModuleName);
            }
            evtLog.Source = AppName;
            evtLog.Log = ModuleName;

            return evtLog;
        }

        static bool IsFileLogInit = false;
        static String LogDirectory = "logs";
        static Queue<string> Logqu = new Queue<string>();

        public static void InitFileLog()
        {
            IsFileLogInit = true;
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
            Logqu = new Queue<string>();
            Task t = new Task(() => LogWriter());
            t.Start();
        }

        public static void WriteFilelog(String AppName, String ModuleName, String Message)
        {
            if (!IsFileLogInit)
            {
                InitFileLog();
            }
            Message = DateTime.Now.ToString("HH:mm:ss") + " : " + ModuleName + " : " + Message;
            Logqu.Enqueue(Message);

        }

        private static void LogWriter()
        {
            while (true)
            {
                if (Logqu.Count > 0)
                {
                    String LogFileName = LogDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                    StreamWriter sw = new StreamWriter(LogFileName, true);
                    while (Logqu.Count > 0)
                    {
                        sw.WriteLine(Logqu.Dequeue());

                    }
                    sw.Close();
                }
                System.Threading.Thread.Sleep(199);
            }
        }
    }


    }
