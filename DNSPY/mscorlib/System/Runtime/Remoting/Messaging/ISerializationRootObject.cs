using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200086E RID: 2158
	internal interface ISerializationRootObject
	{
		// Token: 0x06005BCD RID: 23501
		[SecurityCritical]
		void RootSetObjectData(SerializationInfo info, StreamingContext ctx);
	}
}
