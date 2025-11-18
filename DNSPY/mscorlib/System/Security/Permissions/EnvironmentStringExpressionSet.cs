using System;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002DD RID: 733
	[Serializable]
	internal class EnvironmentStringExpressionSet : StringExpressionSet
	{
		// Token: 0x060025B5 RID: 9653 RVA: 0x0008946B File Offset: 0x0008766B
		public EnvironmentStringExpressionSet()
			: base(true, null, false)
		{
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00089476 File Offset: 0x00087676
		public EnvironmentStringExpressionSet(string str)
			: base(true, str, false)
		{
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00089481 File Offset: 0x00087681
		protected override StringExpressionSet CreateNewEmpty()
		{
			return new EnvironmentStringExpressionSet();
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00089488 File Offset: 0x00087688
		protected override bool StringSubsetString(string left, string right, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return string.Compare(left, right, StringComparison.Ordinal) == 0;
			}
			return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000894A4 File Offset: 0x000876A4
		protected override string ProcessWholeString(string str)
		{
			return str;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000894A7 File Offset: 0x000876A7
		protected override string ProcessSingleString(string str)
		{
			return str;
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000894AA File Offset: 0x000876AA
		[SecuritySafeCritical]
		public override string ToString()
		{
			return base.UnsafeToString();
		}
	}
}
