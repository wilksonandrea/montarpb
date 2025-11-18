using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085F RID: 2143
	[ComVisible(true)]
	public interface IRemotingFormatter : IFormatter
	{
		// Token: 0x06005A9D RID: 23197
		object Deserialize(Stream serializationStream, HeaderHandler handler);

		// Token: 0x06005A9E RID: 23198
		void Serialize(Stream serializationStream, object graph, Header[] headers);
	}
}
