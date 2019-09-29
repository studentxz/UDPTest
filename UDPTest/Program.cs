using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //建立UDPSocket 参数2：udp协议以数据报的方式传输，参数3：UDP协议
            Socket udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            
            //为udp服务器绑定ip
            IPAddress ip = IPAddress.Parse(getLocalIpAddress(5));
            EndPoint ipAddress = new IPEndPoint(ip, 43999);
            udpServer.Bind(ipAddress);

            //接收数据 本机的所有IP地址，所有可用的端口
            EndPoint clientAddress = new IPEndPoint(IPAddress.Any, 0);
            string message;
            byte[] data = new byte[1024];
            int length = 0;

            //把数据的来源放到第二个参数上
            while (true)
            {
                length = udpServer.ReceiveFrom(data, ref clientAddress);
                message = Encoding.UTF8.GetString(data, 0, length);
                Console.WriteLine("从IP:" + (clientAddress as IPEndPoint).Address + "取到了消息:" + message);
            }
        }
        /// <summary>
        /// 获取本机IP地址，3：外网IPV4地址，5：局域网IPV4地址
        /// </summary>
        /// <param name="addressNumber"></param>
        /// <returns></returns>
        private static string getLocalIpAddress(int addressNumber)
        {
            //获得本机局域网IP
            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            if (addressList.Length < 1)
            {
                return "";
            }

            //5是外网IPV4
            //6是以太网IPV4
            return addressList[addressNumber].ToString();
        }
    }
}
