using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000741 RID: 1857
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SerializationException : SystemException
	{
		// Token: 0x060051DA RID: 20954 RVA: 0x0011FC23 File Offset: 0x0011DE23
		[__DynamicallyInvokable]
		public SerializationException()
			: base(SerializationException._nullMessage)
		{
			base.SetErrorCode(-2146233076);
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x0011FC3B File Offset: 0x0011DE3B
		[__DynamicallyInvokable]
		public SerializationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233076);
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0011FC4F File Offset: 0x0011DE4F
		[__DynamicallyInvokable]
		public SerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233076);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x0011FC64 File Offset: 0x0011DE64
		protected SerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x0011FC6E File Offset: 0x0011DE6E
		// Note: this type is marked as 'beforefieldinit'.
		static SerializationException()
		{
		}

		// Token: 0x0400244C RID: 9292
		private static string _nullMessage = Environment.GetResourceString("Arg_SerializationException");
	}
}
