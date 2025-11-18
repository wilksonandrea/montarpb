using Server.Match.Data.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	public class ActionModel
	{
		public byte[] Data
		{
			get;
			set;
		}

		public UdpGameEvent Flag
		{
			get;
			set;
		}

		public ushort Length
		{
			get;
			set;
		}

		public ushort Slot
		{
			get;
			set;
		}

		public UdpSubHead SubHead
		{
			get;
			set;
		}

		public ActionModel()
		{
		}
	}
}