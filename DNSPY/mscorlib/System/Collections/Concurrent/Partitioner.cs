using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
	// Token: 0x020004B1 RID: 1201
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public abstract class Partitioner<TSource>
	{
		// Token: 0x06003992 RID: 14738
		[__DynamicallyInvokable]
		public abstract IList<IEnumerator<TSource>> GetPartitions(int partitionCount);

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000DC5EA File Offset: 0x000DA7EA
		[__DynamicallyInvokable]
		public virtual bool SupportsDynamicPartitions
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000DC5ED File Offset: 0x000DA7ED
		[__DynamicallyInvokable]
		public virtual IEnumerable<TSource> GetDynamicPartitions()
		{
			throw new NotSupportedException(Environment.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000DC5FE File Offset: 0x000DA7FE
		[__DynamicallyInvokable]
		protected Partitioner()
		{
		}
	}
}
