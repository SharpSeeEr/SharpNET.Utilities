using SharpNET.Utilities.Encryption;
using System;
using Xunit;

namespace SharpNET.Utilities.Test
{
    public class EncryptionManagerTest
    {
        [Fact]
        public void EncryptAndDecryptString()
        {
            var key = EncryptionManager.GenerateKey();

            string text = "The quick brown fox jumped over the lazy dog";

            var encryptionManager = new EncryptionManager(key);

            var data = encryptionManager.Encrypt(text);

            var decrypted = encryptionManager.DecryptToString(data);

            Assert.Equal(text, decrypted);
        }
    }
}
