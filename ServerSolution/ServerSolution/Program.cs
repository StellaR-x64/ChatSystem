using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class TcpServer
{
    private static readonly List<TcpClient> connectedClients = new List<TcpClient>();

    static void Main()
    {
        int port = 4444;
        TcpListener server = new TcpListener(IPAddress.Any, port);

        server.Start();
        Console.WriteLine($"Server started on port {port}.");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("A client connected.");

            lock (connectedClients)
            {
                connectedClients.Add(client);
            }

            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.Start();
        }
    }

    private static void HandleClient(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            writer.WriteLine("Welcome to the chat! Type 'exit' to disconnect.");

            while (true)
            {
                string message = reader.ReadLine();
                if (message == null || message.ToLower() == "exit") break;

                Console.WriteLine($"Message from client: {message}");

                BroadcastMessage($"Client says: {message}", client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error with client: {ex.Message}");
        }
        finally
        {
            lock (connectedClients)
            {
                connectedClients.Remove(client);
            }
            client.Close();
            Console.WriteLine("Client disconnected.");
        }
    }

    private static void BroadcastMessage(string message, TcpClient sender)
    {
        lock (connectedClients)
        {
            foreach (TcpClient client in connectedClients)
            {
                if (client != sender)
                {
                    try
                    {
                        StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8) { AutoFlush = true };
                        writer.WriteLine(message);
                    }
                    catch (Exception)
                    {
                     
                    }
                }
            }
        }
    }
}
