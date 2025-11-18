namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_SHOP_REPAIR_REQ : GameClientPacket
    {
        private int int_0;
        private List<long> list_0 = new List<long>();

        public override void Read()
        {
            this.int_0 = base.ReadC();
            for (int i = 0; i < this.int_0; i++)
            {
                uint num2 = base.ReadUD();
                this.list_0.Add((long) num2);
            }
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    int num;
                    int num2;
                    uint num3;
                    List<ItemsModel> list = AllUtils.RepairableItems(player, this.list_0, out num, out num2, out num3);
                    if (list.Count > 0)
                    {
                        player.Gold -= num;
                        player.Cash -= num2;
                        string[] cOLUMNS = new string[] { "gold", "cash" };
                        object[] vALUES = new object[] { player.Gold, player.Cash };
                        if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, cOLUMNS, vALUES))
                        {
                            base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(2, player, list));
                        }
                        base.Client.SendPacket(new PROTOCOL_SHOP_REPAIR_ACK(num3, list, player));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_SHOP_REPAIR_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

