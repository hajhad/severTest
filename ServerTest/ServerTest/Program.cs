using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerTest
{
    class Program
    {

        string name;
        int age;
        object socket;

        static  Socket serverSocket = null;
        static void Main(string[] args)
        {

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8899);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(10);

            Thread thread = new Thread(listenClientConnect);
            thread.Start();

        }


      private static void listenClientConnect()
        {
            Socket clientSocket = serverSocket.Accept();
            clientSocket.Send(Encoding.Default.GetBytes("服务器告诉你连接成功"));
            Thread reeThread = new Thread(reeThreadClientMessage);
            reeThread.Start(clientSocket);
        }

        static void reeThreadClientMessage(object clientSocket)
        {
            Socket socket = clientSocket as Socket;

            byte[] buffer = new byte[1024];

          int length =  socket.Receive(buffer);

            Console.WriteLine(Encoding.Default.GetString(buffer));
        }
    }

   

  
}
