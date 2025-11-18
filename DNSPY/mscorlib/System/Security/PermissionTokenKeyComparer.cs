using System;
using System.Collections;
using System.Globalization;

namespace System.Security
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	internal sealed class PermissionTokenKeyComparer : IEqualityComparer
	{
		// Token: 0x06001D26 RID: 7462 RVA: 0x00064D1E File Offset: 0x00062F1E
		public PermissionTokenKeyComparer()
		{
			this._caseSensitiveComparer = new Comparer(CultureInfo.InvariantCulture);
			this._info = CultureInfo.InvariantCulture.TextInfo;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00064D48 File Offset: 0x00062F48
		[SecuritySafeCritical]
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text == null || text2 == null)
			{
				return this._caseSensitiveComparer.Compare(a, b);
			}
			int num = this._caseSensitiveComparer.Compare(a, b);
			if (num == 0)
			{
				return 0;
			}
			if (SecurityManager.IsSameType(text, text2))
			{
				return 0;
			}
			return num;
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x00064D96 File Offset: 0x00062F96
		public bool Equals(object a, object b)
		{
			return a == b || (a != null && b != null && this.Compare(a, b) == 0);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00064DB4 File Offset: 0x00062FB4
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			int num = text.IndexOf(',');
			if (num == -1)
			{
				num = text.Length;
			}
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				num2 = (num2 << 7) ^ (int)text[i] ^ (num2 >> 25);
			}
			return num2;
		}

		// Token: 0x04000A2C RID: 2604
		private Comparer _caseSensitiveComparer;

		// Token: 0x04000A2D RID: 2605
		private TextInfo _info;
	}
}
