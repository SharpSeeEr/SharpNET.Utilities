using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.Encryption
{
    /// <summary>
    /// Class used as return type for Encrypt so the encrypted data 
    /// and the IV used to encrypt that data can be returned at one time.
    /// </summary>
    public class EncryptedData
    {
        public byte[] Data { get; set; }
        public byte[] IV { get; set; }

        public EncryptedData()
        {
        }

        public EncryptedData(byte[] data, byte[] iv)
        {
            Data = data;
            IV = iv;
        }
    }
}
