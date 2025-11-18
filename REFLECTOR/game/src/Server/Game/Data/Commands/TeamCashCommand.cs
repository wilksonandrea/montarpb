namespace Server.Game.Data.Commands
{
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network.ServerPacket;
    using System;

    public class TeamCashCommand : ICommand
    {
        public string Execute(string Command, string[] Args, Account Player)
        {
            int num;
            if (Player.Room == null)
            {
                return "Please execute the command in a room";
            }
            if (Args.Length != 2)
            {
                return "Please use correct command usage, :teamcash %team% %cash%";
            }
            if (!int.TryParse(Args[1], out num))
            {
                return "Please use correct number as value";
            }
            string str = Args[0].ToLower();
            if ((str != "red") && (str != "blue"))
            {
                return "Please use correct team, 'red' or 'blue'";
            }
            int num2 = (int) (str != "red");
            RoomModel room = Player.Room;
            for (int i = 0; i < 0x12; i++)
            {
                if ((i % 2) == num2)
                {
                    SlotModel slot = room.GetSlot(i);
                    if (slot.PlayerId > 0L)
                    {
                        Account playerBySlot = room.GetPlayerBySlot(slot);
                        if ((playerBySlot != null) && DaoManagerSQL.UpdateAccountCash(playerBySlot.PlayerId, playerBySlot.Cash + num))
                        {
                            playerBySlot.Cash += num;
                            playerBySlot.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, playerBySlot));
                            SendItemInfo.LoadGoldCash(playerBySlot);
                        }
                    }
                }
            }
            return $"'{num}' cash sended to team {str}";
        }

        public string Command =>
            "teamcash";

        public string Description =>
            "Send cash to a team";

        public string Permission =>
            "gamemastercommand";

        public string Args =>
            "%team% %cash% (Team = FR/CT)";
    }
}

