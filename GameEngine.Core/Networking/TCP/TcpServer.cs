﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using GameEngine.Common.EventHandlers.Networking;
using GameEngine.Common.Interfaces.Configuration;
using GameEngine.Common.Interfaces.Networking;

namespace GameEngine.Core.Networking.TCP
{
    public class TcpServer : IServer
    {
        IConfiguration _sConfiguration;
        TcpListener tcpListener;
        Thread listenThread;
        static bool _running;

        public event NetworkHandlers.ConnectionHandler OnConnect;
        public event NetworkHandlers.ConnectionHandler OnDisconnect;
        public event NetworkHandlers.MessageHandler OnMessageRecieved;
        public event NetworkHandlers.MessageHandler OnMessageSent;

        public TcpServer(IConfiguration sConfig)
        {
            _sConfiguration = sConfig;
            tcpListener = new TcpListener(_sConfiguration.IP, _sConfiguration.Port);
            listenThread = new Thread(new ThreadStart(StartListening));
        }

        private void StartListening()
        {
            tcpListener.Start();
            _running = true;
            while (_running)
            {
                TcpClient client = tcpListener.AcceptTcpClient();

                TcpConnection connection = new TcpConnection
                {
                    ConnectedAt = DateTime.Now,
                    Source = (IPEndPoint)client.Client.RemoteEndPoint,
                    Client = client
                };

                OnConnect(connection);

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(client);
            }
        }

        public void Send(object client, IPacket packet)
        {
            TcpClient tcpClient = (TcpClient)client;
            using (var stream = tcpClient.GetStream())
            {
                var ser = new JavaScriptSerializer();
                var json = ser.Serialize(packet.Package);

                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(json);

                stream.Write(buffer, 0, buffer.Length);
                OnMessageSent(packet);
            }
        }

        private void HandleClient(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            TcpPackage package = null;

            bool error = false;
            int bytesRead;
            byte[] message = new byte[tcpClient.ReceiveBufferSize];

            ASCIIEncoding encoder = new ASCIIEncoding();

            while (true)
            {
                bytesRead = 0;
                string strMessage = "";
                try
                {
                    bytesRead = clientStream.Read(message, 0, tcpClient.ReceiveBufferSize);
                    strMessage = encoder.GetString(message, 0, bytesRead);
                    var ser = new JavaScriptSerializer();
                    package = ser.Deserialize<TcpPackage>(strMessage);
                }
                catch
                {
                    error = true;                  
                    break;
                }

                if (bytesRead == 0)
                    break;

                if (!error)
                {
                    TcpPacket packet = new TcpPacket
                    {
                        Source = (IPEndPoint)tcpClient.Client.RemoteEndPoint,
                        Destination = (IPEndPoint)tcpClient.Client.LocalEndPoint,
                        Package = package
                    };

                    OnMessageRecieved(packet);
                }
            }
            TcpConnection connection = new TcpConnection
            {
                ConnectedAt = DateTime.Now,
                Source = (IPEndPoint)tcpClient.Client.RemoteEndPoint,
                Client = client
            };

            OnDisconnect(connection);
        }

        public void StartListeningForClients()
        {
            listenThread.Start();
        }

        public void StopListeningForClients()
        {
            _running = false;
        }
    }
}
