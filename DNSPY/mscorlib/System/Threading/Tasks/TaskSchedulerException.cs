using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	// Token: 0x02000574 RID: 1396
	[__DynamicallyInvokable]
	[Serializable]
	public class TaskSchedulerException : Exception
	{
		// Token: 0x06004195 RID: 16789 RVA: 0x000F4AF6 File Offset: 0x000F2CF6
		[__DynamicallyInvokable]
		public TaskSchedulerException()
			: base(Environment.GetResourceString("TaskSchedulerException_ctor_DefaultMessage"))
		{
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x000F4B08 File Offset: 0x000F2D08
		[__DynamicallyInvokable]
		public TaskSchedulerException(string message)
			: base(message)
		{
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000F4B11 File Offset: 0x000F2D11
		[__DynamicallyInvokable]
		public TaskSchedulerException(Exception innerException)
			: base(Environment.GetResourceString("TaskSchedulerException_ctor_DefaultMessage"), innerException)
		{
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000F4B24 File Offset: 0x000F2D24
		[__DynamicallyInvokable]
		public TaskSchedulerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000F4B2E File Offset: 0x000F2D2E
		protected TaskSchedulerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
