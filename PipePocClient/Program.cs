using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace PipePocClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(2000);
            StartClientSR();
        }


        private static void StartClientSR()
        {

            NamedPipeClientStream pipeClient =
                        new NamedPipeClientStream(".", "testpipe",
                            PipeDirection.InOut, PipeOptions.None,
                            TokenImpersonationLevel.Impersonation);

            Console.WriteLine("Connecting to server...\n");
            pipeClient.Connect();

            StreamReader sr = new StreamReader(pipeClient);
            StreamWriter sw = new StreamWriter(pipeClient);
            sw.AutoFlush = true;


            while (pipeClient.IsConnected)
            {
                Console.Write("Ready: ");

                string readline = Console.ReadLine();

                sw.WriteLine(readline);

                string serverLine = sr.ReadLine();
                Console.WriteLine(serverLine);

            }
            pipeClient.Close();
        }

    }
}
