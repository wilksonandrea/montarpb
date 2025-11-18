using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Server.Game.Data.Commands
{
	public class MessageCommand : ICommand
	{
		public string Args
		{
			get
			{
				return "%options% %text%";
			}
		}

		public string Command
		{
			get
			{
				return "sendmsg";
			}
		}

		public string Description
		{
			get
			{
				return "Send messages";
			}
		}

		public string Permission
		{
			get
			{
				return "moderatorcommand";
			}
		}

		public MessageCommand()
		{
		}

		public string Execute(string Command, string[] Args, Account Player)
		{
			string lower = Args[0].ToLower();
			string str = string.Join(" ", Args, 1, (int)Args.Length - 1);
			if (lower.Equals("room"))
			{
				RoomModel room = Player.Room;
				if (room == null)
				{
					return "A room is required to execute the command.";
				}
				using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK pROTOCOLSERVERMESSAGEANNOUNCEACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str))
				{
					room.SendPacketToPlayers(pROTOCOLSERVERMESSAGEANNOUNCEACK);
				}
				return string.Format("Message sended to current Room Id: {0}", room.RoomId + 1);
			}
			if (lower.Equals("channel"))
			{
				ChannelModel channel = Player.GetChannel();
				if (channel == null)
				{
					return "Please run the command in the lobby.";
				}
				using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK pROTOCOLSERVERMESSAGEANNOUNCEACK1 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str))
				{
					channel.SendPacketToWaitPlayers(pROTOCOLSERVERMESSAGEANNOUNCEACK1);
				}
				return string.Format("Message sended to current Channel Id: {0}", channel.Id + 1);
			}
			if (!lower.Equals("player"))
			{
				return string.Concat("Command ", ComDiv.ToTitleCase(lower), " was not founded!");
			}
			int ınt32 = 0;
			using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK pROTOCOLSERVERMESSAGEANNOUNCEACK2 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str))
			{
				SortedList<long, Account> accounts = AccountManager.Accounts;
				if (accounts.Count == 0)
				{
					ınt32 = 0;
				}
				byte[] completeBytes = pROTOCOLSERVERMESSAGEANNOUNCEACK2.GetCompleteBytes("Player.MessageCommands");
				foreach (Account value in accounts.Values)
				{
					value.SendCompletePacket(completeBytes, pROTOCOLSERVERMESSAGEANNOUNCEACK2.GetType().Name);
					ınt32++;
				}
			}
			return string.Format("Message sended to {0} total of player(s)", ınt32);
		}
	}
}