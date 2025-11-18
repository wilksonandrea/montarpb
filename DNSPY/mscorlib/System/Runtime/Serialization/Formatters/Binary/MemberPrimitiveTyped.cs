using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078D RID: 1933
	internal sealed class MemberPrimitiveTyped : IStreamable
	{
		// Token: 0x06005403 RID: 21507 RVA: 0x00127BA6 File Offset: 0x00125DA6
		internal MemberPrimitiveTyped()
		{
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x00127BAE File Offset: 0x00125DAE
		internal void Set(InternalPrimitiveTypeE primitiveTypeEnum, object value)
		{
			this.primitiveTypeEnum = primitiveTypeEnum;
			this.value = value;
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x00127BBE File Offset: 0x00125DBE
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(8);
			sout.WriteByte((byte)this.primitiveTypeEnum);
			sout.WriteValue(this.primitiveTypeEnum, this.value);
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x00127BE6 File Offset: 0x00125DE6
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.primitiveTypeEnum = (InternalPrimitiveTypeE)input.ReadByte();
			this.value = input.ReadValue(this.primitiveTypeEnum);
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x00127C06 File Offset: 0x00125E06
		public void Dump()
		{
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x00127C08 File Offset: 0x00125E08
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025ED RID: 9709
		internal InternalPrimitiveTypeE primitiveTypeEnum;

		// Token: 0x040025EE RID: 9710
		internal object value;
	}
}
