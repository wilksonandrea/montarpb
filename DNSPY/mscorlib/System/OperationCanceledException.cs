using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	// Token: 0x0200011D RID: 285
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class OperationCanceledException : SystemException
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00032C9A File Offset: 0x00030E9A
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x00032CA2 File Offset: 0x00030EA2
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this._cancellationToken;
			}
			private set
			{
				this._cancellationToken = value;
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00032CAB File Offset: 0x00030EAB
		[__DynamicallyInvokable]
		public OperationCanceledException()
			: base(Environment.GetResourceString("OperationCanceled"))
		{
			base.SetErrorCode(-2146233029);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00032CC8 File Offset: 0x00030EC8
		[__DynamicallyInvokable]
		public OperationCanceledException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233029);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00032CDC File Offset: 0x00030EDC
		[__DynamicallyInvokable]
		public OperationCanceledException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233029);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00032CF1 File Offset: 0x00030EF1
		[__DynamicallyInvokable]
		public OperationCanceledException(CancellationToken token)
			: this()
		{
			this.CancellationToken = token;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00032D00 File Offset: 0x00030F00
		[__DynamicallyInvokable]
		public OperationCanceledException(string message, CancellationToken token)
			: this(message)
		{
			this.CancellationToken = token;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00032D10 File Offset: 0x00030F10
		[__DynamicallyInvokable]
		public OperationCanceledException(string message, Exception innerException, CancellationToken token)
			: this(message, innerException)
		{
			this.CancellationToken = token;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00032D21 File Offset: 0x00030F21
		protected OperationCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040005D3 RID: 1491
		[NonSerialized]
		private CancellationToken _cancellationToken;
	}
}
