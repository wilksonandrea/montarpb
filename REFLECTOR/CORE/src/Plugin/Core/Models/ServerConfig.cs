namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class ServerConfig
    {
        public ServerConfig()
        {
            this.UserFileList = "";
            this.ClientVersion = "";
            this.ExitURL = "";
            this.ShopURL = "";
            this.OfficialSteam = "";
            this.OfficialBanner = "";
            this.OfficialAddress = "";
            this.ChannelAnnounce = "";
            this.ChatAnnounce = "";
            this.Showroom = ShowroomView.S_Default;
        }

        public int ConfigId { get; set; }

        public int ChannelAnnounceColor { get; set; }

        public int ChatAnnounceColor { get; set; }

        public bool OnlyGM { get; set; }

        public bool AccessUFL { get; set; }

        public bool Missions { get; set; }

        public bool GiftSystem { get; set; }

        public bool EnableClan { get; set; }

        public bool EnableTicket { get; set; }

        public bool EnablePlaytime { get; set; }

        public bool EnableTags { get; set; }

        public bool EnableBlood { get; set; }

        public bool OfficialBannerEnabled { get; set; }

        public string UserFileList { get; set; }

        public string ClientVersion { get; set; }

        public string ExitURL { get; set; }

        public string ShopURL { get; set; }

        public string OfficialSteam { get; set; }

        public string OfficialBanner { get; set; }

        public string OfficialAddress { get; set; }

        public string ChannelAnnounce { get; set; }

        public string ChatAnnounce { get; set; }

        public ShowroomView Showroom { get; set; }
    }
}

