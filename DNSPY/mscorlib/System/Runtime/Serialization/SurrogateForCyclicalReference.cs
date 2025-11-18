using System;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000732 RID: 1842
	internal sealed class SurrogateForCyclicalReference : ISerializationSurrogate
	{
		// Token: 0x060051A9 RID: 20905 RVA: 0x0011FB16 File Offset: 0x0011DD16
		internal SurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
		{
			if (innerSurrogate == null)
			{
				throw new ArgumentNullException("innerSurrogate");
			}
			this.innerSurrogate = innerSurrogate;
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x0011FB33 File Offset: 0x0011DD33
		[SecurityCritical]
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			this.innerSurrogate.GetObjectData(obj, info, context);
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0011FB43 File Offset: 0x0011DD43
		[SecurityCritical]
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			return this.innerSurrogate.SetObjectData(obj, info, context, selector);
		}

		// Token: 0x04002447 RID: 9287
		private ISerializationSurrogate innerSurrogate;
	}
}
