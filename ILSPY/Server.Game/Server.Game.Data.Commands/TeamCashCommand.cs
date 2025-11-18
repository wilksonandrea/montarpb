using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands;

public class TeamCashCommand : ICommand
{
	public string Command => "teamcash";

	public string Description => "Send cash to a team";

	public string Permission => "gamemastercommand";

	public string Args => "%team% %cash% (Team = FR/CT)";

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
		if (!int.TryParse(Args[1], out var result))
		{
			return "Please use correct number as value";
		}
		string text = Args[0].ToLower();
		if (text != "red" && text != "blue")
		{
			return "Please use correct team, 'red' or 'blue'";
		}
		int num = ((!(text == "red")) ? 1 : 0);
		RoomModel room = Player.Room;
		for (int i = 0; i < 18; i++)
		{
			if (i % 2 != num)
			{
				continue;
			}
			SlotModel slot = room.GetSlot(i);
			if (slot.PlayerId > 0L)
			{
				Account playerBySlot = room.GetPlayerBySlot(slot);
				if (playerBySlot != null && DaoManagerSQL.UpdateAccountCash(playerBySlot.PlayerId, playerBySlot.Cash + result))
				{
					playerBySlot.Cash += result;
					playerBySlot.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0u, playerBySlot));
					SendItemInfo.LoadGoldCash(playerBySlot);
				}
			}
		}
		return $"'{result}' cash sended to team {text}";
	}
}
