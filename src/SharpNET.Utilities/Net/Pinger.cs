using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace SharpNET.Utilities.Net
{
    /// <summary>
    /// Provides simple shortcuts for pinging a remote computer system
    /// </summary>
    public class Pinger
    {
        /// <summary>
        /// Provides detailed results of a ping
        /// </summary>
        public class PingStatus
        {
            public PingStatus(bool success, int statusCode, long responseTime)
            {
                Success = success;
                StatusCode = statusCode;
                ResponseTime = responseTime;
            }

            public bool Success { get; protected set; }
            public long ResponseTime { get; protected set; }
            public int StatusCode { get; protected set; }
        }

        public static PingStatus Ping(string deviceName)
        {
            return Ping(deviceName, new PingerOptions());
        }

        public static PingStatus Ping(string deviceName, int timeout)
        {
            return Ping(deviceName, new PingerOptions() { Timeout = timeout });
        }

        public static PingStatus Ping(string deviceName, PingerOptions options)
        {
            Ping pinger = new Ping();
            PingOptions pingOptions = new PingOptions() { DontFragment = options.DontFragment };
            
            // Create a buffer of 32 bytes of data to be transmitted.

            byte a = Encoding.ASCII.GetBytes("a")[0];
            byte[] buffer = new byte[options.PayloadSize]; // Encoding.ASCII.GetBytes(data);
            for (int i = 0; i < options.PayloadSize; i++)
            {
                buffer[i] = a;
            }

            try
            {
                PingReply reply = pinger.Send(deviceName, options.Timeout, buffer, pingOptions);
                if (reply.Status == IPStatus.Success)
                {
                    //Ping was successful
                    return new PingStatus(true, (int)reply.Status, reply.RoundtripTime);
                }
                //Ping failed
                return new PingStatus(false, (int)reply.Status, reply.RoundtripTime);
            }
            catch (Exception)
            {
                return new PingStatus(false, 99999, 99999);
            }
        }

        public static void PingUntilDown(string deviceName, long timeout, int interval)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            while (true)
            {
                if (!Ping(deviceName).Success)
                {
                    return;
                }
                if (timer.ElapsedMilliseconds > timeout) throw new TimeoutException();
                System.Threading.Thread.Sleep(interval);
            }
        }

        public static void PingUntilUp(string deviceName, long timeout, int interval)
        {
            System.Diagnostics.Stopwatch timer =  new System.Diagnostics.Stopwatch();
            timer.Start();
            while (true)
            {
                if (Ping(deviceName).Success)
                {
                    return;
                }
                if (timer.ElapsedMilliseconds > timeout) throw new TimeoutException();
                System.Threading.Thread.Sleep(interval);
            }
        }
    }

    
}
