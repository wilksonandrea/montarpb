namespace Plugin.Core.Utility
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ArrayEXT
    {
        public static void ForEach(this Array array, Action<Array, int[]> action)
        {
            if (array.LongLength != 0)
            {
                Class8 class2 = new Class8(array);
                while (true)
                {
                    action(array, class2.int_0);
                    if (!class2.method_0())
                    {
                        return;
                    }
                }
            }
        }
    }
}

