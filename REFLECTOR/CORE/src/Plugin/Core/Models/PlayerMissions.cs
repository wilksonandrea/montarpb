namespace Plugin.Core.Models
{
    using Plugin.Core.Utility;
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerMissions
    {
        public PlayerMissions()
        {
            this.List1 = new byte[40];
            this.List2 = new byte[40];
            this.List3 = new byte[40];
            this.List4 = new byte[40];
        }

        public PlayerMissions DeepCopy() => 
            base.MemberwiseClone() as PlayerMissions;

        public int GetCard(int Index) => 
            (Index != 0) ? ((Index != 1) ? ((Index != 2) ? this.Card4 : this.Card3) : this.Card2) : this.Card1;

        public int GetCurrentCard() => 
            this.GetCard(this.ActualMission);

        public int GetCurrentMissionId() => 
            (this.ActualMission != 0) ? ((this.ActualMission != 1) ? ((this.ActualMission != 2) ? this.Mission4 : this.Mission3) : this.Mission2) : this.Mission1;

        public byte[] GetCurrentMissionList() => 
            (this.ActualMission != 0) ? ((this.ActualMission != 1) ? ((this.ActualMission != 2) ? this.List4 : this.List3) : this.List2) : this.List1;

        public void UpdateSelectedCard()
        {
            int currentCard = this.GetCurrentCard();
            if (0xffff == ComDiv.GetMissionCardFlags(this.GetCurrentMissionId(), currentCard, this.GetCurrentMissionList()))
            {
                this.SelectedCard = true;
            }
        }

        public long OwnerId { get; set; }

        public byte[] List1 { get; set; }

        public byte[] List2 { get; set; }

        public byte[] List3 { get; set; }

        public byte[] List4 { get; set; }

        public int ActualMission { get; set; }

        public int Card1 { get; set; }

        public int Card2 { get; set; }

        public int Card3 { get; set; }

        public int Card4 { get; set; }

        public int Mission1 { get; set; }

        public int Mission2 { get; set; }

        public int Mission3 { get; set; }

        public int Mission4 { get; set; }

        public bool SelectedCard { get; set; }
    }
}

