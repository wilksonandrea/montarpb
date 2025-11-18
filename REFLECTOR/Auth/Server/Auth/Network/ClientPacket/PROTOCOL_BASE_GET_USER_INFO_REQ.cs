namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Auth.Data.Models;
    using Server.Auth.Data.Utils;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_GET_USER_INFO_REQ : AuthClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((player != null) && (player.Inventory.Items.Count == 0))
                {
                    AllUtils.ValidateAuthLevel(player);
                    AllUtils.LoadPlayerInventory(player);
                    AllUtils.LoadPlayerMissions(player);
                    AllUtils.ValidatePlayerInventoryStatus(player);
                    AllUtils.DiscountPlayerItems(player);
                    AllUtils.CheckGameEvents(player);
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_USER_INFO_ACK(player));
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_CHARA_INFO_ACK(player));
                    AllUtils.ProcessBattlepass(player);
                    base.Client.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE());
                    base.Client.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(player));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_GET_USER_INFO_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

