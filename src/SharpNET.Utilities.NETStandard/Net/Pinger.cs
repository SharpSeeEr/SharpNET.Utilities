using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Threading.Tasks;

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

        /// <summary>
        /// Ping a network device using default settings
        /// </summary>
        /// <param name="deviceName">Name of the network device to ping</param>
        /// <returns>PingStatus object</returns>
        public static async Task<PingStatus> PingAsync(string deviceName)
        {
            return await PingAsync(deviceName, new PingerOptions());
        }

        /// <summary>
        /// Ping a network device with a specific timeout
        /// </summary>
        /// <param name="deviceName">Name of the network device to ping</param>
        /// <param name="timeout">Timeout for the ping in milliseconds</param>
        /// <returns>PingStatus object</returns>
        public static async Task<PingStatus> PingAsync(string deviceName, int timeout)
        {
            return await PingAsync(deviceName, new PingerOptions() { Timeout = timeout });
        }

        /// <summary>
        /// Ping a network device with specific options
        /// </summary>
        /// <param name="deviceName">Name of the network device to ping</param>
        /// <param name="options">PingerOptions object</param>
        /// <returns>PingStatus object</returns>
        public static async Task<PingStatus> PingAsync(string deviceName, PingerOptions options)
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
                PingReply reply = await pinger.SendPingAsync(deviceName, options.Timeout, buffer, pingOptions);
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

        /// <summary>
        /// Pings the given device until a success result is received.
        /// </summary>
        /// <param name="deviceName">Name of the network device to ping</param>
        /// <param name="timeout">Max amount of time to wait for a failure, in milliseconds</param>
        /// <param name="interval">Number of milliseconds to wait betweek pings</param>
        public static async void PingUntilDownAsync(string deviceName, long timeout, int interval)
        {
            var timer = new Stopwatch();
            timer.Start();
            while (true)
            {
                var result = await PingAsync(deviceName);
                if (!result.Success)
                {
                    return;
                }
                if (timer.ElapsedMilliseconds > timeout) throw new TimeoutException();
                System.Threading.Thread.Sleep(interval);
            }
        }

        /// <summary>
        /// Pings the given device until a success result is received.
        /// </summary>
        /// <param name="deviceName">Name of the network device to ping</param>
        /// <param name="timeout">Max amount of time to wait for a success, in milliseconds</param>
        /// <param name="interval">Number of milliseconds to wait betweek pings</param>
        public static async void PingUntilUpAsync(string deviceName, long timeout, int interval)
        {
            var timer = new Stopwatch();
            timer.Start();
            while (true)
            {
                var result = await PingAsync(deviceName);
                if (result.Success)
                {
                    return;
                }
                if (timer.ElapsedMilliseconds > timeout) throw new TimeoutException();
                System.Threading.Thread.Sleep(interval);
            }
        }
    }

    
}
