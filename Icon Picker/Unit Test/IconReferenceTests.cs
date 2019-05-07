using NUnit.Framework;
using IconPicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconPicker.Tests
{
    [TestFixture]
    public class IconReferenceTests
    {
        IconReference subject;

        [SetUp]
        public void SetUp()
        {
            subject = null;
        }

        #region Constructor StringString

        [TestCase("filepath", "0")]
        [TestCase("filepath", "1")]
        [TestCase("filepath", "5")]
        [TestCase("filepath", "25")]
        public void Constructor_StringString_WorksWithValidValues(string filePath, string index)
        {
            subject = new IconReference(filePath, index);

            Assert.AreEqual(filePath, subject.FilePath);
            Assert.AreEqual(index, subject.IconIndex.ToString());
        }

        [TestCase("filepath", "-1")]
        [TestCase("filepath", "-5")]
        [TestCase("filepath", "-25")]
        public void Constructor_StringString_FailsWithNegativeValues(string filePath, string index)
        {
            ArgumentException e = Assert.Throws<ArgumentException>(() => new IconReference(filePath, index));

            Assert.AreEqual("Parameter [index] needs to be greater than or equal to zero", e.Message);
        }

        [TestCase("filepath", null)]
        [TestCase("filepath", "")]
        [TestCase("filepath", "TEST")]
        [TestCase("filepath", "--1")]
        public void Constructor_StringString_FailsWithNonCastableValues(string filePath, string index)
        {
            ArgumentException e = Assert.Throws<ArgumentException>(() => new IconReference(filePath, index));

            Assert.AreEqual("Parameter [index] needs to be castable to an integer", e.Message);
        }

        #endregion
        #region Constructor StringInteger

        [TestCase("filepath", 0)]
        [TestCase("filepath", 1)]
        [TestCase("filepath", 5)]
        [TestCase("filepath", 25)]
        public void Constructor_StringInteger_WorksWithValidValues(string filePath, int index)
        {
            subject = new IconReference(filePath, index);

            Assert.AreEqual(filePath, subject.FilePath);
            Assert.AreEqual(index, subject.IconIndex);
        }

        [TestCase("filepath", -1)]
        [TestCase("filepath", -5)]
        [TestCase("filepath", -25)]
        public void Constructor_StringInteger_FailsWithNegativeValues(string filePath, int index)
        {
            ArgumentException e = Assert.Throws<ArgumentException>(() => new IconReference(filePath, index));

            Assert.AreEqual("Parameter [index] needs to be greater than or equal to zero", e.Message);
        }

        #endregion
        #region Constructor String

        [TestCase("filepath", "0")]
        [TestCase("filepath", "1")]
        [TestCase("filepath", "5")]
        [TestCase("filepath", "25")]
        public void Constructor_String_WorksWithValidValues(string filePath, string index)
        {
            string reference = filePath + "," + index;

            subject = new IconReference(reference);

            Assert.AreEqual(filePath, subject.FilePath);
            Assert.AreEqual(index, subject.IconIndex.ToString());
        }

        [TestCase("filepath", "-1")]
        [TestCase("filepath", "-5")]
        [TestCase("filepath", "-25")]
        public void Constructor_String_FailsWithNegativeValues(string filePath, string index)
        {
            string reference = filePath + "," + index;

            ArgumentException e = Assert.Throws<ArgumentException>(() => new IconReference(reference));

            Assert.AreEqual("[reference] must be a valid file location followed by a comma and then an int", e.Message);
        }

        [TestCase("filepath,", null)]
        [TestCase("filepath,", "")]
        [TestCase("filepath,", "TEST")]
        [TestCase("filepath,", "--1")]
        [TestCase("filepath", "")]
        [TestCase("filepath", "1")]
        public void Constructor_String_FailsWithNonCastableValues(string filePath, string index)
        {
            string reference = filePath + index;

            ArgumentException e = Assert.Throws<ArgumentException>(() => new IconReference(reference));

            Assert.AreEqual("[reference] must be a valid file location followed by a comma and then an int", e.Message);
        }

        #endregion
    }
}