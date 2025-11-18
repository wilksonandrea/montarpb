using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001C5 RID: 453
	public class PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ : GameClientPacket
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x00024D5C File Offset: 0x00022F5C
		public override void Read()
		{
			base.ReadD();
			this.string_0 = base.ReadU(46);
			this.mapIdEnum_0 = (MapIdEnum)base.ReadC();
			this.mapRules_0 = (MapRules)base.ReadC();
			this.stageOptions_0 = (StageOptions)base.ReadC();
			this.roomCondition_0 = (RoomCondition)base.ReadC();
			this.roomState_0 = (RoomState)base.ReadC();
			this.int_4 = (int)base.ReadC();
			this.int_1 = (int)base.ReadC();
			this.int_2 = (int)base.ReadC();
			this.roomWeaponsFlag_0 = (RoomWeaponsFlag)base.ReadH();
			this.roomStageFlag_0 = (RoomStageFlag)base.ReadD();
			base.ReadH();
			this.int_0 = base.ReadD();
			base.ReadH();
			this.string_1 = base.ReadU(66);
			this.int_3 = base.ReadD();
			this.byte_2 = base.ReadC();
			this.byte_3 = base.ReadC();
			this.teamBalance_0 = (TeamBalance)base.ReadH();
			this.byte_0 = base.ReadB(24);
			this.byte_6 = base.ReadC();
			this.byte_1 = base.ReadB(4);
			this.byte_7 = base.ReadC();
			base.ReadH();
			this.byte_4 = base.ReadC();
			this.byte_5 = base.ReadC();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00024EA0 File Offset: 0x000230A0
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.LeaderSlot == player.SlotId)
					{
						bool flag = !room.Name.Equals(this.string_0);
						bool flag2 = room.Rule != this.mapRules_0 || room.Stage != this.stageOptions_0 || room.RoomType != this.roomCondition_0;
						room.Name = this.string_0;
						room.MapId = this.mapIdEnum_0;
						room.Rule = this.mapRules_0;
						room.Stage = this.stageOptions_0;
						room.RoomType = this.roomCondition_0;
						room.Ping = this.int_2;
						room.Flag = this.roomStageFlag_0;
						room.NewInt = this.int_0;
						room.KillTime = this.int_3;
						room.Limit = this.byte_2;
						room.WatchRuleFlag = ((room.RoomType == RoomCondition.Ace) ? 142 : this.byte_3);
						room.BalanceType = ((room.RoomType == RoomCondition.Ace) ? TeamBalance.None : this.teamBalance_0);
						room.BalanceType = this.teamBalance_0;
						room.RandomMaps = this.byte_0;
						room.CountdownIG = this.byte_6;
						room.LeaderAddr = this.byte_1;
						room.KillCam = this.byte_7;
						room.AiCount = this.byte_4;
						room.AiLevel = this.byte_5;
						room.SetSlotCount(this.int_1, false, true);
						room.CountPlayers = this.int_4;
						if (this.roomState_0 < RoomState.READY || this.string_1.Equals("") || !this.string_1.Equals(player.Nickname) || flag || flag2 || this.roomWeaponsFlag_0 != room.WeaponsFlag || this.int_1 != room.CountMaxSlots)
						{
							room.State = ((this.roomState_0 < RoomState.READY) ? RoomState.READY : this.roomState_0);
							room.LeaderName = ((this.string_1.Equals("") || !this.string_1.Equals(player.Nickname)) ? player.Nickname : this.string_1);
							room.WeaponsFlag = this.roomWeaponsFlag_0;
							room.CountMaxSlots = this.int_1;
							room.CountdownIG = 0;
							if (room.ResetReadyPlayers() > 0)
							{
								room.UpdateSlotsInfo();
							}
						}
						room.UpdateRoomInfo();
						using (PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK protocol_ROOM_CHANGE_ROOM_OPTIONINFO_ACK = new PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK(room))
						{
							room.SendPacketToPlayers(protocol_ROOM_CHANGE_ROOM_OPTIONINFO_ACK);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_CHANGE_ROOMINFO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ()
		{
		}

		// Token: 0x04000341 RID: 833
		private string string_0;

		// Token: 0x04000342 RID: 834
		private string string_1;

		// Token: 0x04000343 RID: 835
		private MapIdEnum mapIdEnum_0;

		// Token: 0x04000344 RID: 836
		private MapRules mapRules_0;

		// Token: 0x04000345 RID: 837
		private StageOptions stageOptions_0;

		// Token: 0x04000346 RID: 838
		private TeamBalance teamBalance_0;

		// Token: 0x04000347 RID: 839
		private byte[] byte_0;

		// Token: 0x04000348 RID: 840
		private byte[] byte_1;

		// Token: 0x04000349 RID: 841
		private int int_0;

		// Token: 0x0400034A RID: 842
		private int int_1;

		// Token: 0x0400034B RID: 843
		private int int_2;

		// Token: 0x0400034C RID: 844
		private int int_3;

		// Token: 0x0400034D RID: 845
		private int int_4;

		// Token: 0x0400034E RID: 846
		private byte byte_2;

		// Token: 0x0400034F RID: 847
		private byte byte_3;

		// Token: 0x04000350 RID: 848
		private byte byte_4;

		// Token: 0x04000351 RID: 849
		private byte byte_5;

		// Token: 0x04000352 RID: 850
		private byte byte_6;

		// Token: 0x04000353 RID: 851
		private byte byte_7;

		// Token: 0x04000354 RID: 852
		private RoomCondition roomCondition_0;

		// Token: 0x04000355 RID: 853
		private RoomState roomState_0;

		// Token: 0x04000356 RID: 854
		private RoomWeaponsFlag roomWeaponsFlag_0;

		// Token: 0x04000357 RID: 855
		private RoomStageFlag roomStageFlag_0;
	}
}
