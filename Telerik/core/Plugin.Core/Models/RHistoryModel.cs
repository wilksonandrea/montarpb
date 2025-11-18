using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class RHistoryModel
	{
		public uint Date
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public long ObjectId
		{
			get;
			set;
		}

		public long OwnerId
		{
			get;
			set;
		}

		public string OwnerNick
		{
			get;
			set;
		}

		public long SenderId
		{
			get;
			set;
		}

		public string SenderNick
		{
			get;
			set;
		}

		public ReportType Type
		{
			get;
			set;
		}

		public RHistoryModel()
		{
		}
	}
}