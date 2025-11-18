using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001CA RID: 458
	public class PROTOCOL_ROOM_GET_PLAYERINFO_REQ : GameClientPacket
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x00005797 File Offset: 0x00003997
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00025B24 File Offset: 0x00023D24
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					Account account;
					if (room != null && player.SlotId != this.int_0 && room.GetPlayerBySlot(this.int_0, out account))
					{
						uint num = 0U;
						int[] array = new int[2];
						SlotModel slot = room.GetSlot(account.SlotId);
						if (slot != null)
						{
							TeamEnum teamEnum = room.ValidateTeam(slot.Team, slot.CostumeTeam);
							if (teamEnum == TeamEnum.FR_TEAM)
							{
								array[0] = slot.Equipment.CharaRedId;
							}
							else if (teamEnum == TeamEnum.CT_TEAM)
							{
								array[0] = slot.Equipment.CharaBlueId;
							}
							array[1] = slot.Equipment.AccessoryId;
						}
						else
						{
							num = 134217728U;
						}
						this.Client.SendPacket(new PROTOCOL_ROOM_GET_PLAYERINFO_ACK(num, account, array));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_GET_PLAYERINFO_REQ()
		{
		}

		// Token: 0x04000376 RID: 886
		private int int_0;
	}
}
