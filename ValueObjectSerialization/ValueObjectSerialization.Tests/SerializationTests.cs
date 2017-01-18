using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValueObjectSerialization.Tests.TestObjects;

namespace ValueObjectSerialization.Tests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void Deserialization_SimpleValueObject_MatchesOriginal()
        {
            var src = new SimpleValueObject("Donkey Kong", 1981);
            var serializable = new ReflectionSerializable(src);

            var dest = new ReflectionDeserialized<SimpleValueObject>(serializable).Create();

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Year, dest.Year);
        }

        [TestMethod]
        public void Deserialization_ComplexValueObject_MatchesOriginal()
        {
            var src = new ComplexValueObject("I Am Complex", new SimpleValueObject("I Am Simple", 1234));
            var serializable = new ReflectionSerializable(src);

            var dest = new ReflectionDeserialized<ComplexValueObject>(serializable).Create();

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Inner.Name, dest.Inner.Name);
            Assert.AreEqual(src.Inner.Year, dest.Inner.Year);
        }

        [TestMethod]
        public void Deserialization_InheritedValueObject_MatchesOriginal()
        {
            var src = new InheritedValueObject("Cloud Strife", 1997, "Sword");
            var serializable = new ReflectionSerializable(src);

            var dest = new ReflectionDeserialized<InheritedValueObject>(serializable).Create();

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Year, dest.Year);
            Assert.AreEqual(src.Weapon, dest.Weapon);
        }

        [TestMethod]
        public void Deserialization_ToSimplerType_IsCorrect()
        {
            var src = new InheritedValueObject("William Blazkowicz", 2001, "MP40");
            var serializable = new ReflectionSerializable(src);

            var dest = new ReflectionDeserialized<SimpleValueObject>(serializable).Create();

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Year, dest.Year);
        }
    }
}
