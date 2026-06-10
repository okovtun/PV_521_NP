using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Server";
			Console.WriteLine("SERVER");
			//1) Определяем параметры подключения:
			IPAddress address;
			IPAddress.TryParse("0.0.0.0", out address);
			IPEndPoint point = new IPEndPoint(address,27015);

			//2) Создаем сокет,который будет слушать порт:
			Socket listen_socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			listen_socket.Bind(point);
			listen_socket.Listen(1);

			//3) Принимаем клиентское подключение:
			Socket client_socket = listen_socket.Accept();

			//4) Производим обмен данными:
			byte[] buffer = new byte[1024];
			int iRecvResult = client_socket.Receive(buffer);
			string message = Encoding.UTF8.GetString(buffer);
			Console.WriteLine($"Received {iRecvResult} Bytes. Message: {message}");

			string sendMessage = "Hello client";
			byte[] send_buffer = Encoding.UTF8.GetBytes(sendMessage);
			int iSendResult = client_socket.Send(send_buffer);
			Console.WriteLine($"{iSendResult} Bytes sent");
			client_socket.Shutdown(SocketShutdown.Both);
			client_socket.Close();
			listen_socket.Close();
			Console.ReadLine();
		}
	}
}
