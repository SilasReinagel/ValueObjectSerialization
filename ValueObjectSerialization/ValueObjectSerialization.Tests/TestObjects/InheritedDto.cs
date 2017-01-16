
namespace CSharpDtoSerialization.Tests.TestObjects
{
    public class InheritedDto : SimpleDto
    {
        public string Color { get; private set; }

        public InheritedDto(string name, int year, string color) : base(name, year)
        {
            Color = color;
        }
    }
}
