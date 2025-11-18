namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class MessageModel
    {
        public MessageModel()
        {
            this.SenderName = "";
            this.Text = "";
        }

        public MessageModel(double double_0)
        {
            this.SenderName = "";
            this.Text = "";
            DateTime time = DateTimeUtil.Now().AddDays(double_0);
            this.ExpireDate = long.Parse(time.ToString("yyMMddHHmm"));
            this.method_1(time, DateTimeUtil.Now());
        }

        public MessageModel(long long_3, DateTime dateTime_0)
        {
            this.SenderName = "";
            this.Text = "";
            this.ExpireDate = long_3;
            this.method_0(dateTime_0);
        }

        private void method_0(DateTime dateTime_0)
        {
            DateTime time = DateTime.ParseExact(this.ExpireDate.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
            this.method_1(time, dateTime_0);
        }

        private void method_1(DateTime dateTime_0, DateTime dateTime_1)
        {
            int num = (int) Math.Ceiling((dateTime_0 - dateTime_1).TotalDays);
            this.DaysRemaining = (num < 0) ? 0 : num;
        }

        public int ClanId { get; set; }

        public long ObjectId { get; set; }

        public long SenderId { get; set; }

        public long ExpireDate { get; set; }

        public string SenderName { get; set; }

        public string Text { get; set; }

        public NoteMessageState State { get; set; }

        public NoteMessageType Type { get; set; }

        public NoteMessageClan ClanNote { get; set; }

        public int DaysRemaining { get; set; }
    }
}

