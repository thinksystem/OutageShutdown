using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Diagnostics;

namespace OutageShutdown
{
    class Program
    {
        static string targetip = "192.168.1.30";
        static int howmuchping = 5;
        static int waitperping = 10000;

        static void Main()
        {
            int failedpings = 0;

            for (int i = 0; i < howmuchping; i++)
            {
                bool result = SendPing(targetip);
                if (result) Environment.Exit(0);
                if (!result) failedpings++;
                Console.WriteLine("PING " + failedpings + " " + result);
                System.Threading.Thread.Sleep(waitperping);
            }
            
            if (failedpings >= howmuchping)
            {
                Console.WriteLine("POWER OUTAGE SHUTTING DOWN NOW");
                Process.Start("shutdown", "/p");
                Process.Start("shutdown now");
                Process.Start("sudo shutdown now");
            }

            Console.WriteLine("DONE " + failedpings);
            Console.ReadLine();
        }
        static bool SendPing(string ip)
        {
            Ping ping = new Ping();
            PingOptions options = new PingOptions();

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = ping.Send(ip, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}