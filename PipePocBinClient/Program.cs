using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace PipePocBinClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(2000);
            StartClientBytes();
        }


        private static void StartClientBytes()
        {

            NamedPipeClientStream pipeClient =
                        new NamedPipeClientStream(".", "testpipe",
                            PipeDirection.InOut, PipeOptions.None,
                            TokenImpersonationLevel.Impersonation);

            pipeClient.Connect();
            pipeClient.ReadMode = PipeTransmissionMode.Message;


            while (pipeClient.IsConnected)
            {

                Console.Write("Ready: ");
                string readline = Console.ReadLine();


                byte[] resp = UnicodeEncoding.Default.GetBytes(readline);

                pipeClient.Write(resp, 0, resp.Length);


                StringBuilder sb = new StringBuilder();

                do
                {
                    var buffer = new byte[10];
                    var readBytes = pipeClient.Read(buffer, 0, buffer.Length);
                    sb.Append(UnicodeEncoding.Default.GetString(buffer));
                }
                while (!pipeClient.IsMessageComplete);

                Console.WriteLine(sb.ToString().TrimEnd('\0'));


            }
            pipeClient.Close();
        }
    }
}
