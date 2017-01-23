using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValueObjectSerialization.Tests.TestObjects;

namespace ValueObjectSerialization.Tests
{
    [TestClass]
    public class NewtonsoftTests
    {
        [TestMethod]
        public void NewtonsoftDeserialization_SimpleValueObject_MatchesOriginal()
        {
            var src = new SimpleValueObject("Donkey Kong", 1981);

            var dest = GetDeserialized(src);

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Year, dest.Year);
        }

        [TestMethod]
        public void NewtonsoftDeserialization_ComplexValueObject_MatchesOriginal()
        {
            var src = new ComplexValueObject("I Am Complex", new SimpleValueObject("I Am Simple", 1234));

            var dest = GetDeserialized(src);

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Inner.Name, dest.Inner.Name);
            Assert.AreEqual(src.Inner.Year, dest.Inner.Year);
        }

        [TestMethod]
        public void NewtonsoftDeserialization_InheritedValueObject_MatchesOriginal()
        {
            var src = new InheritedValueObject("Cloud Strife", 1997, "Sword");

            var dest = GetDeserialized(src);

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Year, dest.Year);
            Assert.AreEqual(src.Weapon, dest.Weapon);
        }

        [TestMethod]
        public void NewtonsoftDeserialization_ToSimplerType_IsCorrect()
        {
            var src = new InheritedValueObject("William Blazkowicz", 2001, "MP40");

            var dest = GetDeserialized<SimpleValueObject>(src);

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Year, dest.Year);
        }

        private T GetDeserialized<T>(T src)
        {
            var bytes = new NewtonsoftSerializedBytes(src).Create();
            return new NewtonsoftDeserialized<T>(bytes).Create();
        }
    }
}
