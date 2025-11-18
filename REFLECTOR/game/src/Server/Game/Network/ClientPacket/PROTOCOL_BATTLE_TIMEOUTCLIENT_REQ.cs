namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    SlotModel model2;
                    RoomModel room = player.Room;
                    if (((room != null) && room.GetSlot(this.int_0, out model2)) && (player.SlotId == model2.Id))
                    {
                        player.SendPacket(new PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK());
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

