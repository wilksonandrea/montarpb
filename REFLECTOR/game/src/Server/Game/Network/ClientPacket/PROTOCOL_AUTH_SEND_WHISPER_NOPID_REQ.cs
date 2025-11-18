namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ : GameClientPacket
    {
        private long long_0;
        private string string_0;
        private string string_1;

        public override void Read()
        {
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
                    Account account2 = AccountManager.GetAccount(this.string_0, 1, 0x11e);
                    if ((account2 != null) && account2.IsOnline)
                    {
                        account2.SendPacket(new PROTOCOL_AUTH_RECV_WHISPER_ACK(player.Nickname, this.string_1, player.UseChatGM()), true);
                    }
                    else
                    {
                        Console.WriteLine("null");
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

