using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

class TcpClientApp
{
    static void Main()
    {
        string serverIp = "127.0.0.1";
        int port = 4444;

        try
        {
            TcpClient client = new TcpClient(serverIp, port);
            Console.WriteLine($"Connected to server at {serverIp}:{port}.");

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            Thread readThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        string serverMessage = reader.ReadLine();
                        if (serverMessage == null) break;
                        Console.WriteLine(serverMessage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading from server: {ex.Message}");
                }
            });
            readThread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                if (message == "exit") break;

                writer.WriteLine(message);
            }

            client.Close();
            Console.WriteLine("Disconnected from server.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
