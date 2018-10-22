

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

https://github.com/GustavoHennig/PipePoc/blob/9dcb2e19fb2737d4ecadbab46a0e544dae3ed69d/PipePocBinClient/Program.cs#L23-L58


#### The Server

https://github.com/GustavoHennig/PipePoc/blob/9dcb2e19fb2737d4ecadbab46a0e544dae3ed69d/PipePocBinServer/Program.cs#L31-L62