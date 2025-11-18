using System;

namespace System.Runtime.ExceptionServices
{
	// Token: 0x020007AC RID: 1964
	[__DynamicallyInvokable]
	public sealed class ExceptionDispatchInfo
	{
		// Token: 0x0600550A RID: 21770 RVA: 0x0012E08C File Offset: 0x0012C28C
		private ExceptionDispatchInfo(Exception exception)
		{
			this.m_Exception = exception;
			this.m_remoteStackTrace = exception.RemoteStackTrace;
			object obj;
			object obj2;
			this.m_Exception.GetStackTracesDeepCopy(out obj, out obj2);
			this.m_stackTrace = obj;
			this.m_dynamicMethods = obj2;
			this.m_IPForWatsonBuckets = exception.IPForWatsonBuckets;
			this.m_WatsonBuckets = exception.WatsonBuckets;
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x0600550B RID: 21771 RVA: 0x0012E0E7 File Offset: 0x0012C2E7
		internal UIntPtr IPForWatsonBuckets
		{
			get
			{
				return this.m_IPForWatsonBuckets;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x0012E0EF File Offset: 0x0012C2EF
		internal object WatsonBuckets
		{
			get
			{
				return this.m_WatsonBuckets;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x0012E0F7 File Offset: 0x0012C2F7
		internal object BinaryStackTraceArray
		{
			get
			{
				return this.m_stackTrace;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x0012E0FF File Offset: 0x0012C2FF
		internal object DynamicMethodArray
		{
			get
			{
				return this.m_dynamicMethods;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x0600550F RID: 21775 RVA: 0x0012E107 File Offset: 0x0012C307
		internal string RemoteStackTrace
		{
			get
			{
				return this.m_remoteStackTrace;
			}
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x0012E10F File Offset: 0x0012C30F
		[__DynamicallyInvokable]
		public static ExceptionDispatchInfo Capture(Exception source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			return new ExceptionDispatchInfo(source);
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06005511 RID: 21777 RVA: 0x0012E12F File Offset: 0x0012C32F
		[__DynamicallyInvokable]
		public Exception SourceException
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x0012E137 File Offset: 0x0012C337
		[__DynamicallyInvokable]
		public void Throw()
		{
			this.m_Exception.RestoreExceptionDispatchInfo(this);
			throw this.m_Exception;
		}

		// Token: 0x04002728 RID: 10024
		private Exception m_Exception;

		// Token: 0x04002729 RID: 10025
		private string m_remoteStackTrace;

		// Token: 0x0400272A RID: 10026
		private object m_stackTrace;

		// Token: 0x0400272B RID: 10027
		private object m_dynamicMethods;

		// Token: 0x0400272C RID: 10028
		private UIntPtr m_IPForWatsonBuckets;

		// Token: 0x0400272D RID: 10029
		private object m_WatsonBuckets;
	}
}
