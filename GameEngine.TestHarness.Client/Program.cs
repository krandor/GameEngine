using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GameEngine.TestHarness.Client
{
	class MainClass
	{
		private static TcpClient client;
		private static IPEndPoint serverEndPoint;

		public static void Main (string[] args)
		{
			serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);

			client = new TcpClient();
			client.Connect(serverEndPoint);

			Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
			clientThread.Start(client);

			Run ();
		}

		public static void Run()
		{
			string input = "";
			while (input.ToLower() != "quit") 
			{
				Console.WriteLine ("Please enter the text you'd like to send:");
				input = Console.ReadLine();

				ASCIIEncoding encoder = new ASCIIEncoding();
				byte[] buffer = encoder.GetBytes(input);

				NetworkStream clientStream = client.GetStream ();			
				clientStream.Write(buffer, 0 , buffer.Length);
				//clientStream.Flush();

				//client.Close ();
			}

		}

		private static void HandleClient(object client)
		{
			TcpClient tcpClient = (TcpClient)client;
			//bool error = false;

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
					Console.WriteLine("You just sent: " + strMessage);
				}
				catch
				{
					//error = true;
					break;
				}

				if (bytesRead == 0)
					break;                

			}
		}
	}
}
