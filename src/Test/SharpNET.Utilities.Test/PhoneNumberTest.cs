using System;
using Xunit;

namespace SharpNET.Utilities.Test
{
    public class PhoneNumberTest
    {
        [Fact]
        public void USPhoneNumber()
        {
            var phoneNumber = new PhoneNumber("8015461234");
            Assert.Equal("801", phoneNumber.AreaCode);
            Assert.Equal("546", phoneNumber.Prefix);
            Assert.Equal("1234", phoneNumber.Postfix);
            Assert.Equal("(801) 546-1234", phoneNumber.Formatted);
        }
    }
}
