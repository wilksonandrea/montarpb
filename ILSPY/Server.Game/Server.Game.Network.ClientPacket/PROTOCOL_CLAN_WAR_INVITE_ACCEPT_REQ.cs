using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private int int_2;

	private uint uint_0;

	public override void Read()
	{
		ReadD();
		int_0 = ReadH();
		int_1 = ReadH();
		int_2 = ReadC();
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
			MatchModel match = player.Match;
			int ıd = int_1 - int_1 / 10 * 10;
			MatchModel match2 = ChannelsXML.GetChannel(int_1, ıd).GetMatch(int_0);
			if (match != null && match2 != null && player.MatchSlot == match.Leader)
			{
				if (int_2 == 1)
				{
					if (match.Training != match2.Training)
					{
						uint_0 = 2147487890u;
					}
					else if (match2.GetCountPlayers() == match.Training && match.GetCountPlayers() == match.Training)
					{
						if (match2.State != MatchState.Play && match.State != MatchState.Play)
						{
							match.State = MatchState.Play;
							Account leader = match2.GetLeader();
							if (leader != null && leader.Match != null)
							{
								leader.SendPacket(new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match));
								leader.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK(match));
								match2.Slots[leader.MatchSlot].State = SlotMatchState.Ready;
							}
							match2.State = MatchState.Play;
						}
						else
						{
							uint_0 = 2147487888u;
						}
					}
					else
					{
						uint_0 = 2147487889u;
					}
				}
				else
				{
					Account leader2 = match2.GetLeader();
					if (leader2 != null && leader2.Match != null)
					{
						leader2.SendPacket(new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(2147487891u));
					}
				}
			}
			else
			{
				uint_0 = 2147487892u;
			}
			Client.SendPacket(new PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("CLAN_WAR_ACCEPT_BATTLE_REC: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
