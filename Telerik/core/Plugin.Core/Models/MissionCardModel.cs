using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class MissionCardModel
	{
		public int ArrayIdx
		{
			get;
			set;
		}

		public int CardBasicId
		{
			get;
			set;
		}

		public int Flag
		{
			get;
			set;
		}

		public int MapId
		{
			get;
			set;
		}

		public int MissionBasicId
		{
			get;
			set;
		}

		public int MissionId
		{
			get;
			set;
		}

		public int MissionLimit
		{
			get;
			set;
		}

		public Plugin.Core.Enums.MissionType MissionType
		{
			get;
			set;
		}

		public ClassType WeaponReq
		{
			get;
			set;
		}

		public int WeaponReqId
		{
			get;
			set;
		}

		public MissionCardModel(int int_8, int int_9)
		{
			this.CardBasicId = int_8;
			this.MissionBasicId = int_9;
			this.ArrayIdx = int_8 * 4 + int_9;
			this.Flag = 15 << (4 * int_9 & 31);
		}
	}
}