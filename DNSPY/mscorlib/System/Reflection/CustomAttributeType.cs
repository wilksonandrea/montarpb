using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005DF RID: 1503
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeType
	{
		// Token: 0x06004576 RID: 17782 RVA: 0x000FF38D File Offset: 0x000FD58D
		public CustomAttributeType(CustomAttributeEncoding encodedType, CustomAttributeEncoding encodedArrayType, CustomAttributeEncoding encodedEnumType, string enumName)
		{
			this.m_encodedType = encodedType;
			this.m_encodedArrayType = encodedArrayType;
			this.m_encodedEnumType = encodedEnumType;
			this.m_enumName = enumName;
			this.m_padding = this.m_encodedType;
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06004577 RID: 17783 RVA: 0x000FF3B8 File Offset: 0x000FD5B8
		public CustomAttributeEncoding EncodedType
		{
			get
			{
				return this.m_encodedType;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06004578 RID: 17784 RVA: 0x000FF3C0 File Offset: 0x000FD5C0
		public CustomAttributeEncoding EncodedEnumType
		{
			get
			{
				return this.m_encodedEnumType;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x000FF3C8 File Offset: 0x000FD5C8
		public CustomAttributeEncoding EncodedArrayType
		{
			get
			{
				return this.m_encodedArrayType;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x000FF3D0 File Offset: 0x000FD5D0
		[ComVisible(true)]
		public string EnumName
		{
			get
			{
				return this.m_enumName;
			}
		}

		// Token: 0x04001C92 RID: 7314
		private string m_enumName;

		// Token: 0x04001C93 RID: 7315
		private CustomAttributeEncoding m_encodedType;

		// Token: 0x04001C94 RID: 7316
		private CustomAttributeEncoding m_encodedEnumType;

		// Token: 0x04001C95 RID: 7317
		private CustomAttributeEncoding m_encodedArrayType;

		// Token: 0x04001C96 RID: 7318
		private CustomAttributeEncoding m_padding;
	}
}
