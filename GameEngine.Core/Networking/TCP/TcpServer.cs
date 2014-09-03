using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces.Logic;
using GameEngine.Common.Interfaces.Networking;
using GameEngine.Common.Networking;

namespace GameEngine.Core.Networking.TCP
{
    public class TcpServer : IServer
    {
        IServerLogic _sLogic;
        TcpListener tcpListener;
        Thread listenThread;
        static bool _running;

        public TcpServer(IServerLogic sLogic)
        {
            _sLogic = sLogic;
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

                TcpConnection connection = new TcpConnection();
				connection.ConnectedAt = DateTime.Now;
				connection.Message = packet;
				_sLogic.OnConnect(connection);
				_sLogic.OnMessageRecieved(connection);
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

		public void SendToClients()
		{

		}

		public void SendToClient(IConnection connection)
		{
		
		}
    }
}
