using System;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000162 RID: 354
	[NonVersionable]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Nullable<T> where T : struct
	{
		// Token: 0x060015CF RID: 5583 RVA: 0x00040484 File Offset: 0x0003E684
		[NonVersionable]
		[__DynamicallyInvokable]
		public Nullable(T value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00040494 File Offset: 0x0003E694
		[__DynamicallyInvokable]
		public bool HasValue
		{
			[NonVersionable]
			[__DynamicallyInvokable]
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0004049C File Offset: 0x0003E69C
		[__DynamicallyInvokable]
		public T Value
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.hasValue)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_NoValue);
				}
				return this.value;
			}
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000404B3 File Offset: 0x0003E6B3
		[NonVersionable]
		[__DynamicallyInvokable]
		public T GetValueOrDefault()
		{
			return this.value;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000404BB File Offset: 0x0003E6BB
		[NonVersionable]
		[__DynamicallyInvokable]
		public T GetValueOrDefault(T defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000404CD File Offset: 0x0003E6CD
		[__DynamicallyInvokable]
		public override bool Equals(object other)
		{
			if (!this.hasValue)
			{
				return other == null;
			}
			return other != null && this.value.Equals(other);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000404F3 File Offset: 0x0003E6F3
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (!this.hasValue)
			{
				return 0;
			}
			return this.value.GetHashCode();
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00040510 File Offset: 0x0003E710
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString();
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00040531 File Offset: 0x0003E731
		[NonVersionable]
		[__DynamicallyInvokable]
		public static implicit operator T?(T value)
		{
			return new T?(value);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00040539 File Offset: 0x0003E739
		[NonVersionable]
		[__DynamicallyInvokable]
		public static explicit operator T(T? value)
		{
			return value.Value;
		}

		// Token: 0x0400074A RID: 1866
		private bool hasValue;

		// Token: 0x0400074B RID: 1867
		internal T value;
	}
}
