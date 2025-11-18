using System.Collections.Generic;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands;

public class MessageCommand : ICommand
{
	public string Command => "sendmsg";

	public string Description => "Send messages";

	public string Permission => "moderatorcommand";

	public string Args => "%options% %text%";

	public string Execute(string Command, string[] Args, Account Player)
	{
		string text = Args[0].ToLower();
		string string_ = string.Join(" ", Args, 1, Args.Length - 1);
		if (text.Equals("room"))
		{
			RoomModel room = Player.Room;
			if (room == null)
			{
				return "A room is required to execute the command.";
			}
			using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK packet = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string_))
			{
				room.SendPacketToPlayers(packet);
			}
			return $"Message sended to current Room Id: {room.RoomId + 1}";
		}
		if (text.Equals("channel"))
		{
			ChannelModel channel = Player.GetChannel();
			if (channel == null)
			{
				return "Please run the command in the lobby.";
			}
			using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK packet2 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string_))
			{
				channel.SendPacketToWaitPlayers(packet2);
			}
			return $"Message sended to current Channel Id: {channel.Id + 1}";
		}
		if (text.Equals("player"))
		{
			int num = 0;
			using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK pROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string_))
			{
				SortedList<long, Account> accounts = AccountManager.Accounts;
				if (accounts.Count == 0)
				{
					num = 0;
				}
				byte[] completeBytes = pROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK.GetCompleteBytes("Player.MessageCommands");
				foreach (Account value in accounts.Values)
				{
					value.SendCompletePacket(completeBytes, pROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK.GetType().Name);
					num++;
				}
			}
			return $"Message sended to {num} total of player(s)";
		}
		return "Command " + ComDiv.ToTitleCase(text) + " was not founded!";
	}
}
