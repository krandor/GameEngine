using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

			Run ();
		}

		public static void Run()
		{
			string input = "";
			while (input.ToLower() != "quit") {
				Console.WriteLine ("Please enter the text you'd like to send:");
				input = Console.ReadLine ();

				ASCIIEncoding encoder = new ASCIIEncoding();
				byte[] buffer = encoder.GetBytes(input);

				NetworkStream clientStream = client.GetStream ();			
				clientStream.Write(buffer, 0 , buffer.Length);
				//clientStream.Flush();

				//client.Close ();
			}

		}
	}
}
