using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections
{
	// Token: 0x02000493 RID: 1171
	[ComVisible(true)]
	[Serializable]
	public sealed class Comparer : IComparer, ISerializable
	{
		// Token: 0x06003835 RID: 14389 RVA: 0x000D7A1F File Offset: 0x000D5C1F
		private Comparer()
		{
			this.m_compareInfo = null;
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000D7A2E File Offset: 0x000D5C2E
		public Comparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.m_compareInfo = culture.CompareInfo;
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000D7A50 File Offset: 0x000D5C50
		private Comparer(SerializationInfo info, StreamingContext context)
		{
			this.m_compareInfo = null;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (name == "CompareInfo")
				{
					this.m_compareInfo = (CompareInfo)info.GetValue("CompareInfo", typeof(CompareInfo));
				}
			}
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000D7AB0 File Offset: 0x000D5CB0
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this.m_compareInfo != null)
			{
				string text = a as string;
				string text2 = b as string;
				if (text != null && text2 != null)
				{
					return this.m_compareInfo.Compare(text, text2);
				}
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			IComparable comparable2 = b as IComparable;
			if (comparable2 != null)
			{
				return -comparable2.CompareTo(a);
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000D7B2B File Offset: 0x000D5D2B
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_compareInfo != null)
			{
				info.AddValue("CompareInfo", this.m_compareInfo);
			}
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000D7B54 File Offset: 0x000D5D54
		// Note: this type is marked as 'beforefieldinit'.
		static Comparer()
		{
		}

		// Token: 0x040018D8 RID: 6360
		private CompareInfo m_compareInfo;

		// Token: 0x040018D9 RID: 6361
		public static readonly Comparer Default = new Comparer(CultureInfo.CurrentCulture);

		// Token: 0x040018DA RID: 6362
		public static readonly Comparer DefaultInvariant = new Comparer(CultureInfo.InvariantCulture);

		// Token: 0x040018DB RID: 6363
		private const string CompareInfoName = "CompareInfo";
	}
}
