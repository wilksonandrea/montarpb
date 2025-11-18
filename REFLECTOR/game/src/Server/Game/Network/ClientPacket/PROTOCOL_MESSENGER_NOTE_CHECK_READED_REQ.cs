namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ : GameClientPacket
    {
        private readonly List<int> list_0 = new List<int>();

        public override void Read()
        {
            int num = base.ReadC();
            for (int i = 0; i < num; i++)
            {
                this.list_0.Add(base.ReadD());
            }
        }

        public override void Run()
        {
            try
            {
                string[] cOLUMNS = new string[] { "expire_date", "state" };
                object[] vALUES = new object[] { long.Parse(DateTimeUtil.Now().AddDays(7.0).ToString("yyMMddHHmm")), 0 };
                if (ComDiv.UpdateDB("player_messages", "object_id", this.list_0.ToArray(), "owner_id", base.Client.PlayerId, cOLUMNS, vALUES))
                {
                    base.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(this.list_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

