using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	public class DeathServerData
	{
		public int AssistSlot
		{
			get;
			set;
		}

		public CharaDeath DeathType
		{
			get;
			set;
		}

		public PlayerModel Player
		{
			get;
			set;
		}

		public DeathServerData()
		{
			this.AssistSlot = -1;
		}
	}
}