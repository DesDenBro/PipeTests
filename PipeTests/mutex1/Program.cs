using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using ThreadState = System.Threading.ThreadState;

namespace Mutex1
{
    static class Logger
    {

        public static void WriteWithWhile(StreamWriter filestr, int pid)
        {
            filestr.WriteLine($"[NOW PROCESS {pid}]");
            while (true)
            {
                filestr.WriteLine($"\tProcess with id {pid} write in file");
                Thread.Sleep(1000);
            }
        }
    }

    class Program
    {
        const string SHARED_MUTEX_NAME = "textTest";
        static Thread customThread;

        static void Main(string[] args)
        { 
            int pid = Process.GetCurrentProcess().Id;
            using (Mutex mtx = new Mutex(false, SHARED_MUTEX_NAME))
            {
                while (true)
                {
                    Console.WriteLine($"Press any key to write text in the file [pid={pid}]");
                    Console.ReadKey();

                    while (!mtx.WaitOne(1000))
                    {
                        Console.WriteLine($"Process {pid} is waiting to write...");
                    }

                    Console.WriteLine($"Process {pid} is writing in file...");
                    using (StreamWriter filestr = File.AppendText($@"{AppDomain.CurrentDomain.BaseDirectory}\text.txt"))
                    {
                        customThread = new Thread(() => Logger.WriteWithWhile(filestr, pid));
                        customThread.Start();
                        Console.ReadKey();
                        customThread.Abort();
                    }

                    while (customThread.IsAlive) { }
                    mtx.ReleaseMutex();

                    Console.WriteLine($"Process {pid} stop write text...");
                }
            }
        }
    }
}
