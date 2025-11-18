using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000075 RID: 117
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001392B File Offset: 0x00011B2B
		public static StringComparer InvariantCulture
		{
			get
			{
				return StringComparer._invariantCulture;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00013932 File Offset: 0x00011B32
		public static StringComparer InvariantCultureIgnoreCase
		{
			get
			{
				return StringComparer._invariantCultureIgnoreCase;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00013939 File Offset: 0x00011B39
		[__DynamicallyInvokable]
		public static StringComparer CurrentCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, false);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00013946 File Offset: 0x00011B46
		[__DynamicallyInvokable]
		public static StringComparer CurrentCultureIgnoreCase
		{
			[__DynamicallyInvokable]
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, true);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00013953 File Offset: 0x00011B53
		[__DynamicallyInvokable]
		public static StringComparer Ordinal
		{
			[__DynamicallyInvokable]
			get
			{
				return StringComparer._ordinal;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001395A File Offset: 0x00011B5A
		[__DynamicallyInvokable]
		public static StringComparer OrdinalIgnoreCase
		{
			[__DynamicallyInvokable]
			get
			{
				return StringComparer._ordinalIgnoreCase;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00013961 File Offset: 0x00011B61
		[__DynamicallyInvokable]
		public static StringComparer Create(CultureInfo culture, bool ignoreCase)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return new CultureAwareComparer(culture, ignoreCase);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00013978 File Offset: 0x00011B78
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Compare(text, text2);
				}
			}
			IComparable comparable = x as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(y);
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000139D4 File Offset: 0x00011BD4
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Equals(text, text2);
				}
			}
			return x.Equals(y);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00013A14 File Offset: 0x00011C14
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text != null)
			{
				return this.GetHashCode(text);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000580 RID: 1408
		[__DynamicallyInvokable]
		public abstract int Compare(string x, string y);

		// Token: 0x06000581 RID: 1409
		[__DynamicallyInvokable]
		public abstract bool Equals(string x, string y);

		// Token: 0x06000582 RID: 1410
		[__DynamicallyInvokable]
		public abstract int GetHashCode(string obj);

		// Token: 0x06000583 RID: 1411 RVA: 0x00013A47 File Offset: 0x00011C47
		[__DynamicallyInvokable]
		protected StringComparer()
		{
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00013A4F File Offset: 0x00011C4F
		// Note: this type is marked as 'beforefieldinit'.
		static StringComparer()
		{
		}

		// Token: 0x0400028E RID: 654
		private static readonly StringComparer _invariantCulture = new CultureAwareComparer(CultureInfo.InvariantCulture, false);

		// Token: 0x0400028F RID: 655
		private static readonly StringComparer _invariantCultureIgnoreCase = new CultureAwareComparer(CultureInfo.InvariantCulture, true);

		// Token: 0x04000290 RID: 656
		private static readonly StringComparer _ordinal = new OrdinalComparer(false);

		// Token: 0x04000291 RID: 657
		private static readonly StringComparer _ordinalIgnoreCase = new OrdinalComparer(true);
	}
}
