using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.Encryption
{
    public class EncryptionManager
    {
        private byte[] _key;
        private bool _isProtected;
        private readonly object _locker = new object();

        public EncryptionManager(byte[] key)
        {
            if (key == null) throw new ArgumentNullException("Encryption Manager requires an encryption key");
            _key = key;
        }

        /// <summary>
        /// Converts a string containing 2-digit hexadecimal values into a byte array
        /// then initializes a new EncryptionManager using the byte array as the key
        /// </summary>
        /// <param name="key">Hex String formatted as: 86-30-B1-B1-E3-...-51-98-40</param>
        /// <returns></returns>
        public static EncryptionManager FromHexString(string key)
        {
            // Convert a string containing 2-digit hexadecimal
            // values into a byte array.
            // Get the separator character.
            char separator = key[2];

            // Split at the separators.
            string[] pairs = key.Split(separator);
            byte[] bytes = new byte[pairs.Length];
            for (int i = 0; i < pairs.Length; i++)
            {
                bytes[i] = Convert.ToByte(pairs[i], 16);
            }

            return new EncryptionManager(bytes);
        }

        /// <summary>
        /// Encrypts a plainText string using the system key retrieved from the Web Service
        /// </summary>
        /// <param name="plainText">String to encrypt</param>
        /// <returns>EncryptedData with encrypted string and IV used</returns>
        public EncryptedData Encrypt(string plainText)
        {
            lock (_locker)
            {
                using (var crypto = Aes.Create())
                {
                    try
                    {
                        UnprotectKey();
                        // Create the streams used for encryption. 
                        using (MemoryStream msEncrypt = new MemoryStream())
                        using (var encryptor = crypto.CreateEncryptor(_key, crypto.IV))
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            return new EncryptedData(msEncrypt.ToArray(), crypto.IV);
                        }
                    }
                    finally
                    {
                        ProtectKey();
                    }
                }
            }
        }

        /// <summary>
        /// Encrypts a plainText string using the system key retrieved from the Web Service
        /// </summary>
        /// <param name="plainText">String to encrypt</param>
        /// <returns>EncryptedData with encrypted string and IV used</returns>
        public EncryptedData Encrypt(byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException("bytes");
            
            lock (_locker)
            {
                using (var crypto = Aes.Create())
                {
                    try
                    {
                        UnprotectKey();
                    }
                    finally
                    {
                        ProtectKey();
                    }
                    // Create a encryptor to perform the stream transform.
                    ICryptoTransform encryptor = crypto.CreateEncryptor(_key, crypto.IV);

                    // Create the streams used for encryption. 
                    using (MemoryStream msEncrypt = new MemoryStream())
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(bytes, 0, bytes.Length);
                        return new EncryptedData(msEncrypt.ToArray(), crypto.IV);
                    }
                }

            }
        }

        /// <summary>
        /// Encrypts a plainText string using the system key retrieved from the Web Service
        /// </summary>
        /// <param name="plainText">String to encrypt</param>
        /// <returns>EncryptedData with encrypted string and IV used</returns>
        public EncryptedData Encrypt(Stream memStream)
        {
            if (memStream == null) throw new ArgumentNullException("memStream");
            
            lock (_locker)
            {
                using (var crypto = Aes.Create())
                {
                    try
                    {
                        UnprotectKey();
                    }
                    finally
                    {
                        ProtectKey();
                    }
                    // Create a encryptor to perform the stream transform.
                    ICryptoTransform encryptor = crypto.CreateEncryptor(_key, crypto.IV);

                    // Create the streams used for encryption. 
                    using (MemoryStream msEncrypt = new MemoryStream())
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        memStream.CopyTo(csEncrypt);
                        return new EncryptedData(msEncrypt.ToArray(), crypto.IV);
                    }
                }

            }
        }

        /// <summary>
        /// Decrypts a byte array to a Stream using the provided IV
        /// </summary>
        /// <param name="encrypted">Encrypted byte array</param>
        /// <param name="iv">IV used when byte array was encrypted</param>
        /// <param name="outStream">Stream to write decrypted stream to</param>
        public void DecryptToStream(byte[] encrypted, byte[] iv, Stream outStream)
        {
            if (encrypted == null) throw new ArgumentNullException("encrypted");
            if (iv == null) throw new ArgumentNullException("iv");
            if (outStream == null) throw new ArgumentNullException("outStream");

            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            lock (_locker)
            {
                using (var crypto = Aes.Create())
                {
                    try
                    {
                        UnprotectKey();
                        // Create the streams used for decryption. 
                        using (var msDecrypt = new MemoryStream(encrypted))
                        using (var decryptor = crypto.CreateDecryptor(_key, iv))
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            csDecrypt.CopyTo(outStream);
                            outStream.Position = 0;
                        }
                    }
                    finally
                    {
                        ProtectKey();
                    }

                }

            }
        }

        public void DecryptToStream(EncryptedData encrypted, Stream outStream)
        {
            if (encrypted == null) throw new ArgumentNullException("encrypted");
            if (encrypted.Data == null) throw new ArgumentException("Data property of encrypted cannot be null");
            if (encrypted.IV == null) throw new ArgumentException("IV property of encrypted cannot be null");

            DecryptToStream(encrypted.Data, encrypted.IV, outStream);
        }

        /// <summary>
        /// Decrypts a byte array to a string using the provided IV
        /// </summary>
        /// <param name="encrypted">Encrypted byte array</param>
        /// <param name="iv">IV used when byte array was encrypted</param>
        /// <returns>Plaintext string</returns>
        public string DecryptToString(byte[] encrypted, byte[] iv)
        {
            if (encrypted == null) throw new ArgumentNullException("encrypted");
            if (iv == null) throw new ArgumentNullException("iv");
            
            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            lock (_locker)
            {
                using (var crypto = Aes.Create())
                {
                    try
                    {
                        UnprotectKey();
                        using (var msDecrypt = new MemoryStream(encrypted))
                        using (var decryptor = crypto.CreateDecryptor(_key, iv))
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            return srDecrypt.ReadToEnd();
                        }
                    }
                    finally
                    {
                        ProtectKey();
                    }
                    // Create the streams used for decryption. 
                }

            }
        }

        public string DecryptToString(EncryptedData encrypted)
        {
            if (encrypted == null) throw new ArgumentNullException("encrypted");
            if (encrypted.Data == null) throw new ArgumentException("Data property of encrypted cannot be null");
            if (encrypted.IV == null) throw new ArgumentException("IV property of encrypted cannot be null");

            return DecryptToString(encrypted.Data, encrypted.IV);
        }

        /// <summary>
        /// Decrypts a byte array to a byte array using the provided IV
        /// </summary>
        /// <param name="encrypted">Encrypted byte array</param>
        /// <param name="iv">IV used when byte array was encrypted</param>
        /// <returns>Decrypted byte array</returns>
        public byte[] DecryptToBytes(byte[] encrypted, byte[] iv)
        {
            if (encrypted == null) throw new ArgumentNullException("encrypted");
            if (iv == null) throw new ArgumentNullException("iv");

            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            lock (_locker)
            {
                using (var crypto = Aes.Create())
                {
                    try
                    {
                        UnprotectKey();
                        // Create the streams used for decryption. 
                        using (var memStream = new MemoryStream())
                        using (var decryptor = crypto.CreateDecryptor(_key, iv))
                        using (var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Write))
                        {
                            // Write the decrypted bytes to the memory stream and return the byte array
                            cryptoStream.Write(encrypted, 0, encrypted.Length);
                            return memStream.ToArray();
                        }
                    }
                    finally
                    {
                        ProtectKey();
                    }
                }

            }
        }

        public byte[] DecryptToBytes(EncryptedData encrypted)
        {
            if (encrypted == null) throw new ArgumentNullException("encrypted");
            if (encrypted.Data == null) throw new ArgumentException("Data property of encrypted cannot be null");
            if (encrypted.IV == null) throw new ArgumentException("IV property of encrypted cannot be null");

            return DecryptToBytes(encrypted.Data, encrypted.IV);
        }

        /// <summary>
        /// Generates a random 256 bit key
        /// </summary>
        /// <returns>256 bit key</returns>
        public static byte[] GenerateKey()
        {
            using (var crypto = Aes.Create())
            {
                crypto.GenerateKey();
                return crypto.Key;
            }
        }

        /// <summary>
        /// Protects the byte array in memory
        /// </summary>
        private void ProtectKey()
        {
            //lock (_locker)
            //{
            //    if (!_isProtected)
            //    {
            //        System.Security.Cryptography
            //        ProtectedMemory.Protect(_key, MemoryProtectionScope.SameProcess);
            //        _isProtected = true;
            //    }
            //}
        }

        /// <summary>
        /// Unprotects the _key in memory so it can be used
        /// </summary>
        private void UnprotectKey()
        {
            //lock (_locker)
            //{
            //    if (_isProtected)
            //    {
            //        ProtectedMemory.Unprotect(_key, MemoryProtectionScope.SameProcess);
            //        _isProtected = false;
            //    }
            //}
        }
    }
}
