using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000791 RID: 1937
	internal sealed class MemberPrimitiveUnTyped : IStreamable
	{
		// Token: 0x0600541A RID: 21530 RVA: 0x001283F3 File Offset: 0x001265F3
		internal MemberPrimitiveUnTyped()
		{
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x001283FB File Offset: 0x001265FB
		internal void Set(InternalPrimitiveTypeE typeInformation, object value)
		{
			this.typeInformation = typeInformation;
			this.value = value;
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x0012840B File Offset: 0x0012660B
		internal void Set(InternalPrimitiveTypeE typeInformation)
		{
			this.typeInformation = typeInformation;
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x00128414 File Offset: 0x00126614
		public void Write(__BinaryWriter sout)
		{
			sout.WriteValue(this.typeInformation, this.value);
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x00128428 File Offset: 0x00126628
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.value = input.ReadValue(this.typeInformation);
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x0012843C File Offset: 0x0012663C
		public void Dump()
		{
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x00128440 File Offset: 0x00126640
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				string text = Converter.ToComType(this.typeInformation);
			}
		}

		// Token: 0x04002607 RID: 9735
		internal InternalPrimitiveTypeE typeInformation;

		// Token: 0x04002608 RID: 9736
		internal object value;
	}
}
