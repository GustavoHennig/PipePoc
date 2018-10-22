using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;

namespace PipePocServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServerSR();
        }

        private static void StartServerSR()
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message);

            int threadId = Thread.CurrentThread.ManagedThreadId;

            // Wait for a client to connect
            pipeServer.WaitForConnection();
            StreamReader sr = new StreamReader(pipeServer);
            StreamWriter sw = new StreamWriter(pipeServer);
            sw.AutoFlush = true;


            while (pipeServer.IsConnected)
            {


                string serverLine = sr.ReadLine();
                Console.WriteLine(serverLine);

                Console.Write("Ready: ");
                string readline = Console.ReadLine();

                sw.WriteLine(readline);


            }
            pipeServer.Close();
        }


    }
}
