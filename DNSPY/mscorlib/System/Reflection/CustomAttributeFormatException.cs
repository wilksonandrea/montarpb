using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005D0 RID: 1488
	[ComVisible(true)]
	[Serializable]
	public class CustomAttributeFormatException : FormatException
	{
		// Token: 0x060044E2 RID: 17634 RVA: 0x000FD0AC File Offset: 0x000FB2AC
		public CustomAttributeFormatException()
			: base(Environment.GetResourceString("Arg_CustomAttributeFormatException"))
		{
			base.SetErrorCode(-2146232827);
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x000FD0C9 File Offset: 0x000FB2C9
		public CustomAttributeFormatException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232827);
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x000FD0DD File Offset: 0x000FB2DD
		public CustomAttributeFormatException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232827);
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x000FD0F2 File Offset: 0x000FB2F2
		protected CustomAttributeFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
