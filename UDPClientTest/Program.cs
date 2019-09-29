using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //建立UDPClientSocket,参数2：udp协议以数据报的方式传输，参数3：UDP协议
            Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress ip = IPAddress.Parse(GetLocalAddress(5));

            EndPoint ipAddress = new IPEndPoint(ip, 43999);

            Console.WriteLine("请输入需要发送给服务端的信息，以回车结束。");
            //输入客户端需要发送给服务端的信息
            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(message);
                Console.WriteLine(message);
                udpClient.SendTo(data, ipAddress);
            }
            
        }

        /// <summary>
        /// 获取本机IP地址    3：本机外网IPV4;  5：本机局域网IPV4
        /// </summary>
        /// <param name="addressNumber"></param>
        /// <returns></returns>
        private static string GetLocalAddress(int addressNumber)
        {
            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            if (addressList.Length < 1)
            {
                return "";
            }
            return addressList[addressNumber].ToString();
        }
    }
}
