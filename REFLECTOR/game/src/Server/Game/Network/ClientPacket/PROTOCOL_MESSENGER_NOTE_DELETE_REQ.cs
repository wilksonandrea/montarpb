namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.SQL;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_MESSENGER_NOTE_DELETE_REQ : GameClientPacket
    {
        private uint uint_0;
        private List<object> list_0 = new List<object>();

        public override void Read()
        {
            int num = base.ReadC();
            for (int i = 0; i < num; i++)
            {
                long item = base.ReadUD();
                this.list_0.Add(item);
            }
        }

        public override void Run()
        {
            try
            {
                if (!DaoManagerSQL.DeleteMessages(this.list_0, base.Client.PlayerId))
                {
                    this.uint_0 = 0x80000000;
                }
                base.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_DELETE_ACK(this.uint_0, this.list_0));
                this.list_0 = null;
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_MESSENGER_NOTE_DELETE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

