using dynamic_configuration_parser;
using System;
using System.Threading;
using Xunit;

namespace dynamic_configuration_parser_test
{
    public class ParserTest
    {

        [Fact]
        public void ShouldParseAllKeys()
        {
            IParser p = new Parser();

            dynamic cfg = p.Parse(@"

UserName: super_user;
Password: top_secret_password;

TimeToLive: 10;
IsEnabled: false;


");

            Assert.Equal(cfg.Properties.Count, 4);
        }

        [Fact]
        public void ShouldPropertiesSetToRelvantType()
        {
            IParser p = new Parser();

            dynamic cfg = p.Parse(@"
UserName: super_user;
Password: top_secret_password;

TimeToLive: 10;
IsEnabled: false;");


            Assert.Equal(cfg.TimeToLive.GetType().Name, "Int32");
            Assert.Equal(cfg.IsEnabled.GetType().Name, "Boolean");
            Assert.Equal(cfg.UserName.GetType().Name, "String");
        }

        [Fact]
        public void ShouldThrowArgumentExceptionIfNullOrEmptyInput()
        {
            IParser p = new Parser();

            Assert.Throws<ArgumentException>(() => p.Parse(String.Empty));
        }

        [Fact]
        public void ShouldTrimAllKeyAndStringValues()
        {
            IParser p = new Parser();

            dynamic cfg = p.Parse("     UserName  :    admin   ;    ");

            Assert.Equal(cfg.UserName, "admin");
        }

        [Fact]
        public void ShouldThrowEmptyKeyExceptionIfKeyIsNullOrEmpty()
        {
            IParser p = new Parser();

            Assert.Throws<EmptyKeyException>(() => p.Parse(":admin"));
        }

        [Fact]
        public void ShouldThrowUnknownKeyExceptionIfNonExistentPropertyIsRead()
        {
            IParser p = new Parser();

            dynamic cfg = p.Parse(@"
UserName: super_user;
Password: top_secret_password;

TimeToLive: 10;
IsEnabled: false; ");

            Assert.Throws<UnknownKeyException>(() => Console.Write(cfg.isEnabled));
        }

        [Fact]
        public void ShouldThrowInvalidKeyExcptionIfKeyNameIsNotValid()
        {
            IParser p = new Parser();

            Assert.Throws<InvalidKeyException>(() => p.Parse("4submit:true;"));

        }
        [Fact]
        public void ShouldThrowDuplicatedKeyExcptionIfDuplicatesAreDetected()
        {
            IParser p = new Parser();

            Assert.Throws<DuplicatedKeyException>(() => p.Parse(@"submit:true;
submit:false;
"));

        }
    }
}
