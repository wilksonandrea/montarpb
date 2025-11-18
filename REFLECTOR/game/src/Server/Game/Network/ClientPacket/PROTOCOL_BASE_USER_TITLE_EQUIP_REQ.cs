namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_USER_TITLE_EQUIP_REQ : GameClientPacket
    {
        private byte byte_0;
        private byte byte_1;

        public override void Read()
        {
            this.byte_0 = base.ReadC();
            this.byte_1 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    TitleModel model2;
                    TitleModel model3;
                    TitleModel model4;
                    PlayerTitles title = player.Title;
                    TitleModel model = TitleSystemXML.GetTitle(this.byte_1, true);
                    TitleSystemXML.Get3Titles(title.Equiped1, title.Equiped2, title.Equiped3, out model2, out model3, out model4, false);
                    if ((((this.byte_0 >= 3) || ((this.byte_1 >= 0x2d) || ((title == null) || ((model == null) || ((model.ClassId == model2.ClassId) && (this.byte_0 != 0)))))) || (((model.ClassId == model3.ClassId) && (this.byte_0 != 1)) || (((model.ClassId == model4.ClassId) && (this.byte_0 != 2)) || !title.Contains(model.Flag)))) || ((title.Equiped1 == this.byte_1) || ((title.Equiped2 == this.byte_1) || (title.Equiped3 == this.byte_1))))
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(0x80000000, -1, -1));
                    }
                    else if (!DaoManagerSQL.UpdateEquipedPlayerTitle(title.OwnerId, this.byte_0, this.byte_1))
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(0x80000000, -1, -1));
                    }
                    else
                    {
                        title.SetEquip(this.byte_0, this.byte_1);
                        base.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(0, this.byte_0, this.byte_1));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_USER_TITLE_EQUIP_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

