using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000149 RID: 329
	public class PROTOCOL_GM_KICK_COMMAND_REQ : GameClientPacket
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00005000 File Offset: 0x00003200
		public override void Read()
		{
			this.byte_0 = base.ReadC();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00019408 File Offset: 0x00017608
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
						Account playerBySlot = room.GetPlayerBySlot((int)this.byte_0);
						if (playerBySlot != null && !playerBySlot.IsGM())
						{
							room.RemovePlayer(playerBySlot, true, 0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(base.GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_GM_KICK_COMMAND_REQ()
		{
		}

		// Token: 0x04000254 RID: 596
		private byte byte_0;
	}
}
