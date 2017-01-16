
namespace CSharpDtoSerialization.Tests.TestObjects
{
    public class ComplexDto
    {
        public string Name { get; private set; }
        public SimpleDto Inner { get; private set; }

        public ComplexDto(string name, SimpleDto inner)
        {
            Name = name;
            Inner = inner;
        }
    }
}
