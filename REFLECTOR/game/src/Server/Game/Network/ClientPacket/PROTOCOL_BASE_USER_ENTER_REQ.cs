namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_USER_ENTER_REQ : GameClientPacket
    {
        private uint uint_0;
        private long long_0;
        private string string_0;

        public override void Read()
        {
            this.string_0 = base.ReadS(base.ReadC());
            this.long_0 = base.ReadQ();
        }

        public override void Run()
        {
            try
            {
                if (base.Client != null)
                {
                    if (base.Client.Player != null)
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else
                    {
                        Account player = AccountManager.GetAccountDB(this.long_0, 2, 0x1f);
                        if ((player == null) || ((player.Username != this.string_0) || (player.Status.ServerId == 0xff)))
                        {
                            this.uint_0 = 0x80000000;
                        }
                        else
                        {
                            base.Client.PlayerId = player.PlayerId;
                            player.Connection = base.Client;
                            player.ServerId = base.Client.ServerId;
                            player.GetAccountInfos(0x1eff);
                            AllUtils.ValidateAuthLevel(player);
                            AllUtils.LoadPlayerInventory(player);
                            AllUtils.LoadPlayerMissions(player);
                            AllUtils.EnableQuestMission(player);
                            AllUtils.ValidatePlayerInventoryStatus(player);
                            player.SetPublicIP(base.Client.GetAddress());
                            PlayerSession session1 = new PlayerSession();
                            session1.SessionId = base.Client.SessionId;
                            session1.PlayerId = base.Client.PlayerId;
                            player.Session = session1;
                            player.Status.UpdateServer((byte) base.Client.ServerId);
                            player.UpdateCacheInfo();
                            base.Client.Player = player;
                            ComDiv.UpdateDB("accounts", "ip4_address", player.PublicIP.ToString(), "player_id", player.PlayerId);
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_BASE_USER_ENTER_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_USER_ENTER_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

