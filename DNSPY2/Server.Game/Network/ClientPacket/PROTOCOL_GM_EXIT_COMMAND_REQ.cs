using System;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000148 RID: 328
	public class PROTOCOL_GM_EXIT_COMMAND_REQ : GameClientPacket
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00004FF2 File Offset: 0x000031F2
		public override void Read()
		{
			this.byte_0 = base.ReadC();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Run()
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_GM_EXIT_COMMAND_REQ()
		{
		}

		// Token: 0x04000253 RID: 595
		private byte byte_0;
	}
}
