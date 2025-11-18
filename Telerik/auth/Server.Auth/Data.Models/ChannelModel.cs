using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Server.Auth.Data.Models
{
	public class ChannelModel
	{
		public int CashBonus
		{
			get;
			set;
		}

		public int ExpBonus
		{
			get;
			set;
		}

		public int GoldBonus
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int MaxRooms
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public int ServerId
		{
			get;
			set;
		}

		public int TotalPlayers
		{
			get;
			set;
		}

		public ChannelType Type
		{
			get;
			set;
		}

		public ChannelModel(int int_7)
		{
			this.ServerId = int_7;
		}
	}
}