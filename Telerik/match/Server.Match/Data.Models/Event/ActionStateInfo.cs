using Server.Match.Data.Enums;
using System;

namespace Server.Match.Data.Models.Event
{
	public class ActionStateInfo
	{
		public ActionFlag Action;

		public byte Value;

		public WeaponSyncType Flag;

		public ActionStateInfo()
		{
		}
	}
}