using System;
using Plugin.Core;
using Plugin.Core.Enums;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000145 RID: 325
	public class PROTOCOL_BASE_UNKNOWN_3635_REQ : GameClientPacket
	{
		// Token: 0x0600032D RID: 813 RVA: 0x00019168 File Offset: 0x00017368
		public override void Read()
		{
			this.byte_0 = base.ReadC();
			this.string_0 = base.ReadU(66);
			base.ReadD();
			base.ReadH();
			this.byte_1 = base.ReadC();
			base.ReadH();
			base.ReadB(16);
			base.ReadB(12);
			this.short_0 = base.ReadH();
			this.byte_2 = base.ReadC();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000191DC File Offset: 0x000173DC
		public override void Run()
		{
			try
			{
				CLogger.Print(string.Format("{0}; Unk1: {1}; Nickname: {2}; Unk2: {3}; Unk3: {4}; Unk4: {5}", new object[]
				{
					base.GetType().Name,
					this.byte_0,
					this.string_0,
					this.byte_1,
					this.short_0,
					this.byte_2
				}), LoggerType.Warning, null);
			}
			catch (Exception ex)
			{
				CLogger.Print(base.GetType().Name + " Error: " + ex.Message, LoggerType.Error, null);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_UNKNOWN_3635_REQ()
		{
		}

		// Token: 0x04000247 RID: 583
		private byte byte_0;

		// Token: 0x04000248 RID: 584
		private byte byte_1;

		// Token: 0x04000249 RID: 585
		private byte byte_2;

		// Token: 0x0400024A RID: 586
		private string string_0;

		// Token: 0x0400024B RID: 587
		private short short_0;
	}
}
