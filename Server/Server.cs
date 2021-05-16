using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        static private bool firstUsrIsInit = false;
        static private bool secondUsrIsInit = false;

        static private int nowTurn = 1;
        static private int numOfLastChangedBtn = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Ip:");
            string ip = Console.ReadLine();
            Console.WriteLine("Port:");
            int port = int.Parse(Console.ReadLine());

            var tcpSocet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocet.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            tcpSocet.Listen(2);

            while (true)
            {
                var listener = tcpSocet.Accept();
                String msg = getMsg(listener);

                if (msg.StartsWith("get_id")) initUser(listener);
                else if (msg.StartsWith("send_turn")) sendTurn(msg);
                else if (msg.StartsWith("whose_move")) answerWhoose(listener);

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }

        private static string getMsg(Socket listener)
        {
            byte[] buffer = new byte[256];
            StringBuilder data = new StringBuilder();
            do
            {
                var size = listener.Receive(buffer);
                data.Append(Encoding.UTF8.GetString(buffer, 0, size));
            }
            while (listener.Available > 0);
            return data.ToString();
        }

        private static void sendTurn(string msg)
        {
            String[] parsedLine = msg.Split(" ");
            numOfLastChangedBtn = int.Parse(parsedLine[2]);
            if (parsedLine[1].Equals("1")) nowTurn = 2;
            else nowTurn = 1;
        }

        private static void answerWhoose(Socket listener)
        {
            listener.Send(Encoding.UTF8.GetBytes(nowTurn.ToString() + " " + numOfLastChangedBtn.ToString()));
        }

        private static void initUser(Socket listener)
        {
            if (!firstUsrIsInit)
            {
                firstUsrIsInit = true;
                listener.Send(Encoding.UTF8.GetBytes("2"));
            } else
            {
                secondUsrIsInit = true;
                listener.Send(Encoding.UTF8.GetBytes("1"));
            }
        }
    }
}
