
namespace CSharpDtoSerialization.Tests.TestObjects
{
    public class SimpleDto
    {
        public string Name { get; private set; }
        public int Year { get; private set; }

        public SimpleDto(string name, int year)
        {
            Name = name;
            Year = year;
        }
    }
}
