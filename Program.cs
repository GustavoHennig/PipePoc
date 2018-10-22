using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PipePoc
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0] == "server")
                {
                    PipeServer.ServerThread(null);
                }
            }
            else
            {
                PipeClient.StartClient();
            }
        }
    }
}
