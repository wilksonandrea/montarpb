using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000177 RID: 375
	public class PROTOCOL_BATTLE_USER_SOPETYPE_REQ : GameClientPacket
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0000525D File Offset: 0x0000345D
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001E014 File Offset: 0x0001C214
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null)
					{
						player.Sight = this.int_0;
						using (new PROTOCOL_BATTLE_USER_SOPETYPE_ACK(player))
						{
							room.SendPacketToPlayers(new PROTOCOL_BATTLE_USER_SOPETYPE_ACK(player));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_USER_SOPETYPE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_USER_SOPETYPE_REQ()
		{
		}

		// Token: 0x040002AF RID: 687
		private int int_0;
	}
}
