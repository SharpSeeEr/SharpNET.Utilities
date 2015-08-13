using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.Net
{
    public class PingerOptions
    {
        /// <summary>
        /// Ping timeout in milliseconds
        /// </summary>
        public int Timeout = 1000;
        /// <summary>
        /// Payload size in bytes
        /// </summary>
        public int PayloadSize = 32;
        /// <summary>
        /// Whether packet fragmentation should be allowed or not
        /// </summary>
        public bool DontFragment = false;
    }
}
