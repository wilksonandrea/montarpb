using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000188 RID: 392
	public class PROTOCOL_CLAN_WAR_RESULT_REQ : GameClientPacket
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00005377 File Offset: 0x00003577
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001FA10 File Offset: 0x0001DC10
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_CLAN_WAR_RESULT_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CLAN_WAR_RESULT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_RESULT_REQ()
		{
		}

		// Token: 0x040002DE RID: 734
		private int int_0;
	}
}
