# FileDownloadApp

Create application that supports HTTP/2 GET requests and maps responses to files in some directory on host machine. I.e. upon making HTTP request with URL "http://localhost:1234/file.txt", it should return file from {applicationPath}/wwwroot/file.txt. Support for txt and png image files is enough. It should be able to verify the implementation by opening URL in Chrome and trying to download the file.
 
## Requirements:
-	Use .NET core
-	Easy to read, well formatted code
-	Support for concurrent requests

## Implemented

with C#, .NET Core 2.2, used middleware, routing, server Kestrel
 
