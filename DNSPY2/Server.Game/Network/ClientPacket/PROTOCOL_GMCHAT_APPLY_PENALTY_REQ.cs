using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B0 RID: 432
	public class PROTOCOL_GMCHAT_APPLY_PENALTY_REQ : GameClientPacket
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x00022B6C File Offset: 0x00020D6C
		public override void Read()
		{
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
			this.string_1 = base.ReadU((int)(base.ReadC() * 2));
			this.byte_0 = base.ReadC();
			this.int_0 = base.ReadD();
			base.ReadC();
			this.long_0 = base.ReadQ();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00022BCC File Offset: 0x00020DCC
		public override void Run()
		{
			try
			{
				if (this.Client.Player != null)
				{
					if (AccountManager.GetAccount(this.long_0, 31) != null)
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(0U));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(2147483648U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_GMCHAT_APPLY_PENALTY_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_GMCHAT_APPLY_PENALTY_REQ()
		{
		}

		// Token: 0x04000320 RID: 800
		private long long_0;

		// Token: 0x04000321 RID: 801
		private string string_0;

		// Token: 0x04000322 RID: 802
		private string string_1;

		// Token: 0x04000323 RID: 803
		private int int_0;

		// Token: 0x04000324 RID: 804
		private byte byte_0;
	}
}
