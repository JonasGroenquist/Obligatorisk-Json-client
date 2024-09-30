using JsonOPG;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;

Console.WriteLine("This is the TCP Client");

bool keepSending = true;

TcpClient socket = new TcpClient("127.0.0.1", 7);


NetworkStream ns = socket.GetStream();
StreamReader reader = new StreamReader(ns);
StreamWriter writer = new StreamWriter(ns);

while (keepSending)
{

    Console.WriteLine("Input message, Example: add 15 15");
    string message = Console.ReadLine().ToLower();
    string[] parts = message.Split(' ');
    if (message.ToLower() == "stop")
    {
        writer.WriteLine(message);
        keepSending = false;
    }
    if (parts.Length == 3 &&
        int.TryParse(parts[1], out int num1) &&
        int.TryParse(parts[2], out int num2))
    {
        
        var method = parts[0];

        Input jsonMsg = new Input(method, num1, num2);
        string jsonMessage = JsonSerializer.Serialize(jsonMsg);

        writer.WriteLine(jsonMessage);
    }

    writer.Flush();
    string response = reader.ReadLine();

    Console.WriteLine("Server response: " + response);

    if (message.ToLower() == "stop")
    {
        keepSending = false;
    }
}