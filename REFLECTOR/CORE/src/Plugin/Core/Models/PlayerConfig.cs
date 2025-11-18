namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerConfig
    {
        public PlayerConfig()
        {
            this.AudioSFX = 100;
            this.AudioBGM = 60;
            this.Crosshair = 2;
            this.Sensitivity = 50;
            this.PointOfView = 80;
            this.ShowBlood = 11;
            this.AudioEnable = 7;
            this.Config = 0x37;
            this.Macro = 0x1f;
            this.Macro1 = "";
            this.Macro2 = "";
            this.Macro3 = "";
            this.Macro4 = "";
            this.Macro5 = "";
            this.Nations = 0;
            this.KeyboardKeys = new byte[240];
        }

        public long OwnerId { get; set; }

        public int Crosshair { get; set; }

        public int AudioSFX { get; set; }

        public int AudioBGM { get; set; }

        public int Sensitivity { get; set; }

        public int PointOfView { get; set; }

        public int ShowBlood { get; set; }

        public int HandPosition { get; set; }

        public int AudioEnable { get; set; }

        public int Config { get; set; }

        public int InvertMouse { get; set; }

        public int EnableInviteMsg { get; set; }

        public int EnableWhisperMsg { get; set; }

        public int Macro { get; set; }

        public int Nations { get; set; }

        public string Macro1 { get; set; }

        public string Macro2 { get; set; }

        public string Macro3 { get; set; }

        public string Macro4 { get; set; }

        public string Macro5 { get; set; }

        public byte[] KeyboardKeys { get; set; }
    }
}

