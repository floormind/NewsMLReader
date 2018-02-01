using System;
using Moq;
using NUnit.Framework;

namespace NewsMLReader.Tests
{
    [TestFixture]
    public class NewsMLReaderTest
    {
        private INewsMlReader Reader;


        // I do not have a sample NewsML file and an active FTP server so i am using this test server i found online which hosts a txt file.
        // NewsML file seems to be a xml document, if that is the case, the concept is the same as reading a txt file from a FTP server.
        [Test]
        public void Read_NewsML_From_FTP()
        {
            //arrange
            const string ftpPath = "ftp://test.rebex.net/readme.txt";
            const string username = "demo";
            const string password = "password";

            Reader = new NewsMlReader(ftpPath, username, password);

            var sb =
                "Welcome,\r\n\r\nyou are connected to an FTP or SFTP server used for testing purposes by Rebex FTP/SSL or Rebex SFTP sample code.\r\nOnly read access is allowed and the FTP download speed is limited to 16KBps.\r\n\r\nFor infomation about Rebex FTP/SSL, Rebex SFTP and other Rebex .NET components, please visit our website at http://www.rebex.net/\r\n\r\nFor feedback and support, contact support@rebex.net\r\n\r\nThanks!\r\n";

            //act
            var sut = Reader.ReadFromFtp();

            //assert
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Result, Is.EqualTo(sb.ToString()));

        }


        [Test]
        public void Post_Content_To_Feed()
        {
            //arrage
            const string ftpPath = "ftp://test.rebex.net/readme.txt";
            const string username = "demo";
            const string password = "password";

            Reader = new NewsMlReader(ftpPath, username, password);

            //act
            var sut = Reader.PostToFeed("testContent");

            //assert
            Assert.That(sut.Result, Is.True);
        }

        // Ideally another test should also be written for the false result. 
    }
}
