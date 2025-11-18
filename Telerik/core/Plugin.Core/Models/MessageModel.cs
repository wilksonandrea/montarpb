using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class MessageModel
	{
		public int ClanId
		{
			get;
			set;
		}

		public NoteMessageClan ClanNote
		{
			get;
			set;
		}

		public int DaysRemaining
		{
			get;
			set;
		}

		public long ExpireDate
		{
			get;
			set;
		}

		public long ObjectId
		{
			get;
			set;
		}

		public long SenderId
		{
			get;
			set;
		}

		public string SenderName
		{
			get;
			set;
		}

		public NoteMessageState State
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public NoteMessageType Type
		{
			get;
			set;
		}

		public MessageModel()
		{
			this.SenderName = "";
			this.Text = "";
		}

		public MessageModel(long long_3, DateTime dateTime_0)
		{
			this.SenderName = "";
			this.Text = "";
			this.ExpireDate = long_3;
			this.method_0(dateTime_0);
		}

		public MessageModel(double double_0)
		{
			this.SenderName = "";
			this.Text = "";
			DateTime dateTime = DateTimeUtil.Now().AddDays(double_0);
			this.ExpireDate = long.Parse(dateTime.ToString("yyMMddHHmm"));
			this.method_1(dateTime, DateTimeUtil.Now());
		}

		private void method_0(DateTime dateTime_0)
		{
			long expireDate = this.ExpireDate;
			DateTime dateTime = DateTime.ParseExact(expireDate.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
			this.method_1(dateTime, dateTime_0);
		}

		private void method_1(DateTime dateTime_0, DateTime dateTime_1)
		{
			TimeSpan dateTime0 = dateTime_0 - dateTime_1;
			int ınt32 = (int)Math.Ceiling(dateTime0.TotalDays);
			this.DaysRemaining = (ınt32 < 0 ? 0 : ınt32);
		}
	}
}