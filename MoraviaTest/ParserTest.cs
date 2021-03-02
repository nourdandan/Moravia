using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moravia;
using Moq;
using System.Text;
using System.IO;
using Moravia.Homework;
using Moravia.Parsers;
using System.Xml.Linq;

namespace MoraviaTest
{
    [TestClass]
    public class ParserTest
    {
        Document MapValues(XDocument xdoc)
        {
            return new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };
        }

        [TestMethod]
        public void JsonParser_Read()
        {
            Mock<IStreamManager> mockStreamManager = new Mock<IStreamManager>();
            string fakeContent = @"{'Text': 'TextResult','Title': 'TitleResult' }";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeContent);

            Stream fakeMemoryStream = new MemoryStream(fakeFileBytes);
            mockStreamManager.Setup(streamManager =>
            streamManager.GetReadStream(It.IsAny<string>()))
                .Returns(() => fakeMemoryStream);
            var jsonParser = new JsonParser<Document>();
            var result = jsonParser.Read(mockStreamManager.Object.GetReadStream(""));
            Assert.AreEqual("TextResult", result.Text);
            Assert.AreEqual("TitleResult", result.Title);
        }

        [TestMethod]
        public void JsonParser_Write()
        {
            Mock<IStreamManager> mockStreamManager = new Mock<IStreamManager>();
            var document = new Document() { Text = "TextResult", Title = "TitleResult" };

            var mem = new MemoryStream();
            mockStreamManager.Setup(streamManager =>
            streamManager.GetWriteStream(It.IsAny<string>()))
                .Returns(() => mem);
            var jsonParser = new JsonParser<Document>();
            jsonParser.Write(document, mockStreamManager.Object.GetWriteStream(""));
            var fakeContent =  Encoding.UTF8.GetString(mem.ToArray());
            Assert.AreEqual("{\"Title\":\"TitleResult\",\"Text\":\"TextResult\"}",
                fakeContent);
        }

        [TestMethod]
        public void XmlParser_Read()
        {
            Mock<IStreamManager> mockStreamManager = new Mock<IStreamManager>();
            string fakeContent = @"<root>
<text>TextResult</text>
<title>TitleResult</title>
</root>";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeContent);

            Stream fakeMemoryStream = new MemoryStream(fakeFileBytes);
            mockStreamManager.Setup(streamManager =>
            streamManager.GetReadStream(It.IsAny<string>()))
                .Returns(() => fakeMemoryStream);
            var xmlParser = new XmlParser<Document>(MapValues);
            var result = xmlParser.Read(mockStreamManager.Object.GetReadStream(""));
            Assert.AreEqual("TextResult", result.Text);
            Assert.AreEqual("TitleResult", result.Title);
        }

        [TestMethod]
        public void XmlParser_Write()
        {
            Mock<IStreamManager> mockStreamManager = new Mock<IStreamManager>();
            var document = new Document() { Text = "TextResult", Title = "TitleResult" };

            var mem = new MemoryStream();
            mockStreamManager.Setup(streamManager =>
            streamManager.GetWriteStream(It.IsAny<string>()))
                .Returns(() => mem);
            var xmlParser = new XmlParser<Document>(MapValues);
            xmlParser.Write(document, mockStreamManager.Object.GetWriteStream(""));
            var fakeContent = Encoding.UTF8.GetString(mem.ToArray());
            string resultData = System.IO.File.ReadAllText(@"..\\..\\Results\Result.xml");
            Assert.AreEqual(resultData, fakeContent);
        }
    }
}
