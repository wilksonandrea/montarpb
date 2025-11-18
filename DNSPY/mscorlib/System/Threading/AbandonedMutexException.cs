using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020004E4 RID: 1252
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class AbandonedMutexException : SystemException
	{
		// Token: 0x06003B71 RID: 15217 RVA: 0x000E20FB File Offset: 0x000E02FB
		[__DynamicallyInvokable]
		public AbandonedMutexException()
			: base(Environment.GetResourceString("Threading.AbandonedMutexException"))
		{
			base.SetErrorCode(-2146233043);
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000E211F File Offset: 0x000E031F
		[__DynamicallyInvokable]
		public AbandonedMutexException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233043);
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x000E213A File Offset: 0x000E033A
		[__DynamicallyInvokable]
		public AbandonedMutexException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233043);
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x000E2156 File Offset: 0x000E0356
		[__DynamicallyInvokable]
		public AbandonedMutexException(int location, WaitHandle handle)
			: base(Environment.GetResourceString("Threading.AbandonedMutexException"))
		{
			base.SetErrorCode(-2146233043);
			this.SetupException(location, handle);
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x000E2182 File Offset: 0x000E0382
		[__DynamicallyInvokable]
		public AbandonedMutexException(string message, int location, WaitHandle handle)
			: base(message)
		{
			base.SetErrorCode(-2146233043);
			this.SetupException(location, handle);
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000E21A5 File Offset: 0x000E03A5
		[__DynamicallyInvokable]
		public AbandonedMutexException(string message, Exception inner, int location, WaitHandle handle)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233043);
			this.SetupException(location, handle);
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x000E21CA File Offset: 0x000E03CA
		private void SetupException(int location, WaitHandle handle)
		{
			this.m_MutexIndex = location;
			if (handle != null)
			{
				this.m_Mutex = handle as Mutex;
			}
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x000E21E2 File Offset: 0x000E03E2
		protected AbandonedMutexException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000E21F3 File Offset: 0x000E03F3
		[__DynamicallyInvokable]
		public Mutex Mutex
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Mutex;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000E21FB File Offset: 0x000E03FB
		[__DynamicallyInvokable]
		public int MutexIndex
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_MutexIndex;
			}
		}

		// Token: 0x04001965 RID: 6501
		private int m_MutexIndex = -1;

		// Token: 0x04001966 RID: 6502
		private Mutex m_Mutex;
	}
}
