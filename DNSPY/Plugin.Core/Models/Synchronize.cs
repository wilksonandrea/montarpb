using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200007C RID: 124
	public class Synchronize
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000515F File Offset: 0x0000335F
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x00005167 File Offset: 0x00003367
		public int RemotePort
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00005170 File Offset: 0x00003370
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00005178 File Offset: 0x00003378
		public IPEndPoint Connection
		{
			[CompilerGenerated]
			get
			{
				return this.ipendPoint_0;
			}
			[CompilerGenerated]
			set
			{
				this.ipendPoint_0 = value;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00005181 File Offset: 0x00003381
		public Synchronize(string string_0, int int_1)
		{
			this.Connection = new IPEndPoint(IPAddress.Parse(string_0), int_1);
		}

		// Token: 0x0400023E RID: 574
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400023F RID: 575
		[CompilerGenerated]
		private IPEndPoint ipendPoint_0;
	}
}
