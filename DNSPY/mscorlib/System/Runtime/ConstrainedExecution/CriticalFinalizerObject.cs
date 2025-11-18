using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x0200072B RID: 1835
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalFinalizerObject
	{
		// Token: 0x06005178 RID: 20856 RVA: 0x0011EF2E File Offset: 0x0011D12E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalFinalizerObject()
		{
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x0011EF38 File Offset: 0x0011D138
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalFinalizerObject()
		{
		}
	}
}
