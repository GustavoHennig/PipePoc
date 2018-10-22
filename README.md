

### 3 Examples of IPC using named pipes


#### PipePocMsExample
This project is an example using the official Microsoft documentation.
https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-use-named-pipes-for-network-interprocess-communication

#### PipePocBinClient (Client and Server)

It uses PipeTransmissionMode.Message, communication occurs by buffered read/write (bytes);


#### PipePocStreamReader (Client and Server)

This project is a copy of PipePocBin but it uses StreamReader and StreamWriter helpers.