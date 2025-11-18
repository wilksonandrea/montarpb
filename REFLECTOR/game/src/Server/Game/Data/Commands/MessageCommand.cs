namespace Server.Game.Data.Commands
{
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class MessageCommand : ICommand
    {
        public string Execute(string Command, string[] Args, Account Player)
        {
            string text = Args[0].ToLower();
            string str2 = string.Join(" ", Args, 1, Args.Length - 1);
            if (text.Equals("room"))
            {
                RoomModel room = Player.Room;
                if (room == null)
                {
                    return "A room is required to execute the command.";
                }
                using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_server_message_announce_ack = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str2))
                {
                    room.SendPacketToPlayers(protocol_server_message_announce_ack);
                }
                return $"Message sended to current Room Id: {(room.RoomId + 1)}";
            }
            if (text.Equals("channel"))
            {
                ChannelModel channel = Player.GetChannel();
                if (channel == null)
                {
                    return "Please run the command in the lobby.";
                }
                using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_server_message_announce_ack2 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str2))
                {
                    channel.SendPacketToWaitPlayers(protocol_server_message_announce_ack2);
                }
                return $"Message sended to current Channel Id: {(channel.Id + 1)}";
            }
            if (!text.Equals("player"))
            {
                return ("Command " + ComDiv.ToTitleCase(text) + " was not founded!");
            }
            int num = 0;
            using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_server_message_announce_ack3 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str2))
            {
                if (AccountManager.Accounts.Count == 0)
                {
                    num = 0;
                }
                byte[] completeBytes = protocol_server_message_announce_ack3.GetCompleteBytes("Player.MessageCommands");
                using (IEnumerator<Account> enumerator = AccountManager.Accounts.Values.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, protocol_server_message_announce_ack3.GetType().Name);
                        num++;
                    }
                }
            }
            return $"Message sended to {num} total of player(s)";
        }

        public string Command =>
            "sendmsg";

        public string Description =>
            "Send messages";

        public string Permission =>
            "moderatorcommand";

        public string Args =>
            "%options% %text%";
    }
}

