using System;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands
{
	// Token: 0x02000217 RID: 535
	public class TeamCashCommand : ICommand
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x000063C3 File Offset: 0x000045C3
		public string Command
		{
			get
			{
				return "teamcash";
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x000063CA File Offset: 0x000045CA
		public string Description
		{
			get
			{
				return "Send cash to a team";
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x000063D1 File Offset: 0x000045D1
		public string Permission
		{
			get
			{
				return "gamemastercommand";
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x000063D8 File Offset: 0x000045D8
		public string Args
		{
			get
			{
				return "%team% %cash% (Team = FR/CT)";
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00038F4C File Offset: 0x0003714C
		public string Execute(string Command, string[] Args, Account Player)
		{
			if (Player.Room == null)
			{
				return "Please execute the command in a room";
			}
			if (Args.Length != 2)
			{
				return "Please use correct command usage, :teamcash %team% %cash%";
			}
			int num;
			if (!int.TryParse(Args[1], out num))
			{
				return "Please use correct number as value";
			}
			string text = Args[0].ToLower();
			if (text != "red" && text != "blue")
			{
				return "Please use correct team, 'red' or 'blue'";
			}
			int num2 = ((!(text == "red")) ? 1 : 0);
			RoomModel room = Player.Room;
			for (int i = 0; i < 18; i++)
			{
				if (i % 2 == num2)
				{
					SlotModel slot = room.GetSlot(i);
					if (slot.PlayerId > 0L)
					{
						Account playerBySlot = room.GetPlayerBySlot(slot);
						if (playerBySlot != null && DaoManagerSQL.UpdateAccountCash(playerBySlot.PlayerId, playerBySlot.Cash + num))
						{
							playerBySlot.Cash += num;
							playerBySlot.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0U, playerBySlot));
							SendItemInfo.LoadGoldCash(playerBySlot);
						}
					}
				}
			}
			return string.Format("'{0}' cash sended to team {1}", num, text);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000025DF File Offset: 0x000007DF
		public TeamCashCommand()
		{
		}
	}
}
