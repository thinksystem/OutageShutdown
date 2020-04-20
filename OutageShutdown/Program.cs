﻿using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace OutageShutdown
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("write ip you want to ping");
            string input = Console.ReadLine();
            Console.WriteLine(SendPing(input));
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
                Console.WriteLine("Got reply from " + ip + ".");
                return true;
            }
            else
            {
                Console.WriteLine("Host unreachable.");
                return false;
            }
        }
    }
}