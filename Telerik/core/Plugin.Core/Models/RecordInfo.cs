using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class RecordInfo
	{
		public long PlayerId
		{
			get;
			set;
		}

		public int RecordValue
		{
			get;
			set;
		}

		public RecordInfo(string[] string_0)
		{
			this.PlayerId = this.GetPlayerId(string_0);
			this.RecordValue = this.GetPlayerValue(string_0);
		}

		public long GetPlayerId(string[] Split)
		{
			long ınt64;
			try
			{
				ınt64 = long.Parse(Split[0]);
			}
			catch
			{
				ınt64 = 0L;
			}
			return ınt64;
		}

		public int GetPlayerValue(string[] Split)
		{
			int ınt32;
			try
			{
				ınt32 = int.Parse(Split[1]);
			}
			catch
			{
				ınt32 = 0;
			}
			return ınt32;
		}

		public string GetSplit()
		{
			string str = this.PlayerId.ToString();
			int recordValue = this.RecordValue;
			return string.Concat(str, "-", recordValue.ToString());
		}
	}
}