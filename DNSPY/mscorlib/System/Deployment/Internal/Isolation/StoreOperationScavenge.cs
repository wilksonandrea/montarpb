using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006AC RID: 1708
	internal struct StoreOperationScavenge
	{
		// Token: 0x06004FE5 RID: 20453 RVA: 0x0011CAB4 File Offset: 0x0011ACB4
		public StoreOperationScavenge(bool Light, ulong SizeLimit, ulong RunLimit, uint ComponentLimit)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationScavenge));
			this.Flags = StoreOperationScavenge.OpFlags.Nothing;
			if (Light)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.Light;
			}
			this.SizeReclaimationLimit = SizeLimit;
			if (SizeLimit != 0UL)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitSize;
			}
			this.RuntimeLimit = RunLimit;
			if (RunLimit != 0UL)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitTime;
			}
			this.ComponentCountLimit = ComponentLimit;
			if (ComponentLimit != 0U)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitCount;
			}
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0011CB38 File Offset: 0x0011AD38
		public StoreOperationScavenge(bool Light)
		{
			this = new StoreOperationScavenge(Light, 0UL, 0UL, 0U);
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0011CB46 File Offset: 0x0011AD46
		public void Destroy()
		{
		}

		// Token: 0x0400225C RID: 8796
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400225D RID: 8797
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationScavenge.OpFlags Flags;

		// Token: 0x0400225E RID: 8798
		[MarshalAs(UnmanagedType.U8)]
		public ulong SizeReclaimationLimit;

		// Token: 0x0400225F RID: 8799
		[MarshalAs(UnmanagedType.U8)]
		public ulong RuntimeLimit;

		// Token: 0x04002260 RID: 8800
		[MarshalAs(UnmanagedType.U4)]
		public uint ComponentCountLimit;

		// Token: 0x02000C56 RID: 3158
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003796 RID: 14230
			Nothing = 0,
			// Token: 0x04003797 RID: 14231
			Light = 1,
			// Token: 0x04003798 RID: 14232
			LimitSize = 2,
			// Token: 0x04003799 RID: 14233
			LimitTime = 4,
			// Token: 0x0400379A RID: 14234
			LimitCount = 8
		}
	}
}
