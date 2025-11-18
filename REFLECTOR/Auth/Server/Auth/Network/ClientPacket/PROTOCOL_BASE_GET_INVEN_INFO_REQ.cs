namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.XML;
    using Server.Auth.Data.Models;
    using Server.Auth.Data.Utils;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_GET_INVEN_INFO_REQ : AuthClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    int count = 0;
                    List<ItemsModel> list = AllUtils.LimitationIndex(player, player.Inventory.Items);
                    if (list.Count > 0)
                    {
                        count = TemplatePackXML.Basics.Count;
                        if (TemplatePackXML.GetPCCafe(player.CafePC) != null)
                        {
                            count += TemplatePackXML.GetPCCafeRewards(player.CafePC).Count;
                        }
                        base.Client.SendPacket(new PROTOCOL_BASE_GET_INVEN_INFO_ACK(0, list, count));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_GET_INVEN_INFO_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

