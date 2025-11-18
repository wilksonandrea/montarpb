using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000163 RID: 355
	public class PROTOCOL_BATTLE_DEATH_REQ : GameClientPacket
	{
		// Token: 0x0600038B RID: 907 RVA: 0x0001B5EC File Offset: 0x000197EC
		public override void Read()
		{
			this.fragInfos_0 = new FragInfos
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
			for (int i = 0; i < (int)this.fragInfos_0.KillsCount; i++)
			{
				FragModel fragModel = new FragModel
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

		// Token: 0x0600038C RID: 908 RVA: 0x0001B738 File Offset: 0x00019938
		public override void Run()
		{
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
								if (slot.State < SlotState.BATTLE)
								{
									goto IL_165;
								}
								if (slot.Id != player.SlotId)
								{
									goto IL_165;
								}
							}
							int num;
							RoomDeath.RegistryFragInfos(room, slot, out num, flag, this.bool_0, this.fragInfos_0);
							if (flag)
							{
								slot.Score += slot.KillsOnLife + (int)room.IngameAiLevel + num;
								if (slot.Score > 65535)
								{
									slot.Score = 65535;
									AllUtils.ValidateBanPlayer(player, string.Format("AI Score Cheating! ({0})", slot.Score));
								}
								this.fragInfos_0.Score = slot.Score;
							}
							else
							{
								slot.Score += num;
								AllUtils.CompleteMission(room, player, slot, this.fragInfos_0, MissionType.NA, 0);
								this.fragInfos_0.Score = num;
							}
							using (PROTOCOL_BATTLE_DEATH_ACK protocol_BATTLE_DEATH_ACK = new PROTOCOL_BATTLE_DEATH_ACK(room, this.fragInfos_0, slot))
							{
								room.SendPacketToPlayers(protocol_BATTLE_DEATH_ACK, SlotState.BATTLE, 0);
							}
							RoomDeath.EndBattleByDeath(room, slot, flag, this.bool_0, this.fragInfos_0);
						}
						IL_165:;
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_DEATH_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_DEATH_REQ()
		{
		}

		// Token: 0x04000286 RID: 646
		private FragInfos fragInfos_0;

		// Token: 0x04000287 RID: 647
		private bool bool_0;
	}
}
