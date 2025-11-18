using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	public class AssistServerData
	{
		public int Damage
		{
			get;
			set;
		}

		public bool IsAssist
		{
			get;
			set;
		}

		public bool IsKiller
		{
			get;
			set;
		}

		public int Killer
		{
			get;
			set;
		}

		public int RoomId
		{
			get;
			set;
		}

		public int Victim
		{
			get;
			set;
		}

		public bool VictimDead
		{
			get;
			set;
		}

		public AssistServerData()
		{
			this.Killer = -1;
			this.Victim = -1;
		}
	}
}