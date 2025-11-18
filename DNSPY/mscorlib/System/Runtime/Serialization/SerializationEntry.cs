using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000744 RID: 1860
	[ComVisible(true)]
	public struct SerializationEntry
	{
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06005229 RID: 21033 RVA: 0x0012087E File Offset: 0x0011EA7E
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x0600522A RID: 21034 RVA: 0x00120886 File Offset: 0x0011EA86
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x0600522B RID: 21035 RVA: 0x0012088E File Offset: 0x0011EA8E
		public Type ObjectType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x00120896 File Offset: 0x0011EA96
		internal SerializationEntry(string entryName, object entryValue, Type entryType)
		{
			this.m_value = entryValue;
			this.m_name = entryName;
			this.m_type = entryType;
		}

		// Token: 0x04002460 RID: 9312
		private Type m_type;

		// Token: 0x04002461 RID: 9313
		private object m_value;

		// Token: 0x04002462 RID: 9314
		private string m_name;
	}
}
