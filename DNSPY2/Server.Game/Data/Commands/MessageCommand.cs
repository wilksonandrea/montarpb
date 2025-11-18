using System;
using System.Collections.Generic;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands
{
	// Token: 0x02000213 RID: 531
	public class MessageCommand : ICommand
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0000636F File Offset: 0x0000456F
		public string Command
		{
			get
			{
				return "sendmsg";
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00006376 File Offset: 0x00004576
		public string Description
		{
			get
			{
				return "Send messages";
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00006336 File Offset: 0x00004536
		public string Permission
		{
			get
			{
				return "moderatorcommand";
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000637D File Offset: 0x0000457D
		public string Args
		{
			get
			{
				return "%options% %text%";
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000383FC File Offset: 0x000365FC
		public string Execute(string Command, string[] Args, Account Player)
		{
			string text = Args[0].ToLower();
			string text2 = string.Join(" ", Args, 1, Args.Length - 1);
			if (text.Equals("room"))
			{
				RoomModel room = Player.Room;
				if (room == null)
				{
					return "A room is required to execute the command.";
				}
				using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_SERVER_MESSAGE_ANNOUNCE_ACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text2))
				{
					room.SendPacketToPlayers(protocol_SERVER_MESSAGE_ANNOUNCE_ACK);
				}
				return string.Format("Message sended to current Room Id: {0}", room.RoomId + 1);
			}
			else if (text.Equals("channel"))
			{
				ChannelModel channel = Player.GetChannel();
				if (channel == null)
				{
					return "Please run the command in the lobby.";
				}
				using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_SERVER_MESSAGE_ANNOUNCE_ACK2 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text2))
				{
					channel.SendPacketToWaitPlayers(protocol_SERVER_MESSAGE_ANNOUNCE_ACK2);
				}
				return string.Format("Message sended to current Channel Id: {0}", channel.Id + 1);
			}
			else
			{
				if (text.Equals("player"))
				{
					int num = 0;
					using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_SERVER_MESSAGE_ANNOUNCE_ACK3 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text2))
					{
						SortedList<long, Account> accounts = AccountManager.Accounts;
						if (accounts.Count == 0)
						{
							num = 0;
						}
						byte[] completeBytes = protocol_SERVER_MESSAGE_ANNOUNCE_ACK3.GetCompleteBytes("Player.MessageCommands");
						foreach (Account account in accounts.Values)
						{
							account.SendCompletePacket(completeBytes, protocol_SERVER_MESSAGE_ANNOUNCE_ACK3.GetType().Name);
							num++;
						}
					}
					return string.Format("Message sended to {0} total of player(s)", num);
				}
				return "Command " + ComDiv.ToTitleCase(text) + " was not founded!";
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000025DF File Offset: 0x000007DF
		public MessageCommand()
		{
		}
	}
}
