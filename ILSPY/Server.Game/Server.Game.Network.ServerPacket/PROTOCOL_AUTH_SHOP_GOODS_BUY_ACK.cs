using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK : GameServerPacket
{
	private readonly List<GoodsItem> list_0;

	private readonly Account account_0;

	private readonly uint uint_0;

	public PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(uint uint_1, List<GoodsItem> list_1, Account account_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
		list_0 = list_1;
	}

	public PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1044);
		WriteH(0);
		if (uint_0 == 1)
		{
			WriteC((byte)list_0.Count);
			foreach (GoodsItem item in list_0)
			{
				WriteD(0);
				WriteD(item.Id);
				WriteC(0);
			}
			WriteD(account_0.Cash);
			WriteD(account_0.Gold);
			WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}
		else
		{
			WriteD(uint_0);
		}
	}
}
