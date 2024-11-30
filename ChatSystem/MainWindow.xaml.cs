using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ChatSystem
{
    public partial class MainWindow : Window
    {
        private Server _server;
        private Client _client;
        public ObservableCollection<string> Messages { get; set; }
        private bool _isConnected = false;

        public MainWindow()
        {
            InitializeComponent();
            StartClock();

            Messages = new ObservableCollection<string>();
            DataContext = this;

            _server = new Server();
            _server.MessageReceived += OnServerMessageReceived;
            _server.StartServer();

            _client = new Client();
            _client.MessageReceived += OnClientMessageReceived;

            UpdateSendButtonState();
        }

        private void StartClock()
        {
            var _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (sender, e) =>
            {
                ClockText.Text = DateTime.Now.ToString("HH:mm");
            };
            _timer.Start();
        }

        private void OnServerMessageReceived(string message)
        {
            Dispatcher.Invoke(() =>
            {

            });
        }

        private void OnClientMessageReceived(string message)
        {
            Dispatcher.Invoke(() =>
            {
                Messages.Add(message);
            });
        }
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(InputText.Text))
            {
                InitialLetterText.Text = InputText.Text[0].ToString();
            }
            else
            {
                InitialLetterText.Text = string.Empty;
            }
        }


        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isConnected)
            {
                try
                {
                    _client.Connect("127.0.0.1", 4444);
                    Messages.Add("Client connected to the server.");
                    _isConnected = true;
                    ConnectButton.Content = "Disconnect";
                }
                catch (Exception ex)
                {
                    Messages.Add($"Error connecting: {ex.Message}");
                }
            }
            else
            {
                _client.Disconnect();
                Messages.Add("Client disconnected from the server.");
                _isConnected = false;
                ConnectButton.Content = "Connect";
            }

            UpdateSendButtonState();
        }



        private void UpdateSendButtonState()
        {
            SendButton.IsEnabled = _isConnected;
        }
    }

    public class Server
    {
        private TcpListener _listener;
        private bool _isRunning;
        private readonly List<TcpClient> _clients = new List<TcpClient>();
        public event Action<string> MessageReceived;

        public void StartServer(int port = 4444)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _isRunning = true;

            Thread serverThread = new Thread(() =>
            {
                while (_isRunning)
                {
                    try
                    {
                        var client = _listener.AcceptTcpClient();
                        lock (_clients)
                        {
                            _clients.Add(client);
                        }
                        HandleClient(client);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Server Error: {ex.Message}");
                    }
                }
            });
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void HandleClient(TcpClient client)
        {
            Thread clientThread = new Thread(() =>
            {
                try
                {
                    var stream = client.GetStream();
                    byte[] buffer = new byte[1024];

                    while (client.Connected)
                    {
                        int byteCount = stream.Read(buffer, 0, buffer.Length);
                        if (byteCount > 0)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                            MessageReceived?.Invoke(message);
                            BroadcastMessage(message, client);
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    lock (_clients)
                    {
                        _clients.Remove(client);
                    }
                    client.Close();
                }
            });
            clientThread.IsBackground = true;
            clientThread.Start();
        }

        private void BroadcastMessage(string message, TcpClient sender)
        {
            lock (_clients)
            {
                foreach (var client in _clients)
                {
                    if (client != sender && client.Connected)
                    {
                        try
                        {
                            var stream = client.GetStream();
                            byte[] buffer = Encoding.UTF8.GetBytes(message);
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public void StopServer()
        {
            _isRunning = false;
            _listener?.Stop();

            lock (_clients)
            {
                foreach (var client in _clients)
                {
                    client.Close();
                }
                _clients.Clear();
            }
        }
    }

    // Client Class
    public class Client
    {
        private TcpClient _client;
        private NetworkStream _stream;
        public event Action<string> MessageReceived;

        public bool IsConnected => _client != null && _client.Connected;

        public void Connect(string host, int port)
        {
            _client = new TcpClient();
            _client.Connect(host, port);
            _stream = _client.GetStream();

            Thread readThread = new Thread(() =>
            {
                byte[] buffer = new byte[1024];
                while (_client.Connected)
                {
                    try
                    {
                        int byteCount = _stream.Read(buffer, 0, buffer.Length);
                        if (byteCount > 0)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                            MessageReceived?.Invoke(message);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            });
            readThread.IsBackground = true;
            readThread.Start();
        }

        public void SendMessage(string message)
        {
            if (!IsConnected || _stream == null)
            {
                throw new InvalidOperationException("Client is not connected. Unable to send message.");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            _stream.Write(buffer, 0, buffer.Length);
        }

        public void Disconnect()
        {
            _client?.Close();
            _stream = null;

        }

    }
}
