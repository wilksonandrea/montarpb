using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_DEATH_REQ : GameClientPacket
	{
		private FragInfos fragInfos_0;

		private bool bool_0;

		public PROTOCOL_BATTLE_DEATH_REQ()
		{
		}

		public override void Read()
		{
			this.fragInfos_0 = new FragInfos()
			{
				KillingType = (CharaKillType)base.ReadC(),
				KillsCount = base.ReadC(),
				KillerSlot = base.ReadC(),
				WeaponId = base.ReadD(),
				X = base.ReadT(),
				Y = base.ReadT(),
				Z = base.ReadT(),
				Flag = base.ReadC(),
				Unk = base.ReadC()
			};
			for (int i = 0; i < this.fragInfos_0.KillsCount; i++)
			{
				FragModel fragModel = new FragModel()
				{
					VictimSlot = base.ReadC(),
					WeaponClass = base.ReadC(),
					HitspotInfo = base.ReadC(),
					KillFlag = (KillingMessage)base.ReadH(),
					Unk = base.ReadC(),
					X = base.ReadT(),
					Y = base.ReadT(),
					Z = base.ReadT(),
					AssistSlot = base.ReadC(),
					Unks = base.ReadB(8)
				};
				this.fragInfos_0.Frags.Add(fragModel);
				if (fragModel.VictimSlot == this.fragInfos_0.KillerSlot)
				{
					this.bool_0 = true;
				}
			}
		}

		public override void Run()
		{
			int ınt32;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && !room.RoundTime.IsTimer() && room.State >= RoomState.BATTLE)
					{
						bool flag = room.IsBotMode();
						SlotModel slot = room.GetSlot((int)this.fragInfos_0.KillerSlot);
						if (slot != null)
						{
							if (!flag)
							{
								if (slot.State >= SlotState.BATTLE)
								{
									if (slot.Id == player.SlotId)
									{
										goto Label2;
									}
									goto Label1;
								}
								else
								{
									goto Label1;
								}
							}
						Label2:
							RoomDeath.RegistryFragInfos(room, slot, out ınt32, flag, this.bool_0, this.fragInfos_0);
							if (!flag)
							{
								slot.Score += ınt32;
								AllUtils.CompleteMission(room, player, slot, this.fragInfos_0, MissionType.NA, 0);
								this.fragInfos_0.Score = ınt32;
							}
							else
							{
								SlotModel score = slot;
								score.Score = score.Score + slot.KillsOnLife + room.IngameAiLevel + ınt32;
								if (slot.Score > 65535)
								{
									slot.Score = 65535;
									AllUtils.ValidateBanPlayer(player, string.Format("AI Score Cheating! ({0})", slot.Score));
								}
								this.fragInfos_0.Score = slot.Score;
							}
							using (PROTOCOL_BATTLE_DEATH_ACK pROTOCOLBATTLEDEATHACK = new PROTOCOL_BATTLE_DEATH_ACK(room, this.fragInfos_0, slot))
							{
								room.SendPacketToPlayers(pROTOCOLBATTLEDEATHACK, SlotState.BATTLE, 0);
							}
							RoomDeath.EndBattleByDeath(room, slot, flag, this.bool_0, this.fragInfos_0);
							return;
						}
					Label1:
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_DEATH_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}