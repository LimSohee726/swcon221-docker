using System.Net;
using System.Net.Sockets;
using System.Text;

var listener = new TcpListener(IPAddress.Any, 9000);
listener.Start();
Console.WriteLine("Serving HTTP on port 9000 . . .");

while (true)
{
    using var client = listener.AcceptTcpClient();
    using var stream = client.GetStream();

    var buffer = new byte[1024];
    int n = stream.Read(buffer, 0, buffer.Length);
    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, n));

    var body = "Hello world from c#!";
    var response =
        "HTTP/1.1 200 OK\r\n" +
        "Content-Type: text/plain; charset=utf-8|r|n" +
        $"Content-Length: {Encoding.UTF8.GetByteCount(body)}\r\n" +
        "\r\n" +
        body;

    stream.Write(Encoding.UTF8.GetBytes(response));
}