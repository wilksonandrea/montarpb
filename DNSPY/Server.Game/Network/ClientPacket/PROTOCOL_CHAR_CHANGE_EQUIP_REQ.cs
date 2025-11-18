using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000178 RID: 376
	public class PROTOCOL_CHAR_CHANGE_EQUIP_REQ : GameClientPacket
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0001E0A0 File Offset: 0x0001C2A0
		public override void Read()
		{
			this.int_0 = base.ReadD();
			base.ReadUD();
			this.bool_0 = base.ReadC() == 1;
			byte b = base.ReadC();
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				int num = base.ReadD();
				this.sortedList_0.Add((int)b2, num);
			}
			this.bool_1 = base.ReadC() == 1;
			base.ReadC();
			byte b3 = base.ReadC();
			for (byte b4 = 0; b4 < b3; b4 += 1)
			{
				int num2 = base.ReadD();
				this.sortedList_1.Add((int)b4, num2);
			}
			this.bool_2 = base.ReadC() == 1;
			base.ReadC();
			base.ReadC();
			this.int_3[0] = base.ReadD();
			base.ReadUD();
			this.int_3[1] = base.ReadD();
			base.ReadUD();
			this.int_3[2] = base.ReadD();
			base.ReadUD();
			this.int_3[3] = base.ReadD();
			base.ReadUD();
			this.int_3[4] = base.ReadD();
			base.ReadUD();
			this.int_1 = base.ReadD();
			base.ReadUD();
			this.int_3[5] = base.ReadD();
			base.ReadUD();
			this.int_3[6] = base.ReadD();
			base.ReadUD();
			this.int_3[7] = base.ReadD();
			base.ReadUD();
			this.int_3[8] = base.ReadD();
			base.ReadUD();
			this.int_3[9] = base.ReadD();
			base.ReadUD();
			this.int_3[10] = base.ReadD();
			base.ReadUD();
			this.int_3[11] = base.ReadD();
			base.ReadUD();
			this.int_3[12] = base.ReadD();
			base.ReadUD();
			this.int_3[13] = base.ReadD();
			base.ReadUD();
			this.bool_3 = base.ReadC() == 1;
			byte b5 = base.ReadC();
			for (byte b6 = 0; b6 < b5; b6 += 1)
			{
				int num3 = base.ReadD();
				base.ReadUD();
				this.sortedList_2.Add((int)b6, num3);
			}
			this.bool_4 = base.ReadC() == 1;
			base.ReadC();
			this.int_2[0] = (int)base.ReadC();
			this.int_2[1] = (int)base.ReadC();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001E310 File Offset: 0x0001C510
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Character.Characters.Count > 0)
					{
						if (this.bool_0)
						{
							AllUtils.ValidateAccesoryEquipment(player, this.int_0);
						}
						if (this.bool_1)
						{
							AllUtils.ValidateDisabledCoupon(player, this.sortedList_0);
						}
						if (this.bool_2)
						{
							AllUtils.ValidateEnabledCoupon(player, this.sortedList_1);
						}
						if (this.bool_3)
						{
							AllUtils.ValidateCharacterEquipment(player, player.Equipment, this.int_3, this.int_1, this.int_2);
						}
						if (this.bool_4)
						{
							AllUtils.ValidateItemEquipment(player, this.sortedList_2);
						}
						AllUtils.ValidateCharacterSlot(player, player.Equipment, this.int_2);
					}
					RoomModel room = player.Room;
					if (room != null)
					{
						AllUtils.UpdateSlotEquips(player, room);
					}
					this.Client.SendPacket(new PROTOCOL_CHAR_CHANGE_EQUIP_ACK(0U));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001E414 File Offset: 0x0001C614
		public PROTOCOL_CHAR_CHANGE_EQUIP_REQ()
		{
		}

		// Token: 0x040002B0 RID: 688
		private int int_0;

		// Token: 0x040002B1 RID: 689
		private int int_1;

		// Token: 0x040002B2 RID: 690
		private bool bool_0;

		// Token: 0x040002B3 RID: 691
		private bool bool_1;

		// Token: 0x040002B4 RID: 692
		private bool bool_2;

		// Token: 0x040002B5 RID: 693
		private bool bool_3;

		// Token: 0x040002B6 RID: 694
		private bool bool_4;

		// Token: 0x040002B7 RID: 695
		private readonly int[] int_2 = new int[2];

		// Token: 0x040002B8 RID: 696
		private readonly int[] int_3 = new int[14];

		// Token: 0x040002B9 RID: 697
		private readonly SortedList<int, int> sortedList_0 = new SortedList<int, int>();

		// Token: 0x040002BA RID: 698
		private readonly SortedList<int, int> sortedList_1 = new SortedList<int, int>();

		// Token: 0x040002BB RID: 699
		private readonly SortedList<int, int> sortedList_2 = new SortedList<int, int>();
	}
}
