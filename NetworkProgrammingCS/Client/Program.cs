using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "CLIENT";
			Console.WriteLine("CLIENT");
			try
			{
				//1) Определяем параметры подключения:
				IPAddress target_address;
				IPAddress.TryParse("127.0.0.1", out target_address);    //new IPAddress(new byte[] { 127, 0, 0, 1 });
				IPEndPoint target = new IPEndPoint(target_address, 27015);

				//2) Создаем сокет для подключения к Серверу:
				Socket connect_socket = new Socket(target.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

				//3) Подключаемся к Серверу:
				connect_socket.Connect(target);

				//4) Производим обмен данными:
				//do
				//{
				//4.1) Отправка:
				string message = "Hello Server!";
				byte[] bytes = Encoding.UTF8.GetBytes(message);
				connect_socket.Send(bytes);
				Console.WriteLine($"{bytes.Length} Bytes sent");

				//4.2) Ожидаем подтвержение от Сервера:
				byte[] buffer = new byte[1024];
				int received = connect_socket.Receive(buffer);
				string response = Encoding.UTF8.GetString(buffer);
				Console.WriteLine($"Received {received} Bytes from Server. Message: {response}");
				//if (response == "<ACK>")Console.WriteLine("Server acknowledged message");
				//} while (true);

				//5) Разрываем TCP-соединение:
				connect_socket.Shutdown(SocketShutdown.Both);

				//6) Закрываем Сокет:
				connect_socket.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.ReadLine();
		}
	}
}
