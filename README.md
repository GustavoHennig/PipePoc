

### 3 Examples of IPC using named pipes


#### PipePocMsExample
This project is an example using the official Microsoft documentation.

https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-use-named-pipes-for-network-interprocess-communication

#### PipePocBinClient (Client and Server)

It uses PipeTransmissionMode.Message, communication occurs by buffered read/write (bytes);


#### PipePocStreamReader (Client and Server)

This project is a copy of PipePocBin but it uses StreamReader and StreamWriter helpers.


### My findings...

This was the example that best fit the needs of my project, and was quite simple:

#### The Client


```c#
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
```
https://github.com/GustavoHennig/PipePoc/blob/9dcb2e19fb2737d4ecadbab46a0e544dae3ed69d/PipePocBinClient/Program.cs#L23-L58


#### The Server

```c#
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
```

https://github.com/GustavoHennig/PipePoc/blob/9dcb2e19fb2737d4ecadbab46a0e544dae3ed69d/PipePocBinServer/Program.cs#L31-L62
