using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text;

namespace Multicast_Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            //UdpClient 객체 생성
            UdpClient client = new UdpClient();

            //UDP 로컬 IP포트에 바인딩
            client.ExclusiveAddressUse = false;
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2222);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            //Multicast 그룹에 Join
            IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
            client.JoinMulticastGroup(multicastaddress);

            Console.WriteLine("Listening this will quit");

            while(true)
            {
                //Multicast 수신
                byte[] data = client.Receive(ref localEp);
                string strData = Encoding.Unicode.GetString(data);
                Console.WriteLine("receied data : {0}", strData);

                if (strData == "quit")
                    break;
            }

            Console.WriteLine("quit the program to ENTER");
            Console.ReadLine();
        }
    }
}
