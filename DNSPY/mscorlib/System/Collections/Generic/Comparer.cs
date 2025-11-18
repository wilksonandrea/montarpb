using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004BA RID: 1210
	[TypeDependency("System.Collections.Generic.ObjectComparer`1")]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Comparer<T> : IComparer, IComparer<T>
	{
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000DDA72 File Offset: 0x000DBC72
		[__DynamicallyInvokable]
		public static Comparer<T> Default
		{
			[__DynamicallyInvokable]
			get
			{
				return Comparer<T>.defaultComparer;
			}
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x000DDA79 File Offset: 0x000DBC79
		[__DynamicallyInvokable]
		public static Comparer<T> Create(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			return new ComparisonComparer<T>(comparison);
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000DDA90 File Offset: 0x000DBC90
		[SecuritySafeCritical]
		private static Comparer<T> CreateComparer()
		{
			RuntimeType runtimeType = (RuntimeType)typeof(T);
			if (typeof(IComparable<T>).IsAssignableFrom(runtimeType))
			{
				return (Comparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericComparer<int>), runtimeType);
			}
			if (runtimeType.IsGenericType && runtimeType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				RuntimeType runtimeType2 = (RuntimeType)runtimeType.GetGenericArguments()[0];
				if (typeof(IComparable<>).MakeGenericType(new Type[] { runtimeType2 }).IsAssignableFrom(runtimeType2))
				{
					return (Comparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(NullableComparer<int>), runtimeType2);
				}
			}
			return new ObjectComparer<T>();
		}

		// Token: 0x06003A2B RID: 14891
		[__DynamicallyInvokable]
		public abstract int Compare(T x, T y);

		// Token: 0x06003A2C RID: 14892 RVA: 0x000DDB48 File Offset: 0x000DBD48
		[__DynamicallyInvokable]
		int IComparer.Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				if (x is T && y is T)
				{
					return this.Compare((T)((object)x), (T)((object)y));
				}
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
				return 0;
			}
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000DDB83 File Offset: 0x000DBD83
		[__DynamicallyInvokable]
		protected Comparer()
		{
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000DDB8B File Offset: 0x000DBD8B
		// Note: this type is marked as 'beforefieldinit'.
		static Comparer()
		{
		}

		// Token: 0x04001941 RID: 6465
		private static readonly Comparer<T> defaultComparer = Comparer<T>.CreateComparer();
	}
}
