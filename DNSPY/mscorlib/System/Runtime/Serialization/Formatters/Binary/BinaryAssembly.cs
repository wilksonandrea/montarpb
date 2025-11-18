using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000785 RID: 1925
	internal sealed class BinaryAssembly : IStreamable
	{
		// Token: 0x060053D3 RID: 21459 RVA: 0x00126DF9 File Offset: 0x00124FF9
		internal BinaryAssembly()
		{
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x00126E01 File Offset: 0x00125001
		internal void Set(int assemId, string assemblyString)
		{
			this.assemId = assemId;
			this.assemblyString = assemblyString;
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x00126E11 File Offset: 0x00125011
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(12);
			sout.WriteInt32(this.assemId);
			sout.WriteString(this.assemblyString);
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x00126E33 File Offset: 0x00125033
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyString = input.ReadString();
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x00126E4D File Offset: 0x0012504D
		public void Dump()
		{
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x00126E4F File Offset: 0x0012504F
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025C9 RID: 9673
		internal int assemId;

		// Token: 0x040025CA RID: 9674
		internal string assemblyString;
	}
}
