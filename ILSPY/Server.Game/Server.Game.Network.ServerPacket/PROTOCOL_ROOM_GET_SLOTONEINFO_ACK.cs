using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_SLOTONEINFO_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly RoomModel roomModel_0;

	private readonly ClanModel clanModel_0;

	public PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(Account account_1)
	{
		account_0 = account_1;
		if (account_1 != null)
		{
			roomModel_0 = account_1.Room;
			clanModel_0 = ClanManager.GetClan(account_1.ClanId);
		}
	}

	public PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(Account account_1, ClanModel clanModel_1)
	{
		account_0 = account_1;
		if (account_1 != null)
		{
			roomModel_0 = account_1.Room;
		}
		clanModel_0 = clanModel_1;
	}

	public override void Write()
	{
		WriteH(3588);
		WriteH(0);
		WriteC((byte)roomModel_0.GetSlot(account_0.SlotId).Team);
		WriteC((byte)roomModel_0.GetSlot(account_0.SlotId).State);
		WriteC((byte)account_0.GetRank());
		WriteD(clanModel_0.Id);
		WriteD(account_0.ClanAccess);
		WriteC((byte)clanModel_0.Rank);
		WriteD(clanModel_0.Logo);
		WriteC((byte)account_0.CafePC);
		WriteC((byte)account_0.Country);
		WriteQ((long)account_0.Effects);
		WriteC((byte)clanModel_0.Effect);
		WriteC((byte)roomModel_0.GetSlot(account_0.SlotId).ViewType);
		WriteC((byte)NATIONS);
		WriteC(0);
		WriteD(account_0.Equipment.NameCardId);
		WriteC((byte)account_0.Bonus.NickBorderColor);
		WriteC((byte)account_0.AuthLevel());
		WriteU(clanModel_0.Name, 34);
		WriteC((byte)account_0.SlotId);
		WriteU(account_0.Nickname, 66);
		WriteC((byte)account_0.NickColor);
		WriteC((byte)account_0.Bonus.MuzzleColor);
		WriteC(0);
		WriteC(byte.MaxValue);
		WriteC(byte.MaxValue);
	}
}
