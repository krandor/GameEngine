using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces;
using GameEngine.Common.Interfaces.Networking;
using GameEngine.Common.Networking;

namespace GameEngine.Core.Networking.TCP
{
    public class TcpServer : IServer
    {
        INetworkLogic _nLogic;
        TcpListener tcpListener;
        Thread listenThread;
        static bool _running;

        public TcpServer(INetworkLogic nLogic)
        {
            _nLogic = nLogic;
            tcpListener = new TcpListener(IPAddress.Any, 3000);
            listenThread = new Thread(new ThreadStart(StartListening));
        }

        private void StartListening()
        {
            tcpListener.Start();
            _running = true;
            while (_running)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
				clientThread.Start(client);
            }
        }

        private void HandleClient(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            bool error = false;

            NetworkStream clientStream = tcpClient.GetStream();
            int bytesRead;
            byte[] message = new byte[4096];
            StringBuilder stringMessage = new StringBuilder();
            while (true)
            {
                bytesRead = 0;
                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    error = true;
                    break;
                }

                if (bytesRead == 0)
                    break;

                ASCIIEncoding encoder = new ASCIIEncoding();
                stringMessage.Append(encoder.GetString(message, 0, bytesRead));
            }

            if (!error)
            {
                TcpConnection connection = new TcpConnection();
                connection.Source = tcpClient.Client.RemoteEndPoint;
                connection.Message = stringMessage.ToString();

                if (!_nLogic.Manager.Connections.Contains(connection))
                {
                    _nLogic.OnConnect(connection);
                }
                else
                {
                    TcpPacket packet = new TcpPacket
                    {
                        Source = tcpClient.Client.RemoteEndPoint,
                        Destination = tcpClient.Client.LocalEndPoint,
                        Package = new TcpPackage
                        {
                            Contents = stringMessage.ToString(),
                            Size = stringMessage.Length
                        }
                    };

                    _nLogic.OnMessageRecieved(packet);
                }

            }
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
