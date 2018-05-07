using System.Collections.Generic;


namespace Framework.Errors.Tests
{
    public class ItemTestEqualityComparer : IEqualityComparer<ItemTest>
    {
        public bool Equals(ItemTest x, ItemTest y)
        {
            return x.Value.Equals(y.Value);
        }

        public int GetHashCode(ItemTest obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
