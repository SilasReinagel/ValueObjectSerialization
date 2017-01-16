using CSharpDtoSerialization.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValueObjectSerialization.Tests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void Deserialization_SimpleDto_DeserializedCorrectly()
        {
            var src = new SimpleDto("Donkey Kong", 1981);
            var serializable = new ReflectionSerializable(src);

            var dest = new ReflectionDeserialized<SimpleDto>(serializable).Create();

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Year, dest.Year);
        }

        [TestMethod]
        public void Deserialization_ComplexDto_MatchesOriginal()
        {
            var src = new ComplexDto("I Am Complex", new SimpleDto("I Am Simple", 1234));
            var serializable = new ReflectionSerializable(src);

            var dest = new ReflectionDeserialized<ComplexDto>(serializable).Create();

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Inner.Name, dest.Inner.Name);
            Assert.AreEqual(src.Inner.Year, dest.Inner.Year);
        }

        [TestMethod]
        public void Deserialization_InheritedDto_MatchesOriginal()
        {
            var src = new InheritedDto("Cloud Strife", 1997, "Grey");
            var serializable = new ReflectionSerializable(src);

            var dest = new ReflectionDeserialized<InheritedDto>(serializable).Create();

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Year, dest.Year);
            Assert.AreEqual(src.Color, dest.Color);
        }

        [TestMethod]
        public void Deserialization_ToSimplerType_IsCorrect()
        {
            var src = new InheritedDto("Cloud Strife", 1997, "Grey");
            var serializable = new ReflectionSerializable(src);

            var dest = new ReflectionDeserialized<SimpleDto>(serializable).Create();

            Assert.AreEqual(src.Name, dest.Name);
            Assert.AreEqual(src.Year, dest.Year);
        }
    }
}
