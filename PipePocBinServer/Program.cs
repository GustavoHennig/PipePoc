using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;

namespace PipePocBinServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServerBytes();
        }


        private static void StartServerBytes()
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message);

            int threadId = Thread.CurrentThread.ManagedThreadId;

            // Wait for a client to connect
            pipeServer.WaitForConnection();


            while (pipeServer.IsConnected)
            {

                StringBuilder sb = new StringBuilder();

                do
                {
                    var buffer = new byte[10];

                    AutoResetEvent manualResetEvent = new AutoResetEvent(false);

                    pipeServer.BeginRead(buffer, 0, buffer.Length, (st) => {
                        pipeServer.EndRead(st);
                        manualResetEvent.Set();
                    }, null);
                    manualResetEvent.WaitOne();

                    sb.Append(UnicodeEncoding.Default.GetString(buffer));
                }
                while (!pipeServer.IsMessageComplete);

                Console.WriteLine(sb.ToString());

                Console.Write("Ready: ");
                string readline;
                do
                {
                    readline = Console.ReadLine();
                } while (string.IsNullOrEmpty(readline));

                byte[] resp = UnicodeEncoding.Default.GetBytes(readline);

                pipeServer.Write(resp, 0, resp.Length);
            }
            pipeServer.Close();
        }
    }
}
