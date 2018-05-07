

namespace Framework.Errors.Tests
{
    public class ItemTest
    {
        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
