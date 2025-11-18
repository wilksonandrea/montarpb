namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class ActionState
    {
        public static ActionStateInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            ActionStateInfo info1 = new ActionStateInfo();
            info1.Action = (ActionFlag) C.ReadUH();
            info1.Value = C.ReadC();
            info1.Flag = (WeaponSyncType) C.ReadC();
            ActionStateInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Action {info.Action}; Value: {info.Value}; Flag: {info.Flag}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, ActionStateInfo Info)
        {
            S.WriteH((ushort) Info.Action);
            S.WriteC(Info.Value);
            S.WriteC((byte) Info.Flag);
        }
    }
}

