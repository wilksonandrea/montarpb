using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class Synchronize
	{
		public IPEndPoint Connection
		{
			get;
			set;
		}

		public int RemotePort
		{
			get;
			set;
		}

		public Synchronize(string string_0, int int_1)
		{
			this.Connection = new IPEndPoint(IPAddress.Parse(string_0), int_1);
		}
	}
}