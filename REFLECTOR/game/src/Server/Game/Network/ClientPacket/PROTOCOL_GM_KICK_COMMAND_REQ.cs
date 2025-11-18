namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_GM_KICK_COMMAND_REQ : GameClientPacket
    {
        private byte byte_0;

        public override void Read()
        {
            this.byte_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if (room != null)
                    {
                        Account playerBySlot = room.GetPlayerBySlot(this.byte_0);
                        if ((playerBySlot != null) && !playerBySlot.IsGM())
                        {
                            room.RemovePlayer(playerBySlot, true, 0);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(base.GetType().Name + ": " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

