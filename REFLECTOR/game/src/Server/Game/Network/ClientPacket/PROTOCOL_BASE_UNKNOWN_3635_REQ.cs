namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_UNKNOWN_3635_REQ : GameClientPacket
    {
        private byte byte_0;
        private byte byte_1;
        private byte byte_2;
        private string string_0;
        private short short_0;

        public override void Read()
        {
            this.byte_0 = base.ReadC();
            this.string_0 = base.ReadU(0x42);
            base.ReadD();
            base.ReadH();
            this.byte_1 = base.ReadC();
            base.ReadH();
            base.ReadB(0x10);
            base.ReadB(12);
            this.short_0 = base.ReadH();
            this.byte_2 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                CLogger.Print($"{base.GetType().Name}; Unk1: {this.byte_0}; Nickname: {this.string_0}; Unk2: {this.byte_1}; Unk3: {this.short_0}; Unk4: {this.byte_2}", LoggerType.Warning, null);
            }
            catch (Exception exception)
            {
                CLogger.Print(base.GetType().Name + " Error: " + exception.Message, LoggerType.Error, null);
            }
        }
    }
}

