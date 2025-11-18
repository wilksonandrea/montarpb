using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078E RID: 1934
	internal sealed class BinaryObjectWithMap : IStreamable
	{
		// Token: 0x06005409 RID: 21513 RVA: 0x00127C15 File Offset: 0x00125E15
		internal BinaryObjectWithMap()
		{
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00127C1D File Offset: 0x00125E1D
		internal BinaryObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x00127C2C File Offset: 0x00125E2C
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, int assemId)
		{
			this.objectId = objectId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMap;
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x00127C68 File Offset: 0x00125E68
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.name);
			sout.WriteInt32(this.numMembers);
			for (int i = 0; i < this.numMembers; i++)
			{
				sout.WriteString(this.memberNames[i]);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x00127CDC File Offset: 0x00125EDC
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00127D52 File Offset: 0x00125F52
		public void Dump()
		{
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x00127D54 File Offset: 0x00125F54
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				for (int i = 0; i < this.numMembers; i++)
				{
				}
				BinaryHeaderEnum binaryHeaderEnum = this.binaryHeaderEnum;
			}
		}

		// Token: 0x040025EF RID: 9711
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x040025F0 RID: 9712
		internal int objectId;

		// Token: 0x040025F1 RID: 9713
		internal string name;

		// Token: 0x040025F2 RID: 9714
		internal int numMembers;

		// Token: 0x040025F3 RID: 9715
		internal string[] memberNames;

		// Token: 0x040025F4 RID: 9716
		internal int assemId;
	}
}
