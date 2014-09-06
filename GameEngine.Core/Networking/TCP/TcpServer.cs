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
using GameEngine.Common.Interfaces.Configuration;
using GameEngine.Common.EventHandlers.Networking;

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
		public event NetworkHandlers.ConnectionHandler OnMessageRecieved;
		public event NetworkHandlers.ConnectionHandler OnMessageSent;

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
				//OnConnect (client);
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
					//Console.WriteLine("Read Message from Stream: " + strMessage);
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
						Source = tcpClient.Client.RemoteEndPoint,
						Destination = tcpClient.Client.LocalEndPoint,
						Package = new TcpPackage
						{
							Contents = strMessage,
							Size = strMessage.Length
						}
					};

					TcpConnection connection = new TcpConnection();
					connection.ConnectedAt = DateTime.Now;
					connection.Message = packet;

					OnMessageRecieved (connection);
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

		public void SendToClients()
		{

		}

		public void SendToClient(IConnection connection)
		{
		
		}
    }
}
