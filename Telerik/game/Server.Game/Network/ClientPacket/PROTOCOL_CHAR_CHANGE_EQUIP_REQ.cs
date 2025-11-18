using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CHAR_CHANGE_EQUIP_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		private bool bool_0;

		private bool bool_1;

		private bool bool_2;

		private bool bool_3;

		private bool bool_4;

		private readonly int[] int_2 = new int[2];

		private readonly int[] int_3 = new int[14];

		private readonly SortedList<int, int> sortedList_0 = new SortedList<int, int>();

		private readonly SortedList<int, int> sortedList_1 = new SortedList<int, int>();

		private readonly SortedList<int, int> sortedList_2 = new SortedList<int, int>();

		public PROTOCOL_CHAR_CHANGE_EQUIP_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
			base.ReadUD();
			this.bool_0 = base.ReadC() == 1;
			byte num = base.ReadC();
			for (byte i = 0; i < num; i = (byte)(i + 1))
			{
				int ınt32 = base.ReadD();
				this.sortedList_0.Add((int)i, ınt32);
			}
			this.bool_1 = base.ReadC() == 1;
			base.ReadC();
			byte num1 = base.ReadC();
			for (byte j = 0; j < num1; j = (byte)(j + 1))
			{
				int ınt321 = base.ReadD();
				this.sortedList_1.Add((int)j, ınt321);
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
			byte num2 = base.ReadC();
			for (byte k = 0; k < num2; k = (byte)(k + 1))
			{
				int ınt322 = base.ReadD();
				base.ReadUD();
				this.sortedList_2.Add((int)k, ınt322);
			}
			this.bool_4 = base.ReadC() == 1;
			base.ReadC();
			this.int_2[0] = base.ReadC();
			this.int_2[1] = base.ReadC();
		}

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
					this.Client.SendPacket(new PROTOCOL_CHAR_CHANGE_EQUIP_ACK(0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}