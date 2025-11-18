using Server.Match.Data.Enums;
using System;

namespace Server.Match.Data.Models.Event
{
	public class UseObjectInfo
	{
		public CharaMoves SpaceFlags;

		public ushort ObjectId;

		public byte Use;

		public UseObjectInfo()
		{
		}
	}
}