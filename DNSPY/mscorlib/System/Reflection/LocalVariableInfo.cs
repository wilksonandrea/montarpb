using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000616 RID: 1558
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public class LocalVariableInfo
	{
		// Token: 0x06004812 RID: 18450 RVA: 0x00105EAE File Offset: 0x001040AE
		[__DynamicallyInvokable]
		protected LocalVariableInfo()
		{
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x00105EB8 File Offset: 0x001040B8
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string text = this.LocalType.ToString() + " (" + this.LocalIndex.ToString() + ")";
			if (this.IsPinned)
			{
				text += " (pinned)";
			}
			return text;
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x00105F03 File Offset: 0x00104103
		[__DynamicallyInvokable]
		public virtual Type LocalType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06004815 RID: 18453 RVA: 0x00105F0B File Offset: 0x0010410B
		[__DynamicallyInvokable]
		public virtual bool IsPinned
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_isPinned != 0;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x00105F16 File Offset: 0x00104116
		[__DynamicallyInvokable]
		public virtual int LocalIndex
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_localIndex;
			}
		}

		// Token: 0x04001DE5 RID: 7653
		private RuntimeType m_type;

		// Token: 0x04001DE6 RID: 7654
		private int m_isPinned;

		// Token: 0x04001DE7 RID: 7655
		private int m_localIndex;
	}
}
