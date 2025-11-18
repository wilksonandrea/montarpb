using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_RESPAWN_REQ : GameClientPacket
	{
		private int[] int_0;

		private int int_1;

		public PROTOCOL_BATTLE_RESPAWN_REQ()
		{
		}

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
			this.int_1 = base.ReadH();
			this.int_0[15] = base.ReadD();
			base.ReadUD();
		}

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
								int ıdStatics = ComDiv.GetIdStatics(this.int_0[5], 1);
								int ınt32 = ComDiv.GetIdStatics(this.int_0[5], 2);
								int ıdStatics1 = ComDiv.GetIdStatics(this.int_0[5], 5);
								if (ıdStatics == 6)
								{
									if (ınt32 != 1)
									{
										if (ıdStatics1 == 632)
										{
											goto Label2;
										}
										if (ınt32 == 2 || ıdStatics1 == 664)
										{
											AllUtils.InsertItem(playerEquipment.CharaBlueId, slot);
											goto Label0;
										}
										else
										{
											goto Label0;
										}
									}
								Label2:
									AllUtils.InsertItem(playerEquipment.CharaRedId, slot);
								}
								else if (ıdStatics == 15)
								{
									AllUtils.InsertItem(playerEquipment.DinoItem, slot);
								}
							}
							using (PROTOCOL_BATTLE_RESPAWN_ACK pROTOCOLBATTLERESPAWNACK = new PROTOCOL_BATTLE_RESPAWN_ACK(room, slot))
							{
								room.SendPacketToPlayers(pROTOCOLBATTLERESPAWNACK, SlotState.BATTLE, 0);
							}
							if (!slot.FirstRespawn)
							{
								EquipmentSync.SendUDPPlayerSync(room, slot, player.Effects, 2);
							}
							else
							{
								slot.FirstRespawn = false;
								EquipmentSync.SendUDPPlayerSync(room, slot, player.Effects, 0);
							}
							if ((int)room.WeaponsFlag != this.int_1)
							{
								CLogger.Print(string.Format("Player '{0}' Weapon Flags Doesn't Match! (Room: {1}; Player: {2})", player.Nickname, (int)room.WeaponsFlag, this.int_1), LoggerType.Warning, null);
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_RESPAWN_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}