using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace HttpClientApp.Main
{
    public static class Functions
    {
        public static void EnunciationIPAddress()
        {
            IPAddress localIp = new IPAddress(new byte[] { 127, 0, 0, 1 });
            Console.WriteLine(localIp); // 127.0.0.1

            IPAddress someIp = new IPAddress(0x0100007F);
            Console.WriteLine(someIp); // 127.0.0.1
        }
        public static void EnunciationIPEndpoint()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endpoint = new IPEndPoint(ip, 8080);
            Console.WriteLine(endpoint);        // 127.0.0.1:8080
        }
        public static void WorkWithUri()
        {
            Uri uri = new Uri("https://user:password@www.somesite.com:443/home/index?q1=v1&q2=v2#fragmentName");
            Console.WriteLine($"Url: {uri.ToString()}");
            Console.WriteLine($"AbsolutePath: {uri.AbsolutePath}");
            Console.WriteLine($"AbsoluteUri: {uri.AbsoluteUri}");
            Console.WriteLine($"Fragment: {uri.Fragment}");
            Console.WriteLine($"Host: {uri.Host}");
            Console.WriteLine($"IsAbsoluteUri: {uri.IsAbsoluteUri}");
            Console.WriteLine($"IsDefaultPort: {uri.IsDefaultPort}");
            Console.WriteLine($"IsFile: {uri.IsFile}");
            Console.WriteLine($"IsLoopback: {uri.IsLoopback}");
            Console.WriteLine($"OriginalString: {uri.OriginalString}");
            Console.WriteLine($"PathAndQuery: {uri.PathAndQuery}");
            Console.WriteLine($"Port: {uri.Port}");
            Console.WriteLine($"Query: {uri.Query}");
            Console.WriteLine($"Scheme: {uri.Scheme}");
            Console.WriteLine($"Segments: {string.Join(", ", uri.Segments)}");
            Console.WriteLine($"UserInfo: {uri.UserInfo}");
        }
        public static void GetHostEntry()
        {
            var googleEntry = Dns.GetHostEntry("google.com");
            Console.WriteLine(googleEntry.HostName);
            foreach (var ip in googleEntry.AddressList)
            {
                Console.WriteLine(ip);
            }
        }
        public static void GetHosAddresses()
        {
            var googleIps = Dns.GetHostAddresses("google.com");
            foreach (var ip in googleIps)
            {
                Console.WriteLine(ip);
            }
        }
        public static void GetNetworkInterfaces()
        {
            var adapters = NetworkInterface.GetAllNetworkInterfaces();
            Console.WriteLine($"Обнаружено {adapters.Length} устройств");
            foreach (NetworkInterface adapter in adapters)
            {
                Console.WriteLine("=====================================================================");
                Console.WriteLine();
                Console.WriteLine($"ID устройства: ------------- {adapter.Id}");
                Console.WriteLine($"Имя устройства: ------------ {adapter.Name}");
                Console.WriteLine($"Описание: ------------------ {adapter.Description}");
                Console.WriteLine($"Тип интерфейса: ------------ {adapter.NetworkInterfaceType}");
                Console.WriteLine($"Физический адрес: ---------- {adapter.GetPhysicalAddress()}");
                Console.WriteLine($"Статус: -------------------- {adapter.OperationalStatus}");
                Console.WriteLine($"Скорость: ------------------ {adapter.Speed}");

                IPInterfaceStatistics stats = adapter.GetIPStatistics();
                Console.WriteLine($"Получено: ----------------- {stats.BytesReceived}");
                Console.WriteLine($"Отправлено: --------------- {stats.BytesSent}");
            }
        }
        public static void GetAllDevicesInNetwork()
        {
            var ipProps = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnections = ipProps.GetActiveTcpConnections();

            Console.WriteLine($"Всего {tcpConnections.Length} активных TCP-подключений");
            Console.WriteLine();
            foreach (var connection in tcpConnections)
            {
                Console.WriteLine("=============================================");
                Console.WriteLine($"Локальный адрес: {connection.LocalEndPoint.Address}:{connection.LocalEndPoint.Port}");
                Console.WriteLine($"Адрес удаленного хоста: {connection.RemoteEndPoint.Address}:{connection.RemoteEndPoint.Port}");
                Console.WriteLine($"Состояние подключения: {connection.State}");
            }
        }
        public static void Monitoring()
        {
            var ipProps = IPGlobalProperties.GetIPGlobalProperties();
            var ipStats = ipProps.GetIPv4GlobalStatistics();
            Console.WriteLine($"Входящие пакеты: {ipStats.ReceivedPackets}");
            Console.WriteLine($"Исходящие пакеты: {ipStats.OutputPacketRequests}");
            Console.WriteLine($"Отброшено входящих пакетов: {ipStats.ReceivedPacketsDiscarded}");
            Console.WriteLine($"Отброшено исходящих пакетов: {ipStats.OutputPacketsDiscarded}");
            Console.WriteLine($"Ошибки фрагментации: {ipStats.PacketFragmentFailures}");
            Console.WriteLine($"Ошибки восстановления пакетов: {ipStats.PacketReassemblyFailures}");
        }
        public static void WorkWithSocket()
        {
            Socket firstSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Socket secondSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                firstSocket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                firstSocket.Close();
            }
        }
    }
}