using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime.ExceptionServices
{
	// Token: 0x020007AB RID: 1963
	public class FirstChanceExceptionEventArgs : EventArgs
	{
		// Token: 0x06005508 RID: 21768 RVA: 0x0012E073 File Offset: 0x0012C273
		public FirstChanceExceptionEventArgs(Exception exception)
		{
			this.m_Exception = exception;
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06005509 RID: 21769 RVA: 0x0012E082 File Offset: 0x0012C282
		public Exception Exception
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x04002727 RID: 10023
		private Exception m_Exception;
	}
}
