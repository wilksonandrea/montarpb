namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_JOIN_REQUEST_REQ : GameClientPacket
    {
        private int int_0;
        private string string_0;
        private uint uint_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
            this.string_0 = base.ReadU(base.ReadC() * 2);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ClanInvite invite1 = new ClanInvite();
                    invite1.Id = this.int_0;
                    invite1.PlayerId = base.Client.PlayerId;
                    invite1.Text = this.string_0;
                    invite1.InviteDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
                    ClanInvite invite = invite1;
                    if ((player.ClanId > 0) || (player.Nickname.Length == 0))
                    {
                        this.uint_0 = 0x8000105c;
                    }
                    else if (ClanManager.GetClan(this.int_0).Id == 0)
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else if (DaoManagerSQL.GetRequestClanInviteCount(this.int_0) >= 100)
                    {
                        this.uint_0 = 0x80001057;
                    }
                    else if (!DaoManagerSQL.CreateClanInviteInDB(invite))
                    {
                        this.uint_0 = 0x80001068;
                    }
                    invite = null;
                    base.Client.SendPacket(new PROTOCOL_CS_JOIN_REQUEST_ACK(this.uint_0, this.int_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_JOIN_REQUEST_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

