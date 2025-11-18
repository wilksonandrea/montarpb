namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ : GameClientPacket
    {
        private int int_0;
        private EventErrorEnum eventErrorEnum_0;

        public override void Read()
        {
            this.int_0 = base.ReadC();
            base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    PlayerMissions mission = player.Mission;
                    if ((mission == null) || ((mission.Mission1 == this.int_0) || ((mission.Mission2 == this.int_0) || (mission.Mission3 == this.int_0))))
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK((EventErrorEnum) (-2147479475), null));
                    }
                    else
                    {
                        MissionStore store = MissionConfigXML.GetMission(this.int_0);
                        if ((store == null) || (ShopManager.GetItemId(store.ItemId) == null))
                        {
                            CLogger.Print("There is an error on Mission Config. Please check the configuration!", LoggerType.Warning, null);
                            this.eventErrorEnum_0 = (EventErrorEnum) (-2147479475);
                        }
                        else if (mission.Mission1 == 0)
                        {
                            if (!DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, this.int_0, 0))
                            {
                                this.eventErrorEnum_0 = (EventErrorEnum) (-2147479476);
                            }
                            else
                            {
                                mission.Mission1 = this.int_0;
                                mission.List1 = new byte[40];
                                mission.ActualMission = 0;
                                mission.Card1 = 0;
                            }
                        }
                        else if (mission.Mission2 == 0)
                        {
                            if (!DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, this.int_0, 1))
                            {
                                this.eventErrorEnum_0 = (EventErrorEnum) (-2147479476);
                            }
                            else
                            {
                                mission.Mission2 = this.int_0;
                                mission.List2 = new byte[40];
                                mission.ActualMission = 1;
                                mission.Card2 = 0;
                            }
                        }
                        else if (mission.Mission3 != 0)
                        {
                            this.eventErrorEnum_0 = (EventErrorEnum) (-2147479474);
                        }
                        else if (!DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, this.int_0, 2))
                        {
                            this.eventErrorEnum_0 = (EventErrorEnum) (-2147479476);
                        }
                        else
                        {
                            mission.Mission3 = this.int_0;
                            mission.List3 = new byte[40];
                            mission.ActualMission = 2;
                            mission.Card3 = 0;
                        }
                        int priceGold = ShopManager.GetItemId(store.ItemId).PriceGold;
                        if ((this.eventErrorEnum_0 != EventErrorEnum.SUCCESS) || (0 > (player.Gold - priceGold)))
                        {
                            this.eventErrorEnum_0 = (EventErrorEnum) (-2147479475);
                        }
                        else if ((priceGold != 0) && !DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold - priceGold))
                        {
                            this.eventErrorEnum_0 = (EventErrorEnum) (-2147479476);
                        }
                        else
                        {
                            player.Gold -= priceGold;
                        }
                        base.Client.SendPacket(new PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK(this.eventErrorEnum_0, player));
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

