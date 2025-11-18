using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Plugin.Core.Colorful;

// Token: 0x020000FB RID: 251
internal static class Class23
{
	// Token: 0x0600095B RID: 2395 RVA: 0x000078A9 File Offset: 0x00005AA9
	internal static IEnumerable<T> smethod_0<T>(this IEnumerable<T> ienumerable_0) where T : IPrototypable<T>
	{
		Class23.Class27<T> @class = new Class23.Class27<T>(-2);
		@class.ienumerable_1 = ienumerable_0;
		return @class;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x000078B9 File Offset: 0x00005AB9
	internal static IEnumerable<T> smethod_1<T>(this IEnumerable<T> ienumerable_0) where T : struct
	{
		Class23.Class26<T> @class = new Class23.Class26<T>(-2);
		@class.ienumerable_1 = ienumerable_0;
		return @class;
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x000214E8 File Offset: 0x0001F6E8
	internal static string smethod_2<T>(this T gparam_0)
	{
		if (Class23.Class24<T>.callSite_1 == null)
		{
			Class23.Class24<T>.callSite_1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(Class23)));
		}
		Func<CallSite, object, string> target = Class23.Class24<T>.callSite_1.Target;
		CallSite callSite_ = Class23.Class24<T>.callSite_1;
		if (Class23.Class24<T>.callSite_0 == null)
		{
			Class23.Class24<T>.callSite_0 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Join", null, typeof(Class23), new CSharpArgumentInfo[]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
			}));
		}
		return target(callSite_, Class23.Class24<T>.callSite_0.Target(Class23.Class24<T>.callSite_0, typeof(string), string.Empty, gparam_0));
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x000215AC File Offset: 0x0001F7AC
	[return: Dynamic]
	internal static dynamic smethod_3<T>(this T gparam_0)
	{
		List<object> list = new List<object>();
		object[] array = gparam_0 as object[];
		if (array != null)
		{
			foreach (object obj in array)
			{
				if (Class23.Class25<T>.callSite_0 == null)
				{
					Class23.Class25<T>.callSite_0 = CallSite<Action<CallSite, List<object>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(Class23), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Class23.Class25<T>.callSite_0.Target(Class23.Class25<T>.callSite_0, list, obj);
			}
		}
		else
		{
			if (Class23.Class25<T>.callSite_1 == null)
			{
				Class23.Class25<T>.callSite_1 = CallSite<Action<CallSite, List<object>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(Class23), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Class23.Class25<T>.callSite_1.Target(Class23.Class25<T>.callSite_1, list, gparam_0);
		}
		return list.ToArray();
	}

	// Token: 0x020000FC RID: 252
	[CompilerGenerated]
	private static class Class24<T>
	{
		// Token: 0x040006EF RID: 1775
		public static CallSite<Func<CallSite, Type, string, object, object>> callSite_0;

		// Token: 0x040006F0 RID: 1776
		public static CallSite<Func<CallSite, object, string>> callSite_1;
	}

	// Token: 0x020000FD RID: 253
	[CompilerGenerated]
	private static class Class25<T>
	{
		// Token: 0x040006F1 RID: 1777
		public static CallSite<Action<CallSite, List<object>, object>> callSite_0;

		// Token: 0x040006F2 RID: 1778
		public static CallSite<Action<CallSite, List<object>, object>> callSite_1;
	}

	// Token: 0x020000FE RID: 254
	[CompilerGenerated]
	private sealed class Class26<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T : struct
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x000078C9 File Offset: 0x00005AC9
		[DebuggerHidden]
		public Class26(int int_2)
		{
			this.int_0 = int_2;
			this.int_1 = Environment.CurrentManagedThreadId;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000216A8 File Offset: 0x0001F8A8
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.int_0;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.method_0();
				}
			}
			this.ienumerator_0 = null;
			this.int_0 = -2;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000216F0 File Offset: 0x0001F8F0
		bool IEnumerator.MoveNext()
		{
			bool flag;
			try
			{
				int num = this.int_0;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.int_0 = -3;
				}
				else
				{
					this.int_0 = -1;
					this.ienumerator_0 = ienumerable_0.GetEnumerator();
					this.int_0 = -3;
				}
				if (!this.ienumerator_0.MoveNext())
				{
					this.method_0();
					this.ienumerator_0 = null;
					flag = false;
				}
				else
				{
					T t = this.ienumerator_0.Current;
					this.gparam_0 = t;
					this.int_0 = 1;
					flag = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return flag;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000078E3 File Offset: 0x00005AE3
		private void method_0()
		{
			this.int_0 = -1;
			if (this.ienumerator_0 != null)
			{
				this.ienumerator_0.Dispose();
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x000078FF File Offset: 0x00005AFF
		T IEnumerator<T>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.gparam_0;
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00007907 File Offset: 0x00005B07
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0000790E File Offset: 0x00005B0E
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.gparam_0;
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00021790 File Offset: 0x0001F990
		[DebuggerHidden]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			Class23.Class26<T> @class;
			if (this.int_0 == -2 && this.int_1 == Environment.CurrentManagedThreadId)
			{
				this.int_0 = 0;
				@class = this;
			}
			else
			{
				@class = new Class23.Class26<T>(0);
			}
			@class.ienumerable_0 = ienumerable_0;
			return @class;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0000791B File Offset: 0x00005B1B
		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
		}

		// Token: 0x040006F3 RID: 1779
		private int int_0;

		// Token: 0x040006F4 RID: 1780
		private T gparam_0;

		// Token: 0x040006F5 RID: 1781
		private int int_1;

		// Token: 0x040006F6 RID: 1782
		private IEnumerable<T> ienumerable_0;

		// Token: 0x040006F7 RID: 1783
		public IEnumerable<T> ienumerable_1;

		// Token: 0x040006F8 RID: 1784
		private IEnumerator<T> ienumerator_0;
	}

	// Token: 0x020000FF RID: 255
	[CompilerGenerated]
	private sealed class Class27<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T : IPrototypable<T>
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x00007923 File Offset: 0x00005B23
		[DebuggerHidden]
		public Class27(int int_2)
		{
			this.int_0 = int_2;
			this.int_1 = Environment.CurrentManagedThreadId;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x000217D4 File Offset: 0x0001F9D4
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.int_0;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.method_0();
				}
			}
			this.ienumerator_0 = null;
			this.int_0 = -2;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002181C File Offset: 0x0001FA1C
		bool IEnumerator.MoveNext()
		{
			bool flag;
			try
			{
				int num = this.int_0;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.int_0 = -3;
				}
				else
				{
					this.int_0 = -1;
					this.ienumerator_0 = ienumerable_0.GetEnumerator();
					this.int_0 = -3;
				}
				if (!this.ienumerator_0.MoveNext())
				{
					this.method_0();
					this.ienumerator_0 = null;
					flag = false;
				}
				else
				{
					T t = this.ienumerator_0.Current;
					this.gparam_0 = t.Prototype();
					this.int_0 = 1;
					flag = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return flag;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0000793D File Offset: 0x00005B3D
		private void method_0()
		{
			this.int_0 = -1;
			if (this.ienumerator_0 != null)
			{
				this.ienumerator_0.Dispose();
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00007959 File Offset: 0x00005B59
		T IEnumerator<T>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.gparam_0;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00007907 File Offset: 0x00005B07
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00007961 File Offset: 0x00005B61
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.gparam_0;
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000218C8 File Offset: 0x0001FAC8
		[DebuggerHidden]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			Class23.Class27<T> @class;
			if (this.int_0 == -2 && this.int_1 == Environment.CurrentManagedThreadId)
			{
				this.int_0 = 0;
				@class = this;
			}
			else
			{
				@class = new Class23.Class27<T>(0);
			}
			@class.ienumerable_0 = ienumerable_0;
			return @class;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0000796E File Offset: 0x00005B6E
		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
		}

		// Token: 0x040006F9 RID: 1785
		private int int_0;

		// Token: 0x040006FA RID: 1786
		private T gparam_0;

		// Token: 0x040006FB RID: 1787
		private int int_1;

		// Token: 0x040006FC RID: 1788
		private IEnumerable<T> ienumerable_0;

		// Token: 0x040006FD RID: 1789
		public IEnumerable<T> ienumerable_1;

		// Token: 0x040006FE RID: 1790
		private IEnumerator<T> ienumerator_0;
	}
}
