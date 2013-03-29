using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
namespace Linq.Csv.Test
{
    [TestClass]
    public class CsvTests
    {
        private List<string> EmptyList = new List<string>(0);
        private List<MethodInfo> LargeList = new List<MethodInfo>(typeof(string).GetMethods());

        [TestMethod]
        public void TestHeaderGenerated()
        {
            List<string> data = new List<string>();

            var output = data.Csv(new { A = "MethodName", B = "Returns" }, x => x, x => x);

            Assert.AreEqual("MethodName, Returns\r\n", output);
        }

        [TestMethod]
        public void TestNoHeaderIsGenerated()
        {
            var data = EmptyList.Csv(p => p.Length);
            Assert.AreEqual("", data);
        }

        [TestMethod]
        public void TestContentIsCorrectlyGenerated()
        {
            var smallList = LargeList.GetRange(0, 1);
            var item = smallList.First();

            var data = smallList.Csv(p => p.Name);

            var expected = item.Name + "\r\n";
            Assert.AreEqual(expected, data);
        }

        [TestMethod]
        public void TestContentIsCorrectlyGeneratedForTwoColumns()
        {
            var smallList = LargeList.GetRange(0, 1);
            var item = smallList.First();

            var data = smallList.Csv(p => p.Name, p=>p.IsPublic);

            var expected = string.Format("{0}, {1}\r\n", item.Name, item.IsPublic);
            Assert.AreEqual(expected, data);
        }

        [TestMethod]
        public void TestQuoteysAreUsedWhenRequired()
        {
            var input = "field, with, commas";
            var list = new List<string>() { input };

            var data = list.Csv(p => p);

            var expected = string.Format("\"{0}\"\r\n", input);
            Assert.AreEqual(expected, data);
        }

        [TestMethod]
        public void TestQuoteysInQuoteys()
        {
            var input = "field, with, commas and quotes \"";
            var expected = "\"field, with, commas and quotes \"\"\"\r\n";
            var list = new List<string>() { input };

            var data = list.Csv(p => p);

            Assert.AreEqual(expected, data);
        }

        [TestMethod]
        public void TestMultipleItems()
        {
            var input = Enumerable.Range(1, 2);
            var expected = "one, two\r\none, two\r\n";
            var data = input.Csv(p => "one", p => "two");
            Assert.AreEqual(expected, data);
        }
    }
}
