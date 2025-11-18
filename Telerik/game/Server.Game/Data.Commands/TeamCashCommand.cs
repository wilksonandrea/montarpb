using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Commands
{
	public class TeamCashCommand : ICommand
	{
		public string Args
		{
			get
			{
				return "%team% %cash% (Team = FR/CT)";
			}
		}

		public string Command
		{
			get
			{
				return "teamcash";
			}
		}

		public string Description
		{
			get
			{
				return "Send cash to a team";
			}
		}

		public string Permission
		{
			get
			{
				return "gamemastercommand";
			}
		}

		public TeamCashCommand()
		{
		}

		public string Execute(string Command, string[] Args, Account Player)
		{
			int ınt32;
			if (Player.Room == null)
			{
				return "Please execute the command in a room";
			}
			if ((int)Args.Length != 2)
			{
				return "Please use correct command usage, :teamcash %team% %cash%";
			}
			if (!int.TryParse(Args[1], out ınt32))
			{
				return "Please use correct number as value";
			}
			string lower = Args[0].ToLower();
			if (lower != "red" && lower != "blue")
			{
				return "Please use correct team, 'red' or 'blue'";
			}
			int ınt321 = !(lower == "red");
			RoomModel room = Player.Room;
			for (int i = 0; i < 18; i++)
			{
				if (i % 2 == ınt321)
				{
					SlotModel slot = room.GetSlot(i);
					if (slot.PlayerId > 0L)
					{
						Account playerBySlot = room.GetPlayerBySlot(slot);
						if (playerBySlot != null && DaoManagerSQL.UpdateAccountCash(playerBySlot.PlayerId, playerBySlot.Cash + ınt32))
						{
							playerBySlot.Cash += ınt32;
							playerBySlot.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, playerBySlot));
							SendItemInfo.LoadGoldCash(playerBySlot);
						}
					}
				}
			}
			return string.Format("'{0}' cash sended to team {1}", ınt32, lower);
		}
	}
}