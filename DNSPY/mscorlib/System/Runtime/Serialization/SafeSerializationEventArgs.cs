using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x02000755 RID: 1877
	public sealed class SafeSerializationEventArgs : EventArgs
	{
		// Token: 0x060052D3 RID: 21203 RVA: 0x00122DAE File Offset: 0x00120FAE
		internal SafeSerializationEventArgs(StreamingContext streamingContext)
		{
			this.m_streamingContext = streamingContext;
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x00122DC8 File Offset: 0x00120FC8
		public void AddSerializedState(ISafeSerializationData serializedState)
		{
			if (serializedState == null)
			{
				throw new ArgumentNullException("serializedState");
			}
			if (!serializedState.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_NonSerType", new object[]
				{
					serializedState.GetType(),
					serializedState.GetType().Assembly.FullName
				}));
			}
			this.m_serializedStates.Add(serializedState);
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060052D5 RID: 21205 RVA: 0x00122E2E File Offset: 0x0012102E
		internal IList<object> SerializedStates
		{
			get
			{
				return this.m_serializedStates;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060052D6 RID: 21206 RVA: 0x00122E36 File Offset: 0x00121036
		public StreamingContext StreamingContext
		{
			get
			{
				return this.m_streamingContext;
			}
		}

		// Token: 0x040024BD RID: 9405
		private StreamingContext m_streamingContext;

		// Token: 0x040024BE RID: 9406
		private List<object> m_serializedStates = new List<object>();
	}
}
