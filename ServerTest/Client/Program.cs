using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
       

        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iP = new IPEndPoint(IPAddress.Parse("127.0.01"), 8899);
            clientSocket.Connect(iP);
            clientSocket.Send(Encoding.Default.GetBytes("我是客户端你好"));

            Thread clientThread = new Thread(resiveMeassage);
            clientThread.Start(clientSocket);

            while (true) { };
 

        }

        static void resiveMeassage(object socket)
        {
            Socket rss = socket as Socket;
            byte[] buffer = new byte[1024];

            int length = rss.Receive(buffer);
            Console.WriteLine(Encoding.Default.GetString(buffer, 0, length));
        }
    }
}
