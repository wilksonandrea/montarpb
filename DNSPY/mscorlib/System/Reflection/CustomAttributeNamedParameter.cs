using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005DC RID: 1500
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeNamedParameter
	{
		// Token: 0x06004570 RID: 17776 RVA: 0x000FF330 File Offset: 0x000FD530
		public CustomAttributeNamedParameter(string argumentName, CustomAttributeEncoding fieldOrProperty, CustomAttributeType type)
		{
			if (argumentName == null)
			{
				throw new ArgumentNullException("argumentName");
			}
			this.m_argumentName = argumentName;
			this.m_fieldOrProperty = fieldOrProperty;
			this.m_padding = fieldOrProperty;
			this.m_type = type;
			this.m_encodedArgument = default(CustomAttributeEncodedArgument);
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06004571 RID: 17777 RVA: 0x000FF368 File Offset: 0x000FD568
		public CustomAttributeEncodedArgument EncodedArgument
		{
			get
			{
				return this.m_encodedArgument;
			}
		}

		// Token: 0x04001C87 RID: 7303
		private string m_argumentName;

		// Token: 0x04001C88 RID: 7304
		private CustomAttributeEncoding m_fieldOrProperty;

		// Token: 0x04001C89 RID: 7305
		private CustomAttributeEncoding m_padding;

		// Token: 0x04001C8A RID: 7306
		private CustomAttributeType m_type;

		// Token: 0x04001C8B RID: 7307
		private CustomAttributeEncodedArgument m_encodedArgument;
	}
}
