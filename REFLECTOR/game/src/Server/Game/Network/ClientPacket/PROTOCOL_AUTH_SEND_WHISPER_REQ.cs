namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_SEND_WHISPER_REQ : GameClientPacket
    {
        private long long_0;
        private string string_0;
        private string string_1;

        public override void Read()
        {
            this.long_0 = base.ReadQ();
            this.string_0 = base.ReadU(0x42);
            this.string_1 = base.ReadU(base.ReadH() * 2);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((player != null) && (player.Nickname != this.string_0))
                {
                    Account account2 = AccountManager.GetAccount(this.long_0, 0x1f);
                    if ((account2 != null) && account2.IsOnline)
                    {
                        account2.SendPacket(new PROTOCOL_AUTH_RECV_WHISPER_ACK(player.Nickname, this.string_1, player.UseChatGM()), false);
                    }
                    else
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_SEND_WHISPER_ACK(this.string_0, this.string_1, 0x80000000));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

