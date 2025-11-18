using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_JOIN_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private string string_0;

	public override void Read()
	{
		int_0 = ReadD();
		string_0 = ReadS(4);
		int_1 = ReadC();
		ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			if (player.Nickname.Length > 0 && player.Room == null && player.Match == null && player.GetChannel(out var Channel))
			{
				RoomModel room = Channel.GetRoom(int_0);
				if (room != null && room.GetLeader() != null)
				{
					if (room.RoomType == RoomCondition.Tutorial)
					{
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487868u, null));
					}
					else if (room.Password.Length > 0 && string_0 != room.Password && player.Rank != 53 && !player.IsGM() && int_1 != 1)
					{
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487749u, null));
					}
					else if (room.Limit == 1 && room.State >= RoomState.COUNTDOWN && !player.IsGM())
					{
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487763u, null));
					}
					else if (room.KickedPlayersVote.Contains(player.PlayerId) && !player.IsGM())
					{
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487756u, null));
					}
					else if (room.KickedPlayersHost.ContainsKey(player.PlayerId) && ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId]) < (double)ConfigLoader.IntervalEnterRoomAfterKickSeconds)
					{
						Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("KickByHostMessage", ConfigLoader.IntervalEnterRoomAfterKickSeconds, (int)ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId]))));
					}
					else if (room.AddPlayer(player) >= 0)
					{
						player.ResetPages();
						using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK packet = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
						{
							room.SendPacketToPlayers(packet, player.PlayerId);
						}
						if (room.Competitive)
						{
							AllUtils.SendCompetitiveInfo(player);
						}
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0u, player));
					}
					else
					{
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487747u, null));
					}
				}
				else
				{
					Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487748u, null));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487748u, null));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_JOIN_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
