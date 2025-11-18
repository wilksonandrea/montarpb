using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000746 RID: 1862
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct StreamingContext
	{
		// Token: 0x06005235 RID: 21045 RVA: 0x00120A46 File Offset: 0x0011EC46
		public StreamingContext(StreamingContextStates state)
		{
			this = new StreamingContext(state, null);
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x00120A50 File Offset: 0x0011EC50
		public StreamingContext(StreamingContextStates state, object additional)
		{
			this.m_state = state;
			this.m_additionalContext = additional;
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06005237 RID: 21047 RVA: 0x00120A60 File Offset: 0x0011EC60
		public object Context
		{
			get
			{
				return this.m_additionalContext;
			}
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00120A68 File Offset: 0x0011EC68
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is StreamingContext && (((StreamingContext)obj).m_additionalContext == this.m_additionalContext && ((StreamingContext)obj).m_state == this.m_state);
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x00120A9D File Offset: 0x0011EC9D
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this.m_state;
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x0600523A RID: 21050 RVA: 0x00120AA5 File Offset: 0x0011ECA5
		public StreamingContextStates State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x04002469 RID: 9321
		internal object m_additionalContext;

		// Token: 0x0400246A RID: 9322
		internal StreamingContextStates m_state;
	}
}
