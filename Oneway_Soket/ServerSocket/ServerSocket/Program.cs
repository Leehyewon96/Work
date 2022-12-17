using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("172.30.1.22"), 9999);
            socket.Bind(ep);

            //10개까지 연결가능
            socket.Listen(10);

            //클라에서 서버쪽으로 연결을 받으면 새로운 클라이언트 소켓을 만들어줌
            Socket clientSocket = socket.Accept();
            if (clientSocket.Connected == true)
                Console.WriteLine("클라이언트가 서버에 접속했습니다.");

            //계속 클라로부터 데이터를 수신받기위해 while문
            while(true)
            {
                byte[] buff = new byte[2048];

                //클라에서 들어오는 데이터 받아서 길이확인
                int n = clientSocket.Receive(buff);

                //buff에서 0번부터 n번까지 읽음
                string result = Encoding.UTF8.GetString(buff, 0, n);

                Console.WriteLine(result);
            }
        }
    }
}
