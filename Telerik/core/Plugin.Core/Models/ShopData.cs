using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class ShopData
	{
		public byte[] Buffer
		{
			get;
			set;
		}

		public int ItemsCount
		{
			get;
			set;
		}

		public int Offset
		{
			get;
			set;
		}

		public ShopData()
		{
		}
	}
}