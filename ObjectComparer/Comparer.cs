using ObjectComparer.Helper;

namespace ObjectComparer
{
    public static class Comparer
    {
        public static string AreSimilar<T>(T first, T second )
        {
            /// Add your implementation logic here. Feel free to add classes and types as required for your solution.
            var result =  CompareHelper.CompareObjects(first, second);
            if (result)
            {
                return StandardCode.SUCCESS;
            }

            return StandardCode.FAILED;
        }
    }
}
