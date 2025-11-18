using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000786 RID: 1926
	internal sealed class BinaryCrossAppDomainAssembly : IStreamable
	{
		// Token: 0x060053D9 RID: 21465 RVA: 0x00126E5C File Offset: 0x0012505C
		internal BinaryCrossAppDomainAssembly()
		{
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x00126E64 File Offset: 0x00125064
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(20);
			sout.WriteInt32(this.assemId);
			sout.WriteInt32(this.assemblyIndex);
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x00126E86 File Offset: 0x00125086
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyIndex = input.ReadInt32();
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x00126EA0 File Offset: 0x001250A0
		public void Dump()
		{
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x00126EA2 File Offset: 0x001250A2
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025CB RID: 9675
		internal int assemId;

		// Token: 0x040025CC RID: 9676
		internal int assemblyIndex;
	}
}
