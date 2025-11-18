using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x02000088 RID: 136
	public class MessageModel
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00005723 File Offset: 0x00003923
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x0000572B File Offset: 0x0000392B
		public int ClanId
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00005734 File Offset: 0x00003934
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0000573C File Offset: 0x0000393C
		public long ObjectId
		{
			[CompilerGenerated]
			get
			{
				return this.long_0;
			}
			[CompilerGenerated]
			set
			{
				this.long_0 = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00005745 File Offset: 0x00003945
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0000574D File Offset: 0x0000394D
		public long SenderId
		{
			[CompilerGenerated]
			get
			{
				return this.long_1;
			}
			[CompilerGenerated]
			set
			{
				this.long_1 = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00005756 File Offset: 0x00003956
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0000575E File Offset: 0x0000395E
		public long ExpireDate
		{
			[CompilerGenerated]
			get
			{
				return this.long_2;
			}
			[CompilerGenerated]
			set
			{
				this.long_2 = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00005767 File Offset: 0x00003967
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0000576F File Offset: 0x0000396F
		public string SenderName
		{
			[CompilerGenerated]
			get
			{
				return this.string_0;
			}
			[CompilerGenerated]
			set
			{
				this.string_0 = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00005778 File Offset: 0x00003978
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00005780 File Offset: 0x00003980
		public string Text
		{
			[CompilerGenerated]
			get
			{
				return this.string_1;
			}
			[CompilerGenerated]
			set
			{
				this.string_1 = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00005789 File Offset: 0x00003989
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00005791 File Offset: 0x00003991
		public NoteMessageState State
		{
			[CompilerGenerated]
			get
			{
				return this.noteMessageState_0;
			}
			[CompilerGenerated]
			set
			{
				this.noteMessageState_0 = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000579A File Offset: 0x0000399A
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x000057A2 File Offset: 0x000039A2
		public NoteMessageType Type
		{
			[CompilerGenerated]
			get
			{
				return this.noteMessageType_0;
			}
			[CompilerGenerated]
			set
			{
				this.noteMessageType_0 = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x000057AB File Offset: 0x000039AB
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x000057B3 File Offset: 0x000039B3
		public NoteMessageClan ClanNote
		{
			[CompilerGenerated]
			get
			{
				return this.noteMessageClan_0;
			}
			[CompilerGenerated]
			set
			{
				this.noteMessageClan_0 = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x000057BC File Offset: 0x000039BC
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x000057C4 File Offset: 0x000039C4
		public int DaysRemaining
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000057CD File Offset: 0x000039CD
		public MessageModel()
		{
			this.SenderName = "";
			this.Text = "";
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000057EB File Offset: 0x000039EB
		public MessageModel(long long_3, DateTime dateTime_0)
		{
			this.SenderName = "";
			this.Text = "";
			this.ExpireDate = long_3;
			this.method_0(dateTime_0);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001BB60 File Offset: 0x00019D60
		public MessageModel(double double_0)
		{
			this.SenderName = "";
			this.Text = "";
			DateTime dateTime = DateTimeUtil.Now().AddDays(double_0);
			this.ExpireDate = long.Parse(dateTime.ToString("yyMMddHHmm"));
			this.method_1(dateTime, DateTimeUtil.Now());
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001BBBC File Offset: 0x00019DBC
		private void method_0(DateTime dateTime_0)
		{
			DateTime dateTime = DateTime.ParseExact(this.ExpireDate.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
			this.method_1(dateTime, dateTime_0);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		private void method_1(DateTime dateTime_0, DateTime dateTime_1)
		{
			int num = (int)Math.Ceiling((dateTime_0 - dateTime_1).TotalDays);
			this.DaysRemaining = ((num < 0) ? 0 : num);
		}

		// Token: 0x0400028A RID: 650
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400028B RID: 651
		[CompilerGenerated]
		private long long_0;

		// Token: 0x0400028C RID: 652
		[CompilerGenerated]
		private long long_1;

		// Token: 0x0400028D RID: 653
		[CompilerGenerated]
		private long long_2;

		// Token: 0x0400028E RID: 654
		[CompilerGenerated]
		private string string_0;

		// Token: 0x0400028F RID: 655
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000290 RID: 656
		[CompilerGenerated]
		private NoteMessageState noteMessageState_0;

		// Token: 0x04000291 RID: 657
		[CompilerGenerated]
		private NoteMessageType noteMessageType_0;

		// Token: 0x04000292 RID: 658
		[CompilerGenerated]
		private NoteMessageClan noteMessageClan_0;

		// Token: 0x04000293 RID: 659
		[CompilerGenerated]
		private int int_1;
	}
}
