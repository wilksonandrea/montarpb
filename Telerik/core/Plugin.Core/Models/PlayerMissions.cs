using Plugin.Core.Utility;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerMissions
	{
		public int ActualMission
		{
			get;
			set;
		}

		public int Card1
		{
			get;
			set;
		}

		public int Card2
		{
			get;
			set;
		}

		public int Card3
		{
			get;
			set;
		}

		public int Card4
		{
			get;
			set;
		}

		public byte[] List1
		{
			get;
			set;
		}

		public byte[] List2
		{
			get;
			set;
		}

		public byte[] List3
		{
			get;
			set;
		}

		public byte[] List4
		{
			get;
			set;
		}

		public int Mission1
		{
			get;
			set;
		}

		public int Mission2
		{
			get;
			set;
		}

		public int Mission3
		{
			get;
			set;
		}

		public int Mission4
		{
			get;
			set;
		}

		public long OwnerId
		{
			get;
			set;
		}

		public bool SelectedCard
		{
			get;
			set;
		}

		public PlayerMissions()
		{
			this.List1 = new byte[40];
			this.List2 = new byte[40];
			this.List3 = new byte[40];
			this.List4 = new byte[40];
		}

		public PlayerMissions DeepCopy()
		{
			return this.MemberwiseClone() as PlayerMissions;
		}

		public int GetCard(int Index)
		{
			if (Index == 0)
			{
				return this.Card1;
			}
			if (Index == 1)
			{
				return this.Card2;
			}
			if (Index == 2)
			{
				return this.Card3;
			}
			return this.Card4;
		}

		public int GetCurrentCard()
		{
			return this.GetCard(this.ActualMission);
		}

		public int GetCurrentMissionId()
		{
			if (this.ActualMission == 0)
			{
				return this.Mission1;
			}
			if (this.ActualMission == 1)
			{
				return this.Mission2;
			}
			if (this.ActualMission == 2)
			{
				return this.Mission3;
			}
			return this.Mission4;
		}

		public byte[] GetCurrentMissionList()
		{
			if (this.ActualMission == 0)
			{
				return this.List1;
			}
			if (this.ActualMission == 1)
			{
				return this.List2;
			}
			if (this.ActualMission == 2)
			{
				return this.List3;
			}
			return this.List4;
		}

		public void UpdateSelectedCard()
		{
			int currentCard = this.GetCurrentCard();
			if (65535 == ComDiv.GetMissionCardFlags(this.GetCurrentMissionId(), currentCard, this.GetCurrentMissionList()))
			{
				this.SelectedCard = true;
			}
		}
	}
}