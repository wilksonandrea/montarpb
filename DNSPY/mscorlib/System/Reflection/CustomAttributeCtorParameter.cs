using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005DD RID: 1501
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeCtorParameter
	{
		// Token: 0x06004572 RID: 17778 RVA: 0x000FF370 File Offset: 0x000FD570
		public CustomAttributeCtorParameter(CustomAttributeType type)
		{
			this.m_type = type;
			this.m_encodedArgument = default(CustomAttributeEncodedArgument);
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x000FF385 File Offset: 0x000FD585
		public CustomAttributeEncodedArgument CustomAttributeEncodedArgument
		{
			get
			{
				return this.m_encodedArgument;
			}
		}

		// Token: 0x04001C8C RID: 7308
		private CustomAttributeType m_type;

		// Token: 0x04001C8D RID: 7309
		private CustomAttributeEncodedArgument m_encodedArgument;
	}
}
