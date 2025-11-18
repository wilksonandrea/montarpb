namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_MESSENGER_NOTE_SEND_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;
        private string string_0;
        private string string_1;
        private uint uint_0;

        private MessageModel method_0(string string_2, long long_0, long long_1)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = string_2;
            model1.SenderId = long_1;
            model1.Text = this.string_1;
            model1.State = NoteMessageState.Unreaded;
            MessageModel message = model1;
            if (DaoManagerSQL.CreateMessage(long_0, message))
            {
                return message;
            }
            this.uint_0 = 0x80000000;
            return null;
        }

        public override void Read()
        {
            this.int_0 = base.ReadC();
            this.int_1 = base.ReadC();
            this.string_0 = base.ReadU(this.int_0 * 2);
            this.string_1 = base.ReadU(this.int_1 * 2);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (((player != null) && (player.Nickname.Length != 0)) && (player.Nickname != this.string_0))
                {
                    Account account2 = AccountManager.GetAccount(this.string_0, 1, 0);
                    if (account2 == null)
                    {
                        this.uint_0 = 0x8000107e;
                    }
                    else if (DaoManagerSQL.GetMessagesCount(account2.PlayerId) >= 100)
                    {
                        this.uint_0 = 0x8000107f;
                    }
                    else
                    {
                        MessageModel model = this.method_0(player.Nickname, account2.PlayerId, base.Client.PlayerId);
                        if (model != null)
                        {
                            account2.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model), false);
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_MESSENGER_NOTE_SEND_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

