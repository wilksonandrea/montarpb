using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class SChannelModel
	{
		public int ChannelPlayers
		{
			get;
			set;
		}

		public string Host
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public bool IsMobile
		{
			get;
			set;
		}

		public int LastPlayers
		{
			get;
			set;
		}

		public int MaxPlayers
		{
			get;
			set;
		}

		public ushort Port
		{
			get;
			set;
		}

		public bool State
		{
			get;
			set;
		}

		public SChannelType Type
		{
			get;
			set;
		}

		public SChannelModel(string string_1, ushort ushort_1)
		{
			this.Host = string_1;
			this.Port = ushort_1;
		}
	}
}