using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text;

namespace Multicast
{
    //Sender
    class Program
    {
        static void Main(string[] args)
        {
            // UdpClient 객체 생성
            UdpClient udpClient = new UdpClient();

            // IP 주소 설정
            IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
            udpClient.JoinMulticastGroup(multicastaddress);

            // Multicast 중단점 설정
            IPEndPoint remoteEP = new IPEndPoint(multicastaddress, 2222);

            byte[] buffer = null;

            Console.WriteLine("메세지를 전송하려면 엔터키를 눌러주세요.");
            Console.ReadLine();

            for(int i = 0; i <= 10; i++)
            {
                buffer = Encoding.Unicode.GetBytes(i.ToString());

                //Multicast 그룹에 데이터그램 전송
                udpClient.Send(buffer, buffer.Length, remoteEP);
                Console.WriteLine("Sent : {0}", i.ToString());
                Thread.Sleep(1000);
            }

            buffer = Encoding.Unicode.GetBytes("quit");
            udpClient.Send(buffer, buffer.Length, remoteEP);

            udpClient.Close();

            Console.WriteLine("All Done! Press ENTER to quit.");
            Console.ReadLine();
        }
    }
}
