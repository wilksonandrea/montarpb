using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000170 RID: 368
	public class PROTOCOL_BATTLE_RESPAWN_REQ : GameClientPacket
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
		public override void Read()
		{
			this.int_0 = new int[16];
			this.int_0[0] = base.ReadD();
			base.ReadUD();
			this.int_0[1] = base.ReadD();
			base.ReadUD();
			this.int_0[2] = base.ReadD();
			base.ReadUD();
			this.int_0[3] = base.ReadD();
			base.ReadUD();
			this.int_0[4] = base.ReadD();
			base.ReadUD();
			this.int_0[5] = base.ReadD();
			base.ReadUD();
			this.int_0[6] = base.ReadD();
			base.ReadUD();
			this.int_0[7] = base.ReadD();
			base.ReadUD();
			this.int_0[8] = base.ReadD();
			base.ReadUD();
			this.int_0[9] = base.ReadD();
			base.ReadUD();
			this.int_0[10] = base.ReadD();
			base.ReadUD();
			this.int_0[11] = base.ReadD();
			base.ReadUD();
			this.int_0[12] = base.ReadD();
			base.ReadUD();
			this.int_0[13] = base.ReadD();
			base.ReadUD();
			this.int_0[14] = base.ReadD();
			base.ReadUD();
			this.int_1 = (int)base.ReadH();
			this.int_0[15] = base.ReadD();
			base.ReadUD();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001CE78 File Offset: 0x0001B078
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.State == RoomState.BATTLE)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null && slot.State == SlotState.BATTLE)
						{
							if (slot.DeathState.HasFlag(DeadEnum.Dead) || slot.DeathState.HasFlag(DeadEnum.UseChat))
							{
								slot.DeathState = DeadEnum.Alive;
							}
							PlayerEquipment playerEquipment = AllUtils.ValidateRespawnEQ(slot, this.int_0);
							if (playerEquipment != null)
							{
								ComDiv.CheckEquipedItems(playerEquipment, player.Inventory.Items, true);
								AllUtils.ClassicModeCheck(room, playerEquipment);
								slot.Equipment = playerEquipment;
								if ((this.int_1 & 8) > 0)
								{
									AllUtils.InsertItem(playerEquipment.WeaponPrimary, slot);
								}
								if ((this.int_1 & 4) > 0)
								{
									AllUtils.InsertItem(playerEquipment.WeaponSecondary, slot);
								}
								if ((this.int_1 & 2) > 0)
								{
									AllUtils.InsertItem(playerEquipment.WeaponMelee, slot);
								}
								if ((this.int_1 & 1) > 0)
								{
									AllUtils.InsertItem(playerEquipment.WeaponExplosive, slot);
								}
								AllUtils.InsertItem(playerEquipment.WeaponSpecial, slot);
								AllUtils.InsertItem(playerEquipment.PartHead, slot);
								AllUtils.InsertItem(playerEquipment.PartFace, slot);
								AllUtils.InsertItem(playerEquipment.BeretItem, slot);
								AllUtils.InsertItem(playerEquipment.AccessoryId, slot);
								int idStatics = ComDiv.GetIdStatics(this.int_0[5], 1);
								int idStatics2 = ComDiv.GetIdStatics(this.int_0[5], 2);
								int idStatics3 = ComDiv.GetIdStatics(this.int_0[5], 5);
								if (idStatics == 6)
								{
									if (idStatics2 != 1)
									{
										if (idStatics3 != 632)
										{
											if (idStatics2 == 2 || idStatics3 == 664)
											{
												AllUtils.InsertItem(playerEquipment.CharaBlueId, slot);
												goto IL_1D1;
											}
											goto IL_1D1;
										}
									}
									AllUtils.InsertItem(playerEquipment.CharaRedId, slot);
								}
								else if (idStatics == 15)
								{
									AllUtils.InsertItem(playerEquipment.DinoItem, slot);
								}
							}
							IL_1D1:
							using (PROTOCOL_BATTLE_RESPAWN_ACK protocol_BATTLE_RESPAWN_ACK = new PROTOCOL_BATTLE_RESPAWN_ACK(room, slot))
							{
								room.SendPacketToPlayers(protocol_BATTLE_RESPAWN_ACK, SlotState.BATTLE, 0);
							}
							if (slot.FirstRespawn)
							{
								slot.FirstRespawn = false;
								EquipmentSync.SendUDPPlayerSync(room, slot, player.Effects, 0);
							}
							else
							{
								EquipmentSync.SendUDPPlayerSync(room, slot, player.Effects, 2);
							}
							if (room.WeaponsFlag != (RoomWeaponsFlag)this.int_1)
							{
								CLogger.Print(string.Format("Player '{0}' Weapon Flags Doesn't Match! (Room: {1}; Player: {2})", player.Nickname, (int)room.WeaponsFlag, this.int_1), LoggerType.Warning, null);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_RESPAWN_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_RESPAWN_REQ()
		{
		}

		// Token: 0x040002A1 RID: 673
		private int[] int_0;

		// Token: 0x040002A2 RID: 674
		private int int_1;
	}
}
