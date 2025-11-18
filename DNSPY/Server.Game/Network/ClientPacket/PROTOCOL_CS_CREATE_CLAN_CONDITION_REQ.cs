using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200019A RID: 410
	public class PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ : GameClientPacket
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00020E7C File Offset: 0x0001F07C
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ()
		{
		}
	}
}
