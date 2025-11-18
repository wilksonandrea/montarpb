using System;

namespace System.Threading
{
	// Token: 0x0200053F RID: 1343
	internal static class LazyHelpers<T>
	{
		// Token: 0x06003EEA RID: 16106 RVA: 0x000E9DB4 File Offset: 0x000E7FB4
		private static T ActivatorFactorySelector()
		{
			T t;
			try
			{
				t = (T)((object)Activator.CreateInstance(typeof(T)));
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
			}
			return t;
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x000E9DFC File Offset: 0x000E7FFC
		// Note: this type is marked as 'beforefieldinit'.
		static LazyHelpers()
		{
		}

		// Token: 0x04001A77 RID: 6775
		internal static Func<T> s_activatorFactorySelector = new Func<T>(LazyHelpers<T>.ActivatorFactorySelector);
	}
}
