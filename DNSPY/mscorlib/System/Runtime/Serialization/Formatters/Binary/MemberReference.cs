using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000792 RID: 1938
	internal sealed class MemberReference : IStreamable
	{
		// Token: 0x06005421 RID: 21537 RVA: 0x00128465 File Offset: 0x00126665
		internal MemberReference()
		{
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x0012846D File Offset: 0x0012666D
		internal void Set(int idRef)
		{
			this.idRef = idRef;
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x00128476 File Offset: 0x00126676
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(9);
			sout.WriteInt32(this.idRef);
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x0012848C File Offset: 0x0012668C
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.idRef = input.ReadInt32();
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x0012849A File Offset: 0x0012669A
		public void Dump()
		{
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x0012849C File Offset: 0x0012669C
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002609 RID: 9737
		internal int idRef;
	}
}
