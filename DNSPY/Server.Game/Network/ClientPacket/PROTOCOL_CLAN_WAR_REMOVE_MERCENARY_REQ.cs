using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000187 RID: 391
	public class PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_REQ : GameClientPacket
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001F9C4 File Offset: 0x0001DBC4
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_REQ()
		{
		}
	}
}
